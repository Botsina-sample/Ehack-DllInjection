using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Injector
{
    class Actions
    {
        public void openApp(string application)
        {
            if (Process.GetProcessesByName(application).Length > 0)
            {
                foreach (Process running_app in Process.GetProcessesByName(application))
                {
                    running_app.Kill();
                }
            } 
            
            
            //fileName = Path.GetFileNameWithoutExtension(application
            new Thread(() =>
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
