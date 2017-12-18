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
        message mess = new message("dade");

        public Form1()
        {

            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            //openSampleUI(@"C:\Users\EDI\Documents\GitHub\Ehack-DllInjection\SampleUI\bin\Debug\SampleUI.exe");
        }

        public void openSampleUI(string path)
        {
            Process.Start(path);
        }



        // Inject button
        private void button1_Click(object sender, EventArgs e)
        {
           
                    new Thread(() =>
                    {
                        act.doInject(AppByBrowse);
                    }).Start();
            
            
        }






        // broswe to app's location
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
  
            //Filter for EXE
            theDialog.Title = "Open an Application";
            theDialog.Filter = "EXE files|*.exe";

            // Default directory
            //theDialog.InitialDirectory = @"C:\Users\davis\Desktop\test";

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

      

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process[] processlist = Process.GetProcesses();
            


            foreach (Process theprocess in processlist)
            {
                
                //Console.WriteLine(“Process: { 0}
                //ID: { 1}”, theprocess.ProcessName, theprocess.Id
                listBox1.Items.Add(theprocess.ProcessName + "..............." + theprocess.Id);
                
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(act._is64.ToString());
        }
    }
}