using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectKernel.Classes
{
    /// <summary>
    /// Клас для котроля входа и выхода пользователя из системы
    /// </summary>
    public static class Activity
    {
        /// <summary>
        /// Отметить вход в систему
        /// </summary>
        public static void logIn()
        {
            string queryActivity = String.Format("UPDATE Activity SET" +
                " [state] = 'logIn'" +
                ",[last_login_datetime] = GETDATE()" +
                ",[last_login_workstation] = '{0}'" +
                "WHERE [user_login] = '{1}';", Environment.MachineName, Classes.User.CurrentUser.Instance.Login.ToString());

            DatabaseContext.Instance.ExecuteCommand(DatabaseContext.Instance.CreateCommand(queryActivity, System.Data.CommandType.Text));
        }

        /// <summary>
        /// Отметить выход из системы
        /// </summary>
        public static void logOut()
        {
            string queryActivity = String.Format("UPDATE Activity SET" +
                " [state] = 'logOut'" +
                ",[last_login_workstation] = '{0}'" +
                ",[last_logout_datetime] = GETDATE()" +
                "WHERE [user_login] = '{1}';", Environment.MachineName, Classes.User.CurrentUser.Instance.Login.ToString());

            DatabaseContext.Instance.ExecuteCommand(DatabaseContext.Instance.CreateCommand(queryActivity, System.Data.CommandType.Text));
        }
    }
}
