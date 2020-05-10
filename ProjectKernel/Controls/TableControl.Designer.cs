namespace ProjectKernel.Controls
{
    partial class TableControl
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
            this.components = new System.ComponentModel.Container();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvTable = new System.Windows.Forms.DataGridView();
            this.cmsTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.csmiRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.csmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.cmsTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdd,
            this.tsmiRemove,
            this.tsmiOpen});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(474, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Image = global::ProjectKernel.Properties.Resources.add16;
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(57, 20);
            this.tsmiAdd.Text = "Add";
            this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
            // 
            // tsmiRemove
            // 
            this.tsmiRemove.Enabled = false;
            this.tsmiRemove.Image = global::ProjectKernel.Properties.Resources.delete16;
            this.tsmiRemove.Name = "tsmiRemove";
            this.tsmiRemove.Size = new System.Drawing.Size(78, 20);
            this.tsmiRemove.Text = "Remove";
            this.tsmiRemove.Click += new System.EventHandler(this.tsmiRemove_Click);
            // 
            // tsmiOpen
            // 
            this.tsmiOpen.Enabled = false;
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.Size = new System.Drawing.Size(48, 20);
            this.tsmiOpen.Text = "Open";
            this.tsmiOpen.Click += new System.EventHandler(this.tsmiOpen_Click);
            // 
            // dgvTable
            // 
            this.dgvTable.AllowUserToAddRows = false;
            this.dgvTable.AllowUserToDeleteRows = false;
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.ContextMenuStrip = this.cmsTable;
            this.dgvTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTable.Location = new System.Drawing.Point(0, 24);
            this.dgvTable.MultiSelect = false;
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.ReadOnly = true;
            this.dgvTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTable.Size = new System.Drawing.Size(474, 247);
            this.dgvTable.TabIndex = 1;
            this.dgvTable.DataSourceChanged += new System.EventHandler(this.dgvTable_DataSourceChanged);
            this.dgvTable.CurrentCellChanged += new System.EventHandler(this.dgvTable_CurrentCellChanged);
            this.dgvTable.SelectionChanged += new System.EventHandler(this.dgvTable_SelectionChanged);
            this.dgvTable.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvTable_MouseDoubleClick);
            // 
            // cmsTable
            // 
            this.cmsTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csmiAdd,
            this.csmiRemove,
            this.csmiOpen});
            this.cmsTable.Name = "cmsTable";
            this.cmsTable.Size = new System.Drawing.Size(118, 70);
            // 
            // csmiAdd
            // 
            this.csmiAdd.Image = global::ProjectKernel.Properties.Resources.add16;
            this.csmiAdd.Name = "csmiAdd";
            this.csmiAdd.Size = new System.Drawing.Size(117, 22);
            this.csmiAdd.Text = "Add";
            this.csmiAdd.Click += new System.EventHandler(this.csmiAdd_Click);
            // 
            // csmiRemove
            // 
            this.csmiRemove.Enabled = false;
            this.csmiRemove.Image = global::ProjectKernel.Properties.Resources.delete16;
            this.csmiRemove.Name = "csmiRemove";
            this.csmiRemove.Size = new System.Drawing.Size(117, 22);
            this.csmiRemove.Text = "Remove";
            this.csmiRemove.Click += new System.EventHandler(this.csmiRemove_Click);
            // 
            // csmiOpen
            // 
            this.csmiOpen.Enabled = false;
            this.csmiOpen.Name = "csmiOpen";
            this.csmiOpen.Size = new System.Drawing.Size(117, 22);
            this.csmiOpen.Text = "Open";
            this.csmiOpen.Click += new System.EventHandler(this.csmiOpen_Click);
            // 
            // TableControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvTable);
            this.Controls.Add(this.menu);
            this.Name = "TableControl";
            this.Size = new System.Drawing.Size(474, 271);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.cmsTable.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemove;
        private System.Windows.Forms.DataGridView dgvTable;
        private System.Windows.Forms.ContextMenuStrip cmsTable;
        private System.Windows.Forms.ToolStripMenuItem csmiAdd;
        private System.Windows.Forms.ToolStripMenuItem csmiRemove;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
        private System.Windows.Forms.ToolStripMenuItem csmiOpen;
    }
}
