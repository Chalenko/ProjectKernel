using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectKernel.Classes.Logger
{
    /// <summary>
    /// Определяет метод форматирования лога
    /// </summary>
    public interface IFormatter
    {
        /// <summary>
        /// Выполняет форматирование лога
        /// </summary>
        /// <param name="log">Данные лога, предназначенные для форматирования</param>
        /// <returns>Отформатированное строковое представление данных лога</returns>
        string format(LogData log);

        /// <summary>
        /// Предоставляет интерфейс работы с файлом
        /// </summary>
        /// <param name="fileName">Имя файла для работы</param>
        /// <returns>Объект для работы с файлом</returns>
        IWriter getWriter(string fileName);
    }
}
