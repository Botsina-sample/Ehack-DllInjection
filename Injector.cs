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


namespace TestDLL
{
    public partial class Form1 : Form
    {
        public string filePath;
        public string fileName;

        public static int chosenApp;

        public string AppByBrowse;

        public static string notify;


        Actions act = new Actions();

        public enum _apps
        {
            windows,
            /// <summary>
            /// ////////////
            /// </summary>

            notepad,
            wordpad,
            calc,
            mspaint
        }

        public Form1()
        {

            InitializeComponent();
            groupBox3.Enabled = false;
            groupBox1.Enabled = false;
            openSampleUI(@"C:\Users\EDI\Documents\GitHub\Ehack-DllInjection\SampleUI\bin\Debug\SampleUI.exe");
        }

        public void openSampleUI(string path)
        {
            Process.Start(path);
        }



        // Inject button
        private void button1_Click(object sender, EventArgs e)
        {
            var temp = chosenApp;
            //MessageBox.Show(temp.ToString());
            switch (temp)
            {

                case 1:
                    new Thread(() =>
                    {
                        act.doInject(_apps.notepad.ToString());

                    }).Start();

                    break;

                case 2:
                    new Thread(() =>
                    {;
                        act.doInject(_apps.wordpad.ToString());
                    }).Start();

                    break;
                case 3:
                    new Thread(() =>
                    {
                        act.doInject(_apps.calc.ToString());
                    }).Start();

                    break;
                case 4:
                    new Thread(() =>
                    {
                        act.doInject(_apps.mspaint.ToString());
                    }).Start();

                    break;

                default:
                    //MessageBox.Show("Not supported");
                    new Thread(() =>
                    {
                        act.doInject(AppByBrowse);
                    }).Start();

                    break;
            }
        }






        // broswe to app's location
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();

            //Filter for EXE
            theDialog.Title = "Open an Application";
            theDialog.Filter = "EXE files|*.exe";
            theDialog.InitialDirectory = @"C:\Users\davis\Desktop\test";

            // open dialog
            if (theDialog.ShowDialog() == DialogResult.OK)
            {

                AppByBrowse = Path.GetFileNameWithoutExtension(theDialog.FileName);

                textBox1.Text = theDialog.FileName;

                act.openApp(theDialog.FileName);

            }
        }


        //reset button for injector
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            act.openApp(_apps.notepad.ToString());

            chosenApp = (int)_apps.notepad;

            //MessageBox.Show(chosenApp.ToString());
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            act.openApp(_apps.wordpad.ToString());
            chosenApp = (int)_apps.wordpad;
            //MessageBox.Show(chosenApp.ToString());
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            act.openApp(_apps.calc.ToString());
            chosenApp = (int)_apps.calc;
            //MessageBox.Show(chosenApp.ToString());
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            act.openApp(_apps.mspaint.ToString());
            chosenApp = (int)_apps.mspaint;
            //MessageBox.Show(chosenApp.ToString());
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}