using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectKernel.Classes.Logger
{
    /// <summary>
    /// Объект для логгирования данных
    /// </summary>
    public abstract class Logger : ILogger
    {
        /// <summary>
        /// Запись данных в хранилище логов
        /// </summary>
        /// <param name="logData">Данные для логгирования</param>
        public abstract void log(LogData logData);

        /// <inheritdoc/>
        public void log(LogLevel lvl, string text)
        {
            this.log(new LogData(User.CurrentUser.Instance, Environment.MachineName, DateTime.Now, lvl, text, null));
        }

        /// <inheritdoc/>
        public void log(LogLevel lvl, string text, Exception ex)
        {
            this.log(new LogData(User.CurrentUser.Instance, Environment.MachineName, DateTime.Now, lvl, text, ex));
        }

        /// <inheritdoc/>
        public void debug(string text) { log(LogLevel.Debug, text); }

        /// <inheritdoc/>
        public void debug(string text, Exception ex) { log(LogLevel.Debug, text, ex); }

        /// <inheritdoc/>
        public void info(string text) { log(LogLevel.Info, text); }

        /// <inheritdoc/>
        public void info(string text, Exception ex) { log(LogLevel.Info, text, ex); }

        /// <inheritdoc/>
        public void warn(string text) { log(LogLevel.Warn, text); }

        /// <inheritdoc/>
        public void warn(string text, Exception ex) { log(LogLevel.Warn, text, ex); }

        /// <inheritdoc/>
        public void error(string text) { log(LogLevel.Error, text); }

        /// <inheritdoc/>
        public void error(string text, Exception ex) { log(LogLevel.Error, text, ex); }

        /// <inheritdoc/>
        public void fatal(string text) { log(LogLevel.Fatal, text); }

        /// <inheritdoc/>
        public void fatal(string text, Exception ex) { log(LogLevel.Fatal, text, ex); }
    }
}
