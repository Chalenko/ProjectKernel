using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectKernel.Classes.Logger
{
    /// <summary>
    /// Абстрактный класс для логгирования форматированных данных
    /// </summary>
    public abstract class FormattableLogger : Logger
    {
        /// <summary>
        /// Объект для форматирования данных
        /// </summary>
        private IFormatter formatter = TextFormatter.Instance;

        /// <summary>
        /// Объект для форматирования данных
        /// </summary>
        public IFormatter Formatter { get { return formatter; } protected set { if (value == null) throw new ArgumentNullException(); formatter = value; } }

        /// <summary>
        /// Запись форматированных данных в файл
        /// </summary>
        /// <param name="text">Форматированные данные</param>
        protected abstract void log(string text);
    }
}
