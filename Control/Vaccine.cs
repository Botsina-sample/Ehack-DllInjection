using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace Injector
{
    class Vaccine
    {
        //public static string _currentProcName;




        public static void Nexus()
        {

            System.Windows.Forms.MessageBox.Show("Injected", "WARNING!!!");

            //var currentproc = Process.GetCurrentProcess();

            //var currentprocID = currentproc.Id;



            //try
            //{
            //    Application application = Application.Attach(currentprocID);
            //    Window window = application.GetWindow("Form1", InitializeOption.NoCache);
            //} catch (Exception ex)
            //{
            //    string filePath = @"D:\Error.txt";

            //    using (StreamWriter writer = new StreamWriter(filePath, true))
            //    {
            //        writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
            //           "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
            //        writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            //    }
            //}


        }
        
    }
}
