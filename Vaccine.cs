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

            MessageBox.Show("Injected");


            var currentProcName = Process.GetCurrentProcess();
            Thread.Sleep(1000);
            MessageBox.Show(currentProcName.ToString());

        }
    }
}
