using ProjectKernel.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KernelTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //headerContextMenuStrip1.Size = headerContextMenuStrip1.PreferredSize;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBTESTDataSet.Users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.dBTESTDataSet.Users);

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewHitTestInfo HitTestInfo = this.treeView1.HitTest(e.X, e.Y);
            if (HitTestInfo != null && HitTestInfo.Location == TreeViewHitTestLocations.StateImage) 
            {
                TripleTreeNode node = (TripleTreeNode)e.Node;
                if (node.CheckState == CheckState.Checked)
                    node.CheckState = CheckState.Unchecked;
                else
                    node.CheckState = CheckState.Checked;
            }
        }
    }
}
