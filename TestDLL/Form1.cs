﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDLL
{
    public partial class Form1 : Form
    {
        public string filePath;
        public string fileName;
        public string dllPath;
        public string dllName;


        public Form1()
        {
            InitializeComponent();
            label2.Visible = false;

        }

        [DllImport("kernel32")]
        public static extern IntPtr CreateRemoteThread(
          IntPtr hProcess,
          IntPtr lpThreadAttributes,
          uint dwStackSize,
          UIntPtr lpStartAddress, // raw Pointer into remote process
          IntPtr lpParameter,
          uint dwCreationFlags,
          out IntPtr lpThreadId
        );

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(
            UInt32 dwDesiredAccess,
            Int32 bInheritHandle,
            Int32 dwProcessId
            );

        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(
        IntPtr hObject
        );

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool VirtualFreeEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            UIntPtr dwSize,
            uint dwFreeType
            );

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern UIntPtr GetProcAddress(
            IntPtr hModule,
            string procName
            );

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            uint dwSize,
            uint flAllocationType,
            uint flProtect
            );

        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            string lpBuffer,
            UIntPtr nSize,
            out IntPtr lpNumberOfBytesWritten
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(
            string lpModuleName
            );

        [DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
        internal static extern Int32 WaitForSingleObject(
            IntPtr handle,
            Int32 milliseconds
            );

        public Int32 GetProcessId(String proc)
        {
            Process[] ProcList;
            ProcList = Process.GetProcessesByName(proc);
            return ProcList[0].Id;
        }

        public void InjectDLL(IntPtr hProcess, String strDLLName)
        {
            IntPtr bytesout;

            // Length of string containing the DLL file name +1 byte padding
            Int32 LenWrite = strDLLName.Length + 1;
            // Allocate memory within the virtual address space of the target process
            IntPtr AllocMem = (IntPtr)VirtualAllocEx(hProcess, (IntPtr)null, (uint)LenWrite, 0x1000, 0x40); //allocation pour WriteProcessMemory

            // Write DLL file name to allocated memory in target process
            WriteProcessMemory(hProcess, AllocMem, strDLLName, (UIntPtr)LenWrite, out bytesout);
            // Function pointer "Injector"
            UIntPtr Injector = (UIntPtr)GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            if (Injector == null)
            {
                MessageBox.Show(" Injector Error! \n ");
                // return failed
                return;
            }

            // Create thread in target process, and store handle in hThread
            IntPtr hThread = (IntPtr)CreateRemoteThread(hProcess, (IntPtr)null, 0, Injector, AllocMem, 0, out bytesout);
            // Make sure thread handle is valid
            if (hThread == null)
            {
                //incorrect thread handle ... return failed
                MessageBox.Show(" hThread [ 1 ] Error! \n ");
                return;
            }
            // Time-out is 10 seconds...
            int Result = WaitForSingleObject(hThread, 10 * 1000);
            // Check whether thread timed out...
            if (Result == 0x00000080L || Result == 0x00000102L || Result == 0xFFFFFFFF)
            {
                /* Thread timed out... */
                MessageBox.Show(" hThread [ 2 ] Error! \n ");
                // Make sure thread handle is valid before closing... prevents crashes.
                if (hThread != null)
                {
                    //Close thread in target process
                    CloseHandle(hThread);
                }
                return;
            }
            // Sleep thread for 1 second
            System.Threading.Thread.Sleep(1000);
            // Clear up allocated space ( Allocmem )
            VirtualFreeEx(hProcess, AllocMem, (UIntPtr)0, 0x8000);
            // Make sure thread handle is valid before closing... prevents crashes.
            if (hThread != null)
            {
                //Close thread in target process
                CloseHandle(hThread);
            }
            // return succeeded
            return;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            String strDLLName = dllPath;
            String strProcessName = fileName;
            


            Int32 ProcID = GetProcessId(strProcessName);
            if (ProcID >= 0)
            {
                IntPtr hProcess = (IntPtr)OpenProcess(0x1F0FFF, 1, ProcID);
                if (hProcess == null)
                {
                    MessageBox.Show("OpenProcess() Failed!");
                    return;
                }
                else
                    label2.Visible = true;
                    label2.Text = "Injected successfully to " + strProcessName;
           
                InjectDLL(hProcess, strDLLName);
                   
                
                

             
            }
        }


        // browse to exe location
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenThisFile = new OpenFileDialog();


            if(OpenThisFile.ShowDialog() == DialogResult.OK)
            {

                filePath = OpenThisFile.FileName;
                fileName = Path.GetFileNameWithoutExtension(filePath);

                //MessageBox.Show(filePath.ToString());
                textBox1.Text = filePath.ToString();
                Process.Start(fileName);
            }


            //FolderBrowserDialog browser = new FolderBrowserDialog();
            //string tempPath = "";

            //if (browser.ShowDialog() == DialogResult.OK)
            //{
            //    tempPath = browser.SelectedPath; // prints path
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenThisDLL = new OpenFileDialog();
            if (OpenThisDLL.ShowDialog() == DialogResult.OK)
            {

                dllPath = OpenThisDLL.FileName;
                
                dllName = Path.GetFullPath(dllPath);

                textBox2.Text = dllPath.ToString();


            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
           

        }
    }
}
