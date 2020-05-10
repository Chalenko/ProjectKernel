using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectKernel.Classes.Logger;

namespace ProjectKernel.Classes.Logger
{
    /// <summary>
    /// Класс, предназначенный для форматирования лога в текстовый формат 
    /// </summary>
    public sealed class TextFormatter : IFormatter
    {
        private TextFormatter() : base() { }

        private static IFormatter instance;

        /// <summary>
        /// Свойство - точка доступа класса-синглетона
        /// </summary>
        public static IFormatter Instance 
        { 
            get 
            {
                if (instance == null)
                    instance = new TextFormatter();
                return instance;
            }
        }

        /// <summary>
        /// Форматирует объект лога в текстовый формат
        /// </summary>
        /// <param name="log">Объект, содержащий информацию лога</param>
        /// <returns>Строковое представление лога</returns>
        public string format(LogData log)
        {
            return log.user.FullName + ". " + log.date.ToString() + ". " + log.level.ToString() + ": " + log.message + ((log.exception != null) ? (". \r\n" + log.exception.ToString()) : (""));
        }

        /// <summary>
        /// Предоставляет интерфейс для работы с текстовым файлом
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Интерфейс для работы с текстовым файлом</returns>
        public IWriter getWriter(string fileName)
        {
            return new TXTWriter(fileName);
        }

        private class TXTWriter : IWriter
        {
            public string FileName { get; protected set; }

            public TXTWriter(string fullFileName)
            {
                this.FileName = fullFileName;
            }

            public void createFile()
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(FileName, true);
                file.Write("");
                file.Close();
            }

            public void writeData(string text)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(FileName, true))
                {
                    file.WriteLine(text);
                    file.WriteLine("");
                    file.Close();
                }
            }
        }
    }
}
