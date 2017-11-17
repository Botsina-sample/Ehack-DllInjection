using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;



namespace TestDLL
{
    public partial class Form1 : Form
    {
        public string filePath;
        public string fileName;
        public string dllPath;
        public string dllName;
        Inject inj = new Inject();

        public Form1()
        {
            

            InitializeComponent();
            label2.Visible = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        



        public Int32 GetProcessId(String proc)
        {
            Process[] ProcList;
            ProcList = Process.GetProcessesByName(proc);
            return ProcList[0].Id;
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(
            UInt32 dwDesiredAccess,
            Int32 bInheritHandle,
            Int32 dwProcessId
            );


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


                    inj.InjectDLL(hProcess, strDLLName);
             
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
