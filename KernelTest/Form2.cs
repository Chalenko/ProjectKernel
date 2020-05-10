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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            ProjectKernel.Controls.NestedTreeNode tripleTreeNode1 = new ProjectKernel.Controls.NestedTreeNode("1");

            ProjectKernel.Controls.NestedTreeNode tripleTreeNode211 = new ProjectKernel.Controls.NestedTreeNode("2.1.1");
            ProjectKernel.Controls.NestedTreeNode tripleTreeNode212 = new ProjectKernel.Controls.NestedTreeNode("2.1.2");
            ProjectKernel.Controls.NestedTreeNode tripleTreeNode21 = new ProjectKernel.Controls.NestedTreeNode("2.1", new ProjectKernel.Controls.NestedTreeNode[] { tripleTreeNode211, tripleTreeNode212 });
            ProjectKernel.Controls.NestedTreeNode tripleTreeNode2 = new ProjectKernel.Controls.NestedTreeNode("2", new ProjectKernel.Controls.NestedTreeNode[] { tripleTreeNode21 });
            tripleTreeNode2.Nodes.Add(tripleTreeNode211);

            ProjectKernel.Controls.NestedTreeNode tripleTreeNode31 = new ProjectKernel.Controls.NestedTreeNode("3.1");
            ProjectKernel.Controls.NestedTreeNode tripleTreeNode32 = new ProjectKernel.Controls.NestedTreeNode("3.2");
            ProjectKernel.Controls.NestedTreeNode tripleTreeNode33 = new ProjectKernel.Controls.NestedTreeNode("3.3");
            ProjectKernel.Controls.NestedTreeNode tripleTreeNode3 = new ProjectKernel.Controls.NestedTreeNode("3", new ProjectKernel.Controls.NestedTreeNode[] { tripleTreeNode31, tripleTreeNode32, tripleTreeNode33 });

            ProjectKernel.Controls.NestedTreeNode tripleTreeNode4 = new ProjectKernel.Controls.NestedTreeNode("4");

            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            tripleTreeNode1,
            tripleTreeNode2,
            tripleTreeNode3,
            tripleTreeNode4});

            NestedTreeNode[] nodes = KernelTest.ControlsTests.NestedTreeNodeTest.CreateTestTree().Nodes.Cast<NestedTreeNode>().ToArray();
            //NestedTreeNode root2 = new NestedTreeNode("Root 2");
            //root2.Nodes.AddRange(root.Nodes.Cast<TreeNode>().ToArray());
            //this.nestedTreeView1.Nodes.Add(nodes);
            //this.nestedTreeView1.Nodes.Add(root2);
            //this.nestedTreeView1.Nodes.AddRange(nodes);
            this.nestedTreeView1.AddNodesRange(nodes);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            ;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewHitTestInfo HitTestInfo = this.treeView1.HitTest(e.X, e.Y);
            if (HitTestInfo != null && HitTestInfo.Location == TreeViewHitTestLocations.StateImage)
            {
                ((ProjectKernel.Controls.NestedTreeNode)e.Node).Checked = ((ProjectKernel.Controls.NestedTreeNode)e.Node).Checked;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nestedTreeView1.Root.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ((NestedTreeNode)(nestedTreeView1.Nodes[2].Nodes[0])).Checked = true;
        }

        private void nestedTreeView1_CheckedChanged(object sender, TreeViewEventArgs e)
        {
            ;
        }
    }
}
