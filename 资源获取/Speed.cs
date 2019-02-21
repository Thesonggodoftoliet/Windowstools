using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 网速测试
{
    public partial class Speed : Form
    {
        private NetWorkAdapter[] adapters;
        private NetWorkMonitor monitor;

        public Speed()
        {
            InitializeComponent();
        }

        private void Speed_Load(object sender, EventArgs e)
        {
            monitor = new NetWorkMonitor();
            this.adapters = monitor.Adapters;
            /* If the length of adapters is zero, then no instance 
             * exists in the networking category of performance console.*/
            if (adapters.Length == 0)
            {
                this.listBox1.Enabled = false;
                MessageBox.Show("No network adapters found on this computer.");
                return;
            }
            this.listBox1.Items.AddRange(this.adapters);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            monitor.StopMonitoring();
            // Start a timer to obtain new performance counter sample every second.
            monitor.StartMonitoring(adapters[this.listBox1.SelectedIndex]);
            this.timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            NetWorkAdapter adapter = this.adapters[this.listBox1.SelectedIndex];
            /* The DownloadSpeedKbps and UploadSpeedKbps are double values. You can also 
             * use properties DownloadSpeed and UploadSpeed, which are long values but 
             * are measured in bytes per second. */
            this.label5.Text = String.Format("{0:n} kbps", adapter.DownloadSpeedKbps);
            this.label6.Text = String.Format("{0:n} kbps", adapter.UploadSpeedKbps);
        }
    }
}
