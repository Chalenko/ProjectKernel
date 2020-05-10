using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectKernel.Classes.Logger
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class LogData
    {
        /// <summary>
        /// Пользователь системы, добавляющий запись
        /// </summary>
        public User.User user { get; private set; }
        
        /// <summary>
        /// Имя компьютера с которого добавлена запись
        /// </summary>
        public string workstation { get; private set; }
        
        /// <summary>
        /// Дата и время лога
        /// </summary>
        public DateTime date { get; private set; }
        
        /// <summary>
        /// Уровень лога
        /// </summary>
        public LogLevel level { get; private set; }
        
        /// <summary>
        /// Сообщение лога
        /// </summary>
        public string message { get; private set; }
        
        /// <summary>
        /// Логгируемое исключение
        /// </summary>
        public Exception exception { get; private set; }

        private LogData() { }

        /// <summary>
        /// Инициализирует новый объект класса LogData по параметрам
        /// </summary>
        /// <param name="user">Пользователь системы, добавляющий лог</param>
        /// <param name="workstation">Имя компьютреа-источника лога</param>
        /// <param name="date">Дата и время лога</param>
        /// <param name="level">Уровень лога</param>
        /// <param name="message">Сообщение лога</param>
        /// <param name="exception">Логгируемое исключение</param>
        public LogData(User.User user, string workstation, DateTime date, LogLevel level, string message, Exception exception = null) : this()
        {
            this.user = user;
            this.workstation = workstation;
            this.date = date;
            this.level = level;
            this.message = message;
            this.exception = exception;
        }
    }
}
