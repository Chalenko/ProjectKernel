using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectKernel.Classes.Logger
{
    /// <summary>
    /// Интерфейс для работы с файлом
    /// </summary>
    public interface IWriter
    {
        /// <summary>
        /// Создаёт файл
        /// </summary>
        void createFile();

        /// <summary>
        /// Записывает данные в файл
        /// </summary>
        /// <param name="text">Данные для записи</param>
        void writeData(string text);
    }
}
