using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Injector;
using ManagedInjector;

namespace TestDLL
{
    public partial class Form1 : Form
    {
        public string filePath;
        public string fileName;

        public static int chosenApp;

        public static string notify;
        Actions ac = new Actions();


        public enum _apps
        {
            notepad,
            wordpad,
            ieplore,
            calc
        }



        public Form1()
        {
            InitializeComponent();
            label2.Visible = false;

        }

        // INject button
        private void button1_Click(object sender, EventArgs e)
        {
            var temp = chosenApp;
            //MessageBox.Show(temp.ToString());
            switch (temp)
            {

                case 0:
                    doInject(_apps.notepad.ToString());
                    break;

                case 1:
                    doInject(_apps.wordpad.ToString());
                    break;
                case 2:
                    doInject(_apps.ieplore.ToString());
                    break;
                case 3:
                    doInject(_apps.calc.ToString());
                    break;

                default:
                    MessageBox.Show("Not supported");
                    break;
            }
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ac.openApp(_apps.notepad.ToString());
            chosenApp = (int) _apps.notepad;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ac.openApp(_apps.wordpad.ToString());
            chosenApp = (int) _apps.wordpad;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ac.openApp(_apps.ieplore.ToString());
            chosenApp = (int) _apps.ieplore;
        }


        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            ac.openApp(_apps.calc.ToString()); 
            chosenApp = (int) _apps.calc;
        }

   

        #region ManageInjectorCode
        public static void doInject(string apps)
        {
            do
            {
                var proc = Process.GetProcessesByName(apps).FirstOrDefault();
                ManagedInjector.Injector.Launch(proc.MainWindowHandle,
                    typeof(Vaccine).Assembly.Location,
                    typeof(Vaccine).FullName, "Nexus");
            } while (Process.GetProcessesByName(apps).Length < 0);
        }


        #endregion



        // broswe to app's location
        private void button2_Click(object sender, EventArgs e)
        {
            
           
            OpenFileDialog theDialog = new OpenFileDialog();

            //Filter for EXE
            theDialog.Title = "Open Exe File";
            theDialog.Filter = "EXE files|*.exe";
            theDialog.InitialDirectory = @"C:\";

            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedFile = Path.GetFileNameWithoutExtension(theDialog.FileName);
                textBox1.Text = theDialog.FileName;

                if (selectedFile == "wordpad")
                {
                    chosenApp = (int) _apps.wordpad;
                    MessageBox.Show(chosenApp.ToString());
                }

                if (selectedFile == "iexplore")
                {
                    chosenApp = (int) _apps.ieplore;
                    MessageBox.Show(chosenApp.ToString());
                }

                //MessageBox.Show(selectedFile);   
                ac.openApp(selectedFile);
            }
        }


        //reset button for injector
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}