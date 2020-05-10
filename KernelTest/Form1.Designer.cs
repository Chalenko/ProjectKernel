using ProjectKernel.Controls;
namespace KernelTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Button button1;
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.surnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patronymicnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loginDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saltDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passwordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.departmentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.creatoridDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifieridDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifydateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.headerContextMenuStrip1 = new ProjectKernel.Controls.HeaderContextMenuStrip();
            this.usersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dBTESTDataSet = new KernelTest.DBTESTDataSet();
            this.usersTableAdapter = new KernelTest.DBTESTDataSetTableAdapters.UsersTableAdapter();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.treeView1 = new System.Windows.Forms.TreeView();
            button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBTESTDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            button1.Location = new System.Drawing.Point(620, 485);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.surnameDataGridViewTextBoxColumn,
            this.firstnameDataGridViewTextBoxColumn,
            this.patronymicnameDataGridViewTextBoxColumn,
            this.loginDataGridViewTextBoxColumn,
            this.saltDataGridViewTextBoxColumn,
            this.passwordDataGridViewTextBoxColumn,
            this.departmentDataGridViewTextBoxColumn,
            this.creatoridDataGridViewTextBoxColumn,
            this.createdateDataGridViewTextBoxColumn,
            this.modifieridDataGridViewTextBoxColumn,
            this.modifydateDataGridViewTextBoxColumn});
            this.dataGridView1.ContextMenuStrip = this.headerContextMenuStrip1;
            this.dataGridView1.DataSource = this.usersBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(13, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(682, 143);
            this.dataGridView1.TabIndex = 2;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // surnameDataGridViewTextBoxColumn
            // 
            this.surnameDataGridViewTextBoxColumn.DataPropertyName = "surname";
            this.surnameDataGridViewTextBoxColumn.HeaderText = "surname";
            this.surnameDataGridViewTextBoxColumn.Name = "surnameDataGridViewTextBoxColumn";
            this.surnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // firstnameDataGridViewTextBoxColumn
            // 
            this.firstnameDataGridViewTextBoxColumn.DataPropertyName = "first_name";
            this.firstnameDataGridViewTextBoxColumn.HeaderText = "first_name";
            this.firstnameDataGridViewTextBoxColumn.Name = "firstnameDataGridViewTextBoxColumn";
            this.firstnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // patronymicnameDataGridViewTextBoxColumn
            // 
            this.patronymicnameDataGridViewTextBoxColumn.DataPropertyName = "patronymic_name";
            this.patronymicnameDataGridViewTextBoxColumn.HeaderText = "patronymic_name";
            this.patronymicnameDataGridViewTextBoxColumn.Name = "patronymicnameDataGridViewTextBoxColumn";
            this.patronymicnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // loginDataGridViewTextBoxColumn
            // 
            this.loginDataGridViewTextBoxColumn.DataPropertyName = "login";
            this.loginDataGridViewTextBoxColumn.HeaderText = "login";
            this.loginDataGridViewTextBoxColumn.Name = "loginDataGridViewTextBoxColumn";
            this.loginDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // saltDataGridViewTextBoxColumn
            // 
            this.saltDataGridViewTextBoxColumn.DataPropertyName = "salt";
            this.saltDataGridViewTextBoxColumn.HeaderText = "salt";
            this.saltDataGridViewTextBoxColumn.Name = "saltDataGridViewTextBoxColumn";
            this.saltDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // passwordDataGridViewTextBoxColumn
            // 
            this.passwordDataGridViewTextBoxColumn.DataPropertyName = "password";
            this.passwordDataGridViewTextBoxColumn.HeaderText = "password";
            this.passwordDataGridViewTextBoxColumn.Name = "passwordDataGridViewTextBoxColumn";
            this.passwordDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // departmentDataGridViewTextBoxColumn
            // 
            this.departmentDataGridViewTextBoxColumn.DataPropertyName = "department";
            this.departmentDataGridViewTextBoxColumn.HeaderText = "department";
            this.departmentDataGridViewTextBoxColumn.Name = "departmentDataGridViewTextBoxColumn";
            this.departmentDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // creatoridDataGridViewTextBoxColumn
            // 
            this.creatoridDataGridViewTextBoxColumn.DataPropertyName = "creator_id";
            this.creatoridDataGridViewTextBoxColumn.HeaderText = "creator_id";
            this.creatoridDataGridViewTextBoxColumn.Name = "creatoridDataGridViewTextBoxColumn";
            this.creatoridDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // createdateDataGridViewTextBoxColumn
            // 
            this.createdateDataGridViewTextBoxColumn.DataPropertyName = "create_date";
            this.createdateDataGridViewTextBoxColumn.HeaderText = "create_date";
            this.createdateDataGridViewTextBoxColumn.Name = "createdateDataGridViewTextBoxColumn";
            this.createdateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // modifieridDataGridViewTextBoxColumn
            // 
            this.modifieridDataGridViewTextBoxColumn.DataPropertyName = "modifier_id";
            this.modifieridDataGridViewTextBoxColumn.HeaderText = "modifier_id";
            this.modifieridDataGridViewTextBoxColumn.Name = "modifieridDataGridViewTextBoxColumn";
            this.modifieridDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // modifydateDataGridViewTextBoxColumn
            // 
            this.modifydateDataGridViewTextBoxColumn.DataPropertyName = "modify_date";
            this.modifydateDataGridViewTextBoxColumn.HeaderText = "modify_date";
            this.modifydateDataGridViewTextBoxColumn.Name = "modifydateDataGridViewTextBoxColumn";
            this.modifydateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // headerContextMenuStrip1
            // 
            this.headerContextMenuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.headerContextMenuStrip1.IsFilterDateAndTimeEnabled = false;
            this.headerContextMenuStrip1.IsFilterEnabled = false;
            this.headerContextMenuStrip1.IsSortEnabled = false;
            this.headerContextMenuStrip1.MinimumSize = new System.Drawing.Size(186, 97);
            this.headerContextMenuStrip1.Name = "headerContextMenuStrip1";
            this.headerContextMenuStrip1.Size = new System.Drawing.Size(223, 326);
            // 
            // usersBindingSource
            // 
            this.usersBindingSource.DataMember = "Users";
            this.usersBindingSource.DataSource = this.dBTESTDataSet;
            // 
            // dBTESTDataSet
            // 
            this.dBTESTDataSet.DataSetName = "DBTESTDataSet";
            this.dBTESTDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // usersTableAdapter
            // 
            this.usersTableAdapter.ClearBeforeFill = true;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.checkedListBox1.Location = new System.Drawing.Point(12, 161);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(683, 94);
            this.checkedListBox1.TabIndex = 3;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.CheckBoxes = true;
            //this.treeView1.StateImageList = GetCheckImages();
            this.treeView1.StateImageList = new System.Windows.Forms.ImageList();
            this.treeView1.StateImageList.Images.Add("1", ProjectKernel.Properties.Resources.MenuStrip_OrderASCnum);
            this.treeView1.StateImageList.Images.Add("2", ProjectKernel.Properties.Resources.MenuStrip_OrderDESCtxt);
            this.treeView1.StateImageList.Images.Add("3", ProjectKernel.Properties.Resources.MenuStrip_OrderDESCbool);
            this.treeView1.Location = new System.Drawing.Point(13, 262);
            this.treeView1.Name = "treeView1";
            this.treeView1.BeginUpdate();
            TripleTreeNode treeNode1 = new TripleTreeNode("5", 5, System.Windows.Forms.CheckState.Unchecked);
            TripleTreeNode treeNode4 = new TripleTreeNode("6", 6, System.Windows.Forms.CheckState.Unchecked);
            treeNode4.CreateChild("6.1", 61, System.Windows.Forms.CheckState.Unchecked);
            treeNode4.CreateChild("6.2", 62, System.Windows.Forms.CheckState.Unchecked);
            treeNode4.CreateChild("6.3", 63, System.Windows.Forms.CheckState.Unchecked);
            TripleTreeNode treeNode5 = new TripleTreeNode("7", 7, System.Windows.Forms.CheckState.Unchecked);
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode4,
            treeNode5});
            this.treeView1.EndUpdate();
            
            //treeView1.StateImageList = new System.Windows.Forms.ImageList();
            //treeView1.StateImageList.Images.Add(System.Drawing.SystemIcons.Exclamation);
            // Add some nodes to the TreeView and the TreeView to the form.
            //treeView1.Nodes.Add("Node1");
            //treeView1.Nodes.Add("Node2");
            
            this.treeView1.Size = new System.Drawing.Size(682, 97);
            this.treeView1.TabIndex = 4;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 520);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(button1);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBTESTDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ImageList GetCheckImages()
        {
            System.Windows.Forms.ImageList images = new System.Windows.Forms.ImageList();
            System.Drawing.Bitmap unCheckImg = new System.Drawing.Bitmap(16, 16);
            System.Drawing.Bitmap checkImg = new System.Drawing.Bitmap(16, 16);
            System.Drawing.Bitmap mixedImg = new System.Drawing.Bitmap(16, 16);

            using (System.Drawing.Bitmap img = new System.Drawing.Bitmap(16, 16))
            {
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img))
                {
                    System.Windows.Forms.CheckBoxRenderer.DrawCheckBox(g, new System.Drawing.Point(0, 1), System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
                    unCheckImg = (System.Drawing.Bitmap)img.Clone();
                    System.Windows.Forms.CheckBoxRenderer.DrawCheckBox(g, new System.Drawing.Point(0, 1), System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal);
                    checkImg = (System.Drawing.Bitmap)img.Clone();
                    System.Windows.Forms.CheckBoxRenderer.DrawCheckBox(g, new System.Drawing.Point(0, 1), System.Windows.Forms.VisualStyles.CheckBoxState.MixedNormal);
                    mixedImg = (System.Drawing.Bitmap)img.Clone();
                }
            }

            images.Images.Add("uncheck", unCheckImg);
            images.Images.Add("check", checkImg);
            images.Images.Add("mixed", mixedImg);

            return images;
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private DBTESTDataSet dBTESTDataSet;
        private System.Windows.Forms.BindingSource usersBindingSource;
        private DBTESTDataSetTableAdapters.UsersTableAdapter usersTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn surnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn patronymicnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loginDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn saltDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn passwordDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn departmentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn creatoridDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifieridDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifydateDataGridViewTextBoxColumn;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private ProjectKernel.Controls.HeaderContextMenuStrip headerContextMenuStrip1;
        private System.Windows.Forms.TreeView treeView1;
    }
}