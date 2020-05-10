namespace ProjectKernel.Forms
{
    partial class UserForm
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelFIO = new System.Windows.Forms.Panel();
            this.tbPatronymic = new System.Windows.Forms.TextBox();
            this.lblPatronymic = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.tbSurname = new System.Windows.Forms.TextBox();
            this.lblSurname = new System.Windows.Forms.Label();
            this.panelExtended = new System.Windows.Forms.Panel();
            this.cbIsActive = new System.Windows.Forms.CheckBox();
            this.cbChangePassword = new System.Windows.Forms.CheckBox();
            this.btnDropPassword = new System.Windows.Forms.Button();
            this.tbDepartment = new System.Windows.Forms.TextBox();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.lblLogin = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabcRoles = new ProjectKernel.Controls.TableControl();
            this.tlpMain.SuspendLayout();
            this.panelFIO.SuspendLayout();
            this.panelExtended.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.panelFIO, 0, 0);
            this.tlpMain.Controls.Add(this.panelExtended, 1, 0);
            this.tlpMain.Controls.Add(this.pnlButtons, 0, 2);
            this.tlpMain.Controls.Add(this.tabcRoles, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(586, 397);
            this.tlpMain.TabIndex = 0;
            // 
            // panelFIO
            // 
            this.panelFIO.AutoSize = true;
            this.panelFIO.Controls.Add(this.tbPatronymic);
            this.panelFIO.Controls.Add(this.lblPatronymic);
            this.panelFIO.Controls.Add(this.tbName);
            this.panelFIO.Controls.Add(this.lblName);
            this.panelFIO.Controls.Add(this.tbSurname);
            this.panelFIO.Controls.Add(this.lblSurname);
            this.panelFIO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFIO.Location = new System.Drawing.Point(3, 3);
            this.panelFIO.Name = "panelFIO";
            this.panelFIO.Size = new System.Drawing.Size(287, 110);
            this.panelFIO.TabIndex = 0;
            // 
            // tbPatronymic
            // 
            this.tbPatronymic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPatronymic.Location = new System.Drawing.Point(95, 61);
            this.tbPatronymic.Name = "tbPatronymic";
            this.tbPatronymic.Size = new System.Drawing.Size(189, 20);
            this.tbPatronymic.TabIndex = 5;
            // 
            // lblPatronymic
            // 
            this.lblPatronymic.AutoSize = true;
            this.lblPatronymic.Location = new System.Drawing.Point(3, 64);
            this.lblPatronymic.Name = "lblPatronymic";
            this.lblPatronymic.Size = new System.Drawing.Size(54, 13);
            this.lblPatronymic.TabIndex = 4;
            this.lblPatronymic.Text = "Отчество";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(95, 35);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(189, 20);
            this.tbName.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 38);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(29, 13);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Имя";
            // 
            // tbSurname
            // 
            this.tbSurname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSurname.Location = new System.Drawing.Point(95, 9);
            this.tbSurname.Name = "tbSurname";
            this.tbSurname.Size = new System.Drawing.Size(189, 20);
            this.tbSurname.TabIndex = 1;
            // 
            // lblSurname
            // 
            this.lblSurname.AutoSize = true;
            this.lblSurname.Location = new System.Drawing.Point(3, 13);
            this.lblSurname.Name = "lblSurname";
            this.lblSurname.Size = new System.Drawing.Size(56, 13);
            this.lblSurname.TabIndex = 0;
            this.lblSurname.Text = "Фамилия";
            // 
            // panelExtended
            // 
            this.panelExtended.AutoSize = true;
            this.panelExtended.Controls.Add(this.cbIsActive);
            this.panelExtended.Controls.Add(this.cbChangePassword);
            this.panelExtended.Controls.Add(this.btnDropPassword);
            this.panelExtended.Controls.Add(this.tbDepartment);
            this.panelExtended.Controls.Add(this.lblDepartment);
            this.panelExtended.Controls.Add(this.tbLogin);
            this.panelExtended.Controls.Add(this.lblLogin);
            this.panelExtended.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExtended.Location = new System.Drawing.Point(296, 3);
            this.panelExtended.Name = "panelExtended";
            this.panelExtended.Size = new System.Drawing.Size(287, 110);
            this.panelExtended.TabIndex = 1;
            // 
            // cbIsActive
            // 
            this.cbIsActive.AutoSize = true;
            this.cbIsActive.Checked = true;
            this.cbIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsActive.Location = new System.Drawing.Point(6, 84);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(68, 17);
            this.cbIsActive.TabIndex = 6;
            this.cbIsActive.Text = "Активен";
            this.cbIsActive.UseVisualStyleBackColor = true;
            // 
            // cbChangePassword
            // 
            this.cbChangePassword.AutoSize = true;
            this.cbChangePassword.Location = new System.Drawing.Point(6, 61);
            this.cbChangePassword.Name = "cbChangePassword";
            this.cbChangePassword.Size = new System.Drawing.Size(203, 17);
            this.cbChangePassword.TabIndex = 5;
            this.cbChangePassword.Text = "Сменить пароль при первом входе";
            this.cbChangePassword.UseVisualStyleBackColor = true;
            // 
            // btnDropPassword
            // 
            this.btnDropPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDropPassword.Location = new System.Drawing.Point(95, 84);
            this.btnDropPassword.Name = "btnDropPassword";
            this.btnDropPassword.Size = new System.Drawing.Size(189, 23);
            this.btnDropPassword.TabIndex = 4;
            this.btnDropPassword.Text = "Сбросить пароль";
            this.btnDropPassword.UseVisualStyleBackColor = true;
            this.btnDropPassword.Click += new System.EventHandler(this.btnDropPassword_Click);
            // 
            // tbDepartment
            // 
            this.tbDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDepartment.Location = new System.Drawing.Point(95, 35);
            this.tbDepartment.Name = "tbDepartment";
            this.tbDepartment.Size = new System.Drawing.Size(189, 20);
            this.tbDepartment.TabIndex = 3;
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Location = new System.Drawing.Point(3, 39);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(87, 13);
            this.lblDepartment.TabIndex = 2;
            this.lblDepartment.Text = "Подразделение";
            // 
            // tbLogin
            // 
            this.tbLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLogin.Location = new System.Drawing.Point(95, 9);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(189, 20);
            this.tbLogin.TabIndex = 1;
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(3, 13);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(38, 13);
            this.lblLogin.TabIndex = 0;
            this.lblLogin.Text = "Логин";
            // 
            // pnlButtons
            // 
            this.pnlButtons.AutoSize = true;
            this.tlpMain.SetColumnSpan(this.pnlButtons, 2);
            this.pnlButtons.Controls.Add(this.btnOk);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButtons.Location = new System.Drawing.Point(3, 364);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(580, 30);
            this.pnlButtons.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(421, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(502, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabcRoles
            // 
            this.tabcRoles.AddMenuItemText = "Добавить";
            this.tlpMain.SetColumnSpan(this.tabcRoles, 2);
            this.tabcRoles.DataSource = null;
            this.tabcRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabcRoles.Location = new System.Drawing.Point(3, 119);
            this.tabcRoles.Name = "tabcRoles";
            this.tabcRoles.OpenMenuItemText = "Открыть";
            this.tabcRoles.RemoveMenuItemText = "Исключить";
            this.tabcRoles.Size = new System.Drawing.Size(580, 239);
            this.tabcRoles.TabIndex = 2;
            this.tabcRoles.AddClick += new System.EventHandler(this.tabcRoles_AddClick);
            this.tabcRoles.RemoveClick += new System.EventHandler(this.tabcRoles_RemoveClick);
            this.tabcRoles.OpenButton.Visible = false;
            // 
            // UserForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(586, 397);
            this.Controls.Add(this.tlpMain);
            this.Name = "UserForm";
            this.Text = "Пользователь";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.panelFIO.ResumeLayout(false);
            this.panelFIO.PerformLayout();
            this.panelExtended.ResumeLayout(false);
            this.panelExtended.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel panelFIO;
        private System.Windows.Forms.TextBox tbPatronymic;
        private System.Windows.Forms.Label lblPatronymic;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox tbSurname;
        private System.Windows.Forms.Label lblSurname;
        private System.Windows.Forms.Panel panelExtended;
        private System.Windows.Forms.Button btnDropPassword;
        private System.Windows.Forms.TextBox tbDepartment;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private Controls.TableControl tabcRoles;
        private System.Windows.Forms.CheckBox cbChangePassword;
        private System.Windows.Forms.CheckBox cbIsActive;
    }
}