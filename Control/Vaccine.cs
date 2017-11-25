using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using TestDLL;
using System.Reflection;

namespace Injector
{
    class Vaccine
    {
        public static string _currentProcName;




        public static void Nexus()
        {

            MessageBox.Show("Injected", "WARNING!!!");

            Form f = Application.OpenForms.Cast<Form>().Last();

            Type fType = f.GetType();

            //MemberInfo[] props = 

            var fieldButton1 = fType.GetField("button1", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);

            Button b1 = (Button)fieldButton1.GetValue(f);

            b1.Text = "Haha";
        }

    }
}
