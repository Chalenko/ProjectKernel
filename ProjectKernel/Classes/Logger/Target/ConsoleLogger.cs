using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectKernel.Classes.Logger
{
    /// <summary>
    /// Класс логиирования событий в стандартную консоль ввода-вывода
    /// </summary>
    public sealed class ConsoleLogger : FormattableLogger
    {
        private static ConsoleLogger currentLogger = null; //= new ConsoleLogger();

        /// <summary>
        /// Свойство, предоставляющее в окружающую среду стандартный объект класса ConsoleLogger
        /// </summary>
        public static ILogger Instance { get { return getInstance(TextFormatter.Instance); } }

        /// <summary>
        /// Метод, предоствляющий в окружающую среду объект класса ConsoleLogger
        /// </summary>
        /// <param name="formatter">Экземпляр класса, предназначенного для форматирования лога</param>
        /// <returns>Объект-логгер</returns>
        public static ILogger getInstance(IFormatter formatter)
        {
            if (currentLogger == null) currentLogger = new ConsoleLogger(formatter);
            return currentLogger;
        }

        private ConsoleLogger() { }

        private ConsoleLogger(IFormatter formatter) : this()
        {
            this.Formatter = formatter;
        }

        /// <summary>
        /// Запись форматированных данных в стандартную консоль ввода-вывода
        /// </summary>
        /// <param name="text">Форматированные данные</param>
        protected override void log(string text)
        {
            System.Console.WriteLine(text);
        }

        /// <summary>
        /// Запись форматированных данных в стандартный поток ввода-вывода
        /// </summary>
        /// <param name="logData"><inheritdoc/></param>
        public override void log(LogData logData)
        {
            this.log(Formatter.format(logData));
        }
    }
}
