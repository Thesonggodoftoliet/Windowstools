using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 自动关机
{
    public partial class Shutdown : Form
    {
        public Shutdown()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int taghours = int.Parse(textBox1.Text);
            int tagminutes = int.Parse(textBox2.Text);
            int nowhours = DateTime.Now.Hour;
            int nowminutes = DateTime.Now.Minute;
            long lefttime = 0;

            if (taghours < nowhours)
            {
                taghours += 24;
                if (tagminutes < nowminutes)
                    lefttime += (tagminutes + 60 - nowminutes) * 60 + (taghours - 1 - nowhours) * 60 * 60;
                else lefttime += (tagminutes - nowminutes) * 60 + (taghours - nowhours) * 60 * 60;
            }
            else
            {
                if (tagminutes < nowminutes)
                    lefttime += (tagminutes + 60 - nowminutes) * 60 + (taghours - 1 - nowhours) * 60 * 60;
                else lefttime += (tagminutes - nowminutes) * 60 + (taghours - nowhours) * 60 * 60;
            }

            var startInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe");

            startInfo.UseShellExecute = false;

            startInfo.RedirectStandardInput = true;

            startInfo.RedirectStandardOutput = true;

            startInfo.RedirectStandardError = true;

            startInfo.CreateNoWindow = true;

            var myProcess = new System.Diagnostics.Process();

            myProcess.StartInfo = startInfo;

            myProcess.Start();

            myProcess.StandardInput.WriteLine("shutdown -s -t " + lefttime);

        }

        private void Shutdown_Load(object sender, EventArgs e)
        {
            textBox1.Text = DateTime.Now.Hour.ToString();
            textBox2.Text = DateTime.Now.Minute.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           // textBox1.Text = DateTime.Now.Hour.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //textBox2.Text = DateTime.Now.Minute.ToString();
            //textBox2.ReadOnly = false;
        }

        /*private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Interval = 1000;
            timer1.Enabled = false;
            textBox1.Text = DateTime.Now.Hour.ToString();
            timer1.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.timer2.Interval = 1000;
            timer2.Enabled = false;
            textBox2.Text = DateTime.Now.Minute.ToString();
            timer2.Enabled = true;
        }*/
    }
}
