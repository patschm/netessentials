using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtA.Text, out int a) && int.TryParse(txtB.Text, out int b))
            {
                int result = LongAdd(a, b);
                UpdateAnswer(result);
            }
        }

        private void UpdateAnswer(object result)
        {
            lblAnswer.Text = result.ToString();
        }

        private int LongAdd(int a, int b)
        {
           Task.Delay(10000).Wait();
            return a + b;
        }
    }
}
