using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using TestDLL;


namespace Injector
{
    class Vaccine
    {
     
        public static void Nexus()
        {

            MessageBox.Show("Injected", "WARNING!!!");


            var currentProcName = Process.GetCurrentProcess();
            Thread.Sleep(500);
            MessageBox.Show(currentProcName.ToString(), "Proc name");


            Form1.notify = "Hello";
        }

    }
}
