using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 资源获取
{
    public partial class FileManage : Form
    {
        public FileManage()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
            this.BindInfo(node);
        }

        private void BindInfo(TreeNode node)
        {
             //清空
            listView1.Items.Clear();
            //绑定子目录
            DirectoryInfo dir = new DirectoryInfo(node.Tag.ToString());
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo di in dirs)
            {
                TreeNode temp = new TreeNode();
                temp.Text = di.Name;
                temp.Tag = di.FullName;
                node.Nodes.Add(temp);
            }
            //获取目录下文件列表，dir是目录对象
            FileInfo[] fi = dir.GetFiles();
            //定义反省集合存储文件信息
            List<MyFile> files = new List<MyFile>();
            //遍历文件列表
            foreach (FileInfo myFile in fi)
            {
                MyFile mf = new MyFile();
                mf.FileName = myFile.Name;
                mf.FileLength = myFile.Length;
                mf.FileType = myFile.Extension;
                mf.FilePath = myFile.FullName;
                files.Add(mf);
            }
            //绑定到Listview中
            foreach (MyFile em in files)
            {
                ListViewItem lv = new ListViewItem(em.FileName);
                lv.SubItems.Add(em.FileLength.ToString());
                lv.SubItems.Add(em.FileType);
                lv.SubItems.Add(em.FilePath);
                listView1.Items.Add(lv);
            }

        }

        private void 复制文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //判断是否选中，没选中return
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选中一个文件");
                return;
            }
            //提示用户选择目标文件夹
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            //源文件路径
            string sourcepath = listView1.SelectedItems[0].SubItems[3].Text;
            //目标文件路径
            string despath = null;
            //如果正确选择目标位置，执行复制操作
            if (result == DialogResult.OK)
            {
                despath = fbd.SelectedPath;
                //lvlist表示显示文件信息的ListView对象
                despath += "\\" + listView1.SelectedItems[0].SubItems[0].Text;
                //复制文件
                try
                {
                    File.Copy(sourcepath, despath);
                    MessageBox.Show("复制成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void 删除文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string delectpath = listView1.SelectedItems[0].SubItems[3].Text;
            try
            {
                File.Delete(delectpath);
                MessageBox.Show("删除成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FileManage_Load(object sender, EventArgs e)
        {
            //检索计算机上的所有逻辑驱动器的驱动器名称
            DriveInfo[] di = DriveInfo.GetDrives();
            foreach (DriveInfo item in di)
            {
                TreeNode tn = new TreeNode(item.Name);
                tn.Tag = item.Name;
                treeView1.Nodes.Add(tn);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
