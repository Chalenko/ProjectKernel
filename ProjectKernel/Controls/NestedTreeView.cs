using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectKernel.Controls
{
    /// <summary>
    /// Отображает иерархическую коллекцию помеченных элементов, каждый из которых представлен объектом ProjectKernel.NestedTreeNode.
    /// </summary>
    public partial class NestedTreeView : TreeView
    {
        /// <summary>
        /// Происходит при смене состояния узла дерева.
        /// </summary>
        public event TreeViewEventHandler CheckedChanged;

        /// <summary>
        /// Инициализирует новый экземпляр класса ProjectKernel.NestedTreeNode.
        /// </summary>
        public NestedTreeView()
        {
            InitializeComponent();

            root = new NestedTreeNode("Все");
            root.NodeFont = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Bold);

            this.SetStyle(ControlStyles.StandardDoubleClick, false);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса ProjectKernel.NestedTreeNode с элементами для отображения.
        /// </summary>
        /// <param name="items">Элементы для отображения</param>
        public NestedTreeView(NestedTreeNode[] items) : this()
        {
            this.Nodes.Add(root);

            this.Nodes.AddRange(items);
        }
        
        /// <summary>
        /// Возвращает элемент с меткой "Все".
        /// </summary>
        public NestedTreeNode Root
        {
            get
            {
                return root;
            }
        }
        
        /// <summary>
        /// Добавляет элемент в список отображаемых элементов.
        /// </summary>
        /// <param name="item">Элемент для отображения.</param>
        public void AddNode(NestedTreeNode item)
        {
            if (Nodes.Count == 0 || !Root.Equals(Nodes[0]))
                this.Nodes.Insert(0, Root);

            this.Nodes.Add(item);
        }

        /// <summary>
        /// Получение узла по индексу
        /// </summary>
        /// <param name="index">Индекс узла</param>
        /// <returns>Узел дерева</returns>
        public NestedTreeNode GetNode(int index)
        {
            return (NestedTreeNode)Nodes[index];
        }

        /// <summary>
        /// Добавляет элементы в список отображаемых элементов.
        /// </summary>
        /// <param name="items">Список элементов для отображения.</param>
        public void AddNodesRange(NestedTreeNode[] items)
        {
            if (Nodes.Count == 0 || !Root.Equals(Nodes[0]))
                this.Nodes.Insert(0, Root);

            this.Nodes.AddRange(items);
        }

        /// <summary>
        /// Переопределяет метод System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@).
        /// </summary>
        /// <param name="m">Сообщение System.Windows.Forms.Message Windows для обработки.</param>
        /// <remarks>Используется для фиксации бага с MouseDoubleClick и пересылки события MouseDoubleClick на MouseClick.</remarks>
        protected override void WndProc(ref Message m)
        {
            // Change WM_LBUTTONDBLCLK to WM_LBUTTONCLICK
            if (m.Msg == 0x203) m.Msg = 0x201;
            base.WndProc(ref m);
        }

        private void NestedTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewHitTestInfo HitTestInfo = this.HitTest(e.X, e.Y);

            if (HitTestInfo != null && HitTestInfo.Location == TreeViewHitTestLocations.StateImage)
            {
                ChangeState((NestedTreeNode)(e.Node), e.Node.Checked);
            }
        }

        private void NestedTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                bool isChecked = !this.SelectedNode.Checked;

                foreach (NestedTreeNode child in this.SelectedNode.Nodes)
                    ChangeState(child, isChecked);
            }
        }

        private void ChangeState(NestedTreeNode node, bool newState)
        {
            bool previousState = node.Checked;
            node.Checked = newState;

            //if (previousState != newState) CheckedChanged.Invoke(this, new TreeViewEventArgs(node, TreeViewAction.Unknown));

            if (node.Equals(Root))
                for (int i = 1; i < Nodes.Count; i++)
                    ((NestedTreeNode)(Nodes[i])).Checked = newState;
        }

        private void NestedTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (((NestedTreeNode)(e.Node)).Equals(Root))
                for (int i = 1; i < Nodes.Count; i++)
                    ((NestedTreeNode)(Nodes[i])).Checked = e.Node.Checked;
        }
    }
}
