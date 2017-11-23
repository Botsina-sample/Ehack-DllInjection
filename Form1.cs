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
using ManagedInjector;

namespace TestDLL
{
    public partial class Form1 : Form
    {
        public string filePath;
        public string fileName;

        public string dllPath;
        public string dllName;
        public static int chosenApp;

        public static string notify;
        


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
            switch(temp)
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

            }
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            openApp(_apps.notepad.ToString());
            chosenApp = (int)_apps.notepad;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            openApp(_apps.wordpad.ToString());
            chosenApp = (int)_apps.wordpad;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            openApp(_apps.ieplore.ToString());
            chosenApp = (int)_apps.ieplore;
        }


        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            openApp(_apps.calc.ToString());
            chosenApp = (int)_apps.calc;
        }


        public void openApp(string application)
        {

            if(Process.GetProcessesByName(application).Length > 0)
            {

                foreach (Process running_app in Process.GetProcessesByName(application))
                {
                    running_app.Kill();
                }
            }

                        

            //fileName = Path.GetFileNameWithoutExtension(application);

            new Thread (() =>
            {
                Thread.CurrentThread.IsBackground = true;
                /* run your code here */
                Thread.Sleep(1000);
                Process.Start(application);


                //Thread.Sleep(1000);
                //doInject(application);

            }).Start();



            //Thread.Sleep(2000);
            //Process.Start(application);
        }

        public static void doInject(string apps)
        {

            do
            {

                var proc = Process.GetProcessesByName(apps).FirstOrDefault();
                ManagedInjector.Injector.Launch(proc.MainWindowHandle,
                    typeof(Loader).Assembly.Location,
                    typeof(Loader).FullName, "Inject");
            }
            while (Process.GetProcessesByName(apps).Length < 0);
        }
    }

    public class Loader
    {
        public static void Inject()
        {
            MessageBox.Show("Inject successful");
        }
    }
}
