using ProjectKernel.Forms.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectKernel.Forms
{
    /// <summary>
    /// Форма для работы с карточкой пользователя приложения
    /// </summary>
    public partial class UserForm : Form
    {
        private UserViewModel viewModel;

        private UserForm()
        {
            InitializeComponent();
            //DataTable dt = new DataTable();
            //dt.Columns.Add("id", typeof(Guid));
            //dt.Columns.Add("name", typeof(string));
            //tabcRoles.DataSource = dt;
            //tabcRoles.DataSource = ProjectKernel.Classes.DatabaseContext.Instance.LoadFromDatabase("select distinct name from roles where name like '%ri%'", CommandType.Text);
        }

        public UserForm(UserViewModel viewModel) : this()
        {
            this.viewModel = viewModel;
            backBind();
            tabcRoles.DataGridView.Columns["id"].Visible = false;
        }

        private void btnDropPassword_Click(object sender, EventArgs e)
        {
            bind();
            viewModel.dropPassword(this);
            backBind();
        }

        private void tabcRoles_AddClick(object sender, EventArgs e)
        {
            bind();
            viewModel.addRole(this);
            backBind();
        }

        private void tabcRoles_RemoveClick(object sender, EventArgs e)
        {
            bind();
            viewModel.removeRole(this, tabcRoles.DataGridView.CurrentRow.Index);
            backBind();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bind();
            viewModel.OKPerform();
            if (viewModel.OKResult == ActionResult.OK)
                this.Close();
        }

        private void bind()
        {
            viewModel.Surname = tbSurname.Text;
            viewModel.Name = tbName.Text;
            viewModel.Patronymic = tbPatronymic.Text;
            viewModel.Login = tbLogin.Text;
            viewModel.Department = tbDepartment.Text;
            viewModel.IsActive = cbIsActive.Checked;
            viewModel.HaveToChangePassword = cbChangePassword.Checked;
            viewModel.Roles = (DataTable)tabcRoles.DataSource;
        }

        private void backBind()
        {
            tbSurname.Text = viewModel.Surname;
            tbName.Text = viewModel.Name;
            tbPatronymic.Text = viewModel.Patronymic;
            tbLogin.Text = viewModel.Login;
            tbDepartment.Text = viewModel.Department;
            cbIsActive.Checked = viewModel.IsActive;
            cbChangePassword.Checked = viewModel.HaveToChangePassword;
            tabcRoles.DataSource = viewModel.Roles;
            tabcRoles.DataGridView.Columns["id"].Visible = false;
            tabcRoles.DataGridView.Columns["name"].HeaderText = "Наименование";

            tbSurname.ReadOnly = !viewModel.canEdit;
            tbName.ReadOnly = !viewModel.canEdit;
            tbPatronymic.ReadOnly = !viewModel.canEdit;
            tbLogin.ReadOnly = !viewModel.canEdit;
            tbDepartment.ReadOnly = !viewModel.canEdit;
            cbChangePassword.Enabled = viewModel.canEdit;
            btnDropPassword.Enabled = viewModel.canDropPassword;
            tabcRoles.CanEdit = viewModel.canEdit;
        }
    }
}
