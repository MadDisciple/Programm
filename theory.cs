using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class theory : Form
    {
        public string pathToTheoryDir = @"Theory";
        public theory()
        {
            InitializeComponent();
            this.CenterToScreen();
            LoadDirectory(pathToTheoryDir);
        }

        private void LoadDirectory(string Dir)
        {
            DirectoryInfo di = new DirectoryInfo(Dir);

            TreeNode tds = treeView1.Nodes.Add(di.Name);

            tds.Tag = di.FullName;

            tds.StateImageIndex = 0;

            LoadFiles(Dir, tds);

            LoadSubDirectories(Dir, tds);
        }

        private void LoadSubDirectories(string dir, TreeNode td)
        {
            string[] subdirectoryEntries = Directory.GetDirectories(dir);

            foreach (string subdirectory in subdirectoryEntries)
            {
                DirectoryInfo di = new DirectoryInfo(subdirectory);

                TreeNode tds = td.Nodes.Add(di.Name);

                tds.StateImageIndex = 0;

                tds.Tag = di.FullName;

                LoadFiles(subdirectory, tds);

                LoadSubDirectories(subdirectory, tds);
            }
        }

        private void LoadFiles(string dir, TreeNode td)
        {
            string[] files = Directory.GetFiles(dir, "*.rtf*");

            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);

                TreeNode tds = td.Nodes.Add(fi.Name);

                tds.Tag = fi.FullName;

                tds.StateImageIndex = 1;
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
          }
            
        


        private void theory_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
           
            if (Path.GetExtension(treeView1.SelectedNode.FullPath) == ".rtf")
            {
                richTextBox1.LoadFile(treeView1.SelectedNode.FullPath);

            }
            {
                

            }
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {

        }

        private void CloseButton_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
