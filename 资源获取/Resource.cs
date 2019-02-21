using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 网速测试;
using 自动关机;

namespace 资源获取
{
    public partial class Resource : Form
    {
        public Resource()
        {
            InitializeComponent();
        }

        private void Resource_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MachineInfo machineInfo = new MachineInfo();
            textBox1.Text = machineInfo.example();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Speed s1 = new Speed();
            s1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Shutdown s1 = new Shutdown();
            s1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FileManage fileManage = new FileManage();
            fileManage.Show();
        }
    }
}
