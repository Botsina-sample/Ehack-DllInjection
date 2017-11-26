using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form f = Application.OpenForms.Cast<Form>().Last();

            Form f = Application.OpenForms.Cast<Form>().Last();

            Type fType = f.GetType();

            //MemberInfo[] props = 

            var fieldButton1 = fType.GetField("button1", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);

            Button b1 = (Button) fieldButton1.GetValue(f);

            b1.Text = "Haha";

            var events = (EventHandlerList) b1.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(b1);
            //events
            MessageBox.Show("ABC");
        }
    }
}
