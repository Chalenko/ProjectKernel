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
    /// Форма авторизации
    /// </summary>
    public partial class LoginForm : Form
    {
        private LoginFormViewModel vm;

        /// <summary>
        /// Результат попытки входа в систему
        /// </summary>
        public ActionResult EnterResult { get {return vm.Result; }}

        /// <summary>
        /// Инициализирует новую форму авторизации с нулевыми делеггатами функций
        /// </summary>
        protected LoginForm()
        {
            InitializeComponent();
            foreach (Control c in Controls)
                c.KeyDown += LoginForm_KeyDown;
        }

        /// <summary>
        /// Инициализирует новую форму авторизации по параметрам
        /// </summary>
        /// <param name="vm">Модель отображения для формы</param>
        public LoginForm(LoginFormViewModel vm) : this()
        {
            this.vm = vm;
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
        /// Обработка события Нажатие кнопки Вход
        /// </summary>
        /// <param name="sender">Отправитель события</param>
        /// <param name="e">Аргументы события</param>
        protected virtual void btnEnter_Click(object sender, EventArgs e)
        {
            bind();
            vm.enter();
            if (EnterResult == ActionResult.OK)
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

        /// <summary>
        /// Обработка события Нажатие клавиши клавиатуры на LoginForm
        /// </summary>
        /// <param name="sender">Отправитель события</param>
        /// <param name="e">Аргументы события</param>
        protected virtual void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
                btnEnter_Click(sender, e); 
        }

        private void bind()
        {
            vm.Login = tbLogin.Text;
            vm.Password = tbPassword.Text;
        }

        private void backBind()
        {
            //this.DialogResult = (vm.Result == ActionResult.OK ? DialogResult.OK : System.Windows.Forms.DialogResult.Cancel);
        }

        private void tbPassword_Enter(object sender, EventArgs e)
        {
            tbPassword.SelectAll();
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            tbPassword.Clear();
        }
    }
    
    //internal class DefaultLogin 
    //{
    //    public static bool check(string login, string password)
    //    {
    //        //Go to database and check
    //        //or to do somthing enother
    //        //return Convert.ToInt32(DatabaseContext.Instance.ExecuteScalar(String.Format("SELECT password FROM Users WHERE login = '{0}'", login), System.Data.CommandType.Text)) == password.GetHashCode();
    //        return true;
    //    }
    //}
}
