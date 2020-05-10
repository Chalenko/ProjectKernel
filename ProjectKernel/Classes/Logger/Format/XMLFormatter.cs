using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ProjectKernel.Classes.Logger
{
    /// <summary>
    /// Класс, предназначенный для форматирования лога в xml-формат  
    /// </summary>
    public sealed class XMLFormatter : IFormatter
    {
        private static IFormatter instance;

        private XMLFormatter() : base() { }

        /// <summary>
        ///  Свойство - точка доступа класса-синглетона
        /// </summary>
        public static IFormatter Instance
        {
            get
            {
                if (instance == null)
                    instance = new XMLFormatter();
                return instance;
            }
        }

        /// <summary>
        /// Форматирует объект лога в xml-формат
        /// </summary>
        /// <param name="log">Объект, содержащий информацию лога</param>
        /// <returns>Строковое представление лога в xml-формате</returns>
        public string format(LogData log)
        {
            XElement logElement = new XElement("Log", "");
            XElement userElement = new XElement("User", log.user.FullName);
            XElement dateElement = new XElement("Date", log.date.ToString());
            XElement levelElement = new XElement("Level", log.level.ToString());
            XElement messageElement = new XElement("Message", log.message);
            logElement.Add(userElement); logElement.Add(dateElement); logElement.Add(levelElement); logElement.Add(messageElement);

            if (log.exception != null)
            {
                XElement exceptionElement = new XElement("Exception");
                exceptionElement.SetAttributeValue("Message", log.exception.Message);
                XElement exceptionTypeElement = new XElement("Type", log.exception.GetType());
                XElement exceptionTraceElement = new XElement("StackTrace", (log.exception.StackTrace != null) ? log.exception.StackTrace : "");
                exceptionElement.Add(exceptionTypeElement); exceptionElement.Add(exceptionTraceElement);
                logElement.Add(exceptionElement);
            }

            return logElement.ToString();
        }

        /// <summary>
        /// Предоставляет интерфейс для работы с xml файлом
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Интерфейс для работы с xml файлом</returns>
        public IWriter getWriter(string fileName)
        {
            return new XMLWriter(fileName);
        }

        private class XMLWriter : IWriter
        {
            public string FileName { get; protected set; }

            public XMLWriter(string fullFileName)
            {
                this.FileName = fullFileName;
            }

            public void createFile()
            {
                XDocument xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Logs"));
                xDoc.Save(FileName);
            }

            public void writeData(string text)
            {
                XDocument xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Logs"));
                xDoc = XDocument.Load(FileName);
                XElement el = XElement.Parse(text);
                xDoc.Root.Add(el);
                xDoc.Save(FileName);
            }
        }
    }
}
