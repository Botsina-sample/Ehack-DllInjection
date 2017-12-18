using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Injector
{
    class message
    {
        //protected string _name = "REQ_PB";
        //protected bool _clicked = false;
        //protected string _messRec = "";


        //public string name
        //{
        //    get { return _name; }
        //    set { _name = value; }
        //}

        //public bool clicked
        //{
        //    get { return _clicked; }
        //    set { _clicked = value; }
        //}

        //public string messRec
        //{
        //    get { return _messRec; }
        //    set { _messRec = value; }
        //}

        private string _appname = "notepad";

        public string name
        {
            get { return _appname; }
            set { _appname = value; }
        }
        
        public message(string name)
        {
            this._appname = name;
        }

    }




    class Actions
    {
        

        public bool _is64 = false;

        public bool proc64
        {
            get { return _is64; }
            set { _is64 = value; }
        }


        public void openApp(string application)
        {

            Process[] proc = Process.GetProcessesByName(application);
            
            if(Environment.Is64BitProcess)
            {
                //MessageBox.Show(_is64.ToString());
                proc64 = true;
            }

            //fileName = Path.GetFileNameWithoutExtension(application
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                /* run your code here */
  
                Process.Start(application);

                /*new thread to inject
                Thread.Sleep(1000);
                doInject(application); */

            }).Start();

        }


        

        public void doInject(string apps)
        {
            do
            {
                var proc = Process.GetProcessesByName(apps).FirstOrDefault();
                ManagedInjector.Injector.Launch(proc.MainWindowHandle,
                    typeof(Vaccine).Assembly.Location,
                    typeof(Vaccine).FullName, "Nexus");
            } while (Process.GetProcessesByName(apps).Length < 0);
        }


    }
}
