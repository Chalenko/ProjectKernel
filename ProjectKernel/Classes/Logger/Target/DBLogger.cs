using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectKernel.Classes.Logger
{
    /// <summary>
    /// Класс для логиирования в таблицу Log в БД.
    /// </summary>
    public sealed class DBLogger : Logger
    {
        private DatabaseContext context = DatabaseContext.Instance;
        private static Dictionary<string, ILogger> loggers = new Dictionary<string, ILogger>();

        /// <summary>
        /// Свойство для предостовления в окружение стандартного объекта-логгера по стандартному контексту
        /// </summary>
        public static ILogger Instance { get { return getInstance(); } }

        /// <summary>
        /// Метод для предостовления в программу объекта-логгера по параметрам
        /// </summary>
        /// <param name="context">Контекст для работы с БД</param>
        /// <returns>Объект-логгер</returns>
        public static ILogger getInstance(DatabaseContext context)
        {
            if (!loggers.ContainsKey(context.ToString())) 
                loggers.Add(context.ToString(), new DBLogger(context));
            return loggers[context.ToString()];
        }

        /// <summary>
        /// Метод для предостовления в программу объекта-логгера по параметрам
        /// </summary>
        /// <param name="connection">Строка подключения к БД</param>
        /// <returns>Объект-логгер</returns>
        public static ILogger getInstance(string connection)
        {
            return getInstance(DatabaseContext.getInstance(connection));
        }

        /// <summary>
        /// Метод для предостовления в окружение стандартного объекта-логгера
        /// </summary>
        /// <returns>Стандартный объект-логгер</returns>
        public static ILogger getInstance()
        {
            return getInstance(DatabaseContext.Instance);
        }

        private DBLogger() { }

        private DBLogger(DatabaseContext context) : this()
        {
            this.context = context;
        }

        /// <summary>
        /// Запись данных в таблицу Log
        /// </summary>
        /// <param name="logData"><inheritdoc/></param>
        public override void log(LogData logData)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@type", logData.user.Type.ToString());
            dic.Add("@user_login", logData.user.Login);
            dic.Add("@workstation", logData.workstation);
            dic.Add("@level", logData.level.ToString());
            dic.Add("@message", logData.message);
            dic.Add("@exception", (logData.exception != null) ? (object)(logData.exception.ToString()) : (object)(DBNull.Value));
            string query = "INSERT INTO [dbo].[Log] VALUES (@type ,@user_login ,@workstation ,getdate() ,@level , @message, @exception) " + 
                            "SELECT @@identity";
            DatabaseContext.Instance.ExecuteScalar(query, System.Data.CommandType.Text, dic);
        }
    }
}
