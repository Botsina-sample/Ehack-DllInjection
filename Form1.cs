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

                default:
                    MessageBox.Show("Not supported");
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
			//fileName = Path.GetFileNameWithoutExtension(application
			new Thread (() =>
			{
				Thread.CurrentThread.IsBackground = true;
				/* run your code here */
				Thread.Sleep(1000);
				Process.Start(application);

				/*new thread to inject
				Thread.Sleep(1000);
				doInject(application); */
                
			}).Start();

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

                if(selectedFile == "wordpad")         
                {
                    chosenApp = (int)_apps.wordpad;
                    MessageBox.Show(chosenApp.ToString());
                }

                if(selectedFile == "iexplore")
                {
                    chosenApp = (int)_apps.ieplore;
                    MessageBox.Show(chosenApp.ToString());
                }



                //MessageBox.Show(selectedFile);   
				openApp(selectedFile);
			}
		}


		//reset button for injector
		private void button3_Click(object sender, EventArgs e)
		{
			Application.Restart();
		}
	}

<<<<<<< HEAD
	public class Loader
	{
		public static void Inject()
		{
			MessageBox.Show("Inject successful");
		}

	} 
=======
            //Filter for EXE
            theDialog.Title = "Open Exe File";
            theDialog.Filter = "EXE files|*.exe";
            theDialog.InitialDirectory = @"C:\";

            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedFile = Path.GetFileNameWithoutExtension(theDialog.FileName);
                textBox1.Text = theDialog.FileName;
                //MessageBox.Show(selectedFile);
                Process.Start(selectedFile);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class Loader
    {
        public static void Inject()
        {
            MessageBox.Show("Inject successful ", "WARNING!!!");
 
        }


    } 
>>>>>>> 19de3ce09ec92a16560f4e87c3b19ebe5a6adc24
}
