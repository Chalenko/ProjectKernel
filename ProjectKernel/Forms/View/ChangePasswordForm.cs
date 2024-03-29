﻿using ProjectKernel.Classes.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectKernel.Forms.ViewModel;

namespace ProjectKernel.Forms.View
{
    /// <summary>
    /// Форма для изменения пароля
    /// </summary>
    public partial class ChangePasswordForm : Form
    {
        private ChangePasswordFormViewModel vm;

        /// <summary>
        /// Результат смены пароля
        /// </summary>
        public ActionResult ChangeResult { get { return vm.Result; } }

        private ChangePasswordForm()
        {
            InitializeComponent();
            foreach (Control c in Controls)
                c.KeyDown += ChangePasswordForm_KeyDown;
        }

        /// <summary>
        /// Инициализирует форму изменения пароля
        /// </summary>
        /// <param name="model">Модель отображения для формы</param>
        public ChangePasswordForm(ChangePasswordFormViewModel model) : this()
        {
            this.vm = model;
        }

        /// <summary>
        /// Обработка события Нажатие кнопки Cancel
        /// </summary>
        /// <param name="sender">Отправитель события</param>
        /// <param name="e">Аргументы события</param>
        protected virtual void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Обработка события Нажатие кнопки OK
        /// </summary>
        /// <param name="sender">Отправитель события</param>
        /// <param name="e">Аргументы события</param>
        protected virtual void btnOk_Click(object sender, EventArgs e)
        {
            bind();
            vm.changePassword();
            if (ChangeResult == ActionResult.OK)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }

            //if (vm.CheckResult == true)
            //{
            //    this.Close();
            //    vm.runApp();
            //}
            backBind();
        }

        private void bind()
        {
            vm.OldPassword = tbOldPassword.Text;
            vm.NewPassword = tbNewPassword.Text;
            vm.RepeatPassword = tbNewPasswordDuplicate.Text;
        }

        private void backBind()
        {
            //this.DialogResult = (vm.Result == ActionResult.OK ? DialogResult.OK : System.Windows.Forms.DialogResult.Cancel);
        }

        /// <summary>
        /// Обработка события Нажатие клавиши клавиатуры на ChangePasswordForm
        /// </summary>
        /// <param name="sender">Отправитель события</param>
        /// <param name="e">Аргументы события</param>
        protected virtual void ChangePasswordForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(sender, e);
        }
    }
}
