namespace ProjectKernel.Controls
{
    partial class NestedTreeView
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // NestedTreeView
            // 
            this.CheckBoxes = true;
            this.LineColor = System.Drawing.Color.Black;
            this.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.NestedTreeView_AfterCheck);
            this.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.NestedTreeView_NodeMouseClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NestedTreeView_KeyDown);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Кореневой элемент дерева. В дереве отображается как узел 'Все'
        /// </summary>
        protected NestedTreeNode root;

        #endregion
    }
}
