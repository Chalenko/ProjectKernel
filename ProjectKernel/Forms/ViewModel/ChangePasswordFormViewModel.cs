using ProjectKernel.Classes.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectKernel.Forms.ViewModel
{
    /// <summary>
    /// Делегат функции операции изменения пароля
    /// </summary>
    /// <param name="user">Объект сущности пользователя системы</param>
    /// <param name="password">Новый пароль</param>
    public delegate void ChangePasswordDelegate(ProjectKernel.Classes.User.DBUser user, string password);

    /// <summary>
    /// View model для формы смены пароля
    /// </summary>
    public class ChangePasswordFormViewModel
    {
        private DBUser user = null;
        private CheckPasswordDelegate check = null;
        private ChangePasswordDelegate change = null;

        /// <summary>
        /// Старый пароль
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// Новый пароль
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Повтор нового пароля
        /// </summary>
        public string RepeatPassword { get; set; }

        /// <summary>
        /// Результат смены пароля
        /// </summary>
        [System.ComponentModel.DefaultValue(ActionResult.FAIL)]
        public ActionResult Result { get; private set; }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="user">Объект пользователя системы</param>
        /// <param name="check">Делегат функции авторизации</param>
        /// <param name="change">Делегат функции смены пароля</param>
        public ChangePasswordFormViewModel(DBUser user, CheckPasswordDelegate check, ChangePasswordDelegate change)
        {
            this.user = user;
            this.check = check;
            this.change = change;
        }

        /// <summary>
        /// Осуществляет попытку смены пароля
        /// </summary>
        /// <returns>Результат аутентификации</returns>
        public void changePassword()
        {
            if (NewPassword != RepeatPassword)
            {
                MessageBox.Show("Введеные пароли не совпадают");
                Result = ActionResult.FAIL;
                return;
            }

            if (OldPassword == NewPassword)
            {
                MessageBox.Show("Новый пароль не должен совпадать со старым");
                Result = ActionResult.FAIL;
                return;
            }

            bool isChecked = false;
            try
            {
                isChecked = check(user.Login, OldPassword);
            }
            catch (ProjectKernel.Classes.User.UserNotExistException ex)
            {
                MessageBox.Show("Неверное имя пользователя");
                Result = ActionResult.FAIL;
                return;
            }

            if (isChecked)
            {
                //view.Close();
                if (change != null)
                    change(user, NewPassword);
                //ProjectKernel.Classes.User.CurrentUser.getDBInstance(Login);
                Result = ActionResult.OK;
            }
            else
            {
                MessageBox.Show("Неверный старый пароль");
                Result = ActionResult.FAIL;
            }
        }
    }
}
