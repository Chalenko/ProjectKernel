using ProjectKernel.Forms.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectKernel.Forms.ViewModel
{
    /// <summary>
    /// Делегат функции-проверки пароля
    /// </summary>
    /// <param name="login">Логин пользователя</param>
    /// <param name="password">Проверяемый пароль</param>
    /// <returns>Логическое значение является ли пара логин-пароль действительной</returns>
    public delegate bool CheckPasswordDelegate(string login, string password);

    /// <summary>
    /// Делегат функции по инициализации пользователя
    /// </summary>
    /// <param name="login">Логин пользователя</param>
    public delegate ProjectKernel.Classes.User.User UserInitDelegate(string login);

    /// <summary>
    /// Результат выполнения действия
    /// </summary>
    public enum ActionResult
    {
        /// <summary>
        /// Действие завершилось удачно
        /// </summary>
        OK,
        /// <summary>
        /// Действие завершилось неудачно
        /// </summary>
        FAIL
    }

    /// <summary>
    /// View model для формы аутентификации
    /// </summary>
    public class LoginFormViewModel
    {
        private CheckPasswordDelegate check = null;
        private UserInitDelegate userInit = null;
        //private RunDelegate run = null;

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Результат аутентификации
        /// </summary>
        [System.ComponentModel.DefaultValue(ActionResult.FAIL)]
        public ActionResult Result { get; private set; }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="check">Делегат функции авторизации</param>
        public LoginFormViewModel(CheckPasswordDelegate check)
        {
            this.check = check;
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="check">Делегат функции авторизации</param>
        /// <param name="init">Делегат функции инициализации пользователя</param>
        public LoginFormViewModel(CheckPasswordDelegate check, UserInitDelegate init) : this(check)
        {
            this.userInit = init;
        }

        /// <summary>
        /// Осуществляет попытку аутентификации
        /// </summary>
        /// <returns>Результат аутентификации</returns>
        public void enter()
        {
            bool isChecked = false;

            try
            {
                isChecked = check(Login, Password);
            }
            catch (ProjectKernel.Classes.User.UserNotExistException ex)
            {
                MessageBox.Show("Wrong user login");
                Result = ActionResult.FAIL;
                return;
            }

            if (isChecked)
            {
                //view.Close();
                if (userInit != null)
                    userInit(Login);
                //ProjectKernel.Classes.User.CurrentUser.getDBInstance(Login);
                Result = ActionResult.OK;
            }
            else
            {
                MessageBox.Show("Incorrect login-password pair");
                Result = ActionResult.FAIL;
            }

        }
    }
}
