using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            //SynchronizationContext? hoofdthread = SynchronizationContext.Current;

            if (int.TryParse(txtA.Text, out int a) && int.TryParse(txtB.Text, out int b))
            {
                //int result = LongAdd(a, b);
                //UpdateAnswer(result);
                //Task.Run(() => LongAdd(a, b))
                //    .ContinueWith(pt => {
                //        hoofdthread?.Post(UpdateAnswer!, pt.Result);
                //    });


                // Deadlock!!!!
                int result = DeadLocker(a, b).Result;//.ConfigureAwait(false);
                UpdateAnswer(result);
            }
        }
        private async Task<int> DeadLocker(int a, int b)
        {
            var res = await LongAddAsync(a, b);
            return res;
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
        private Task<int> LongAddAsync(int a, int b)
        {
            return Task.Run(() => LongAdd(a, b));
        }
    }
}
