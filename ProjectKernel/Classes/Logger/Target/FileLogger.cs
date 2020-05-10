using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectKernel.Classes.User;
using System.Xml.Linq;

namespace ProjectKernel.Classes.Logger
{
    /// <summary>
    /// Класс для логиирования в файл.
    /// </summary>
    public sealed class FileLogger : FormattableLogger
    {
        private int maxFileSize = 5000000;
        private string filePath = System.IO.Directory.GetCurrentDirectory();//System.Windows.Forms.Application.StartupPath.ToString();
        private string fileName = "log.txt";//System.Windows.Forms.Application.StartupPath.ToString();
        private IWriter writer;
        private static Dictionary<string, FileLogger> loggers = new Dictionary<string, FileLogger>();//FileLogger currentUser = new FileLogger(System.Windows.Forms.Application.StartupPath.ToString());

        /// <summary>
        /// Максимальный размер лог-файла
        /// </summary>
        public int MaxFileSize {
            get
            {
                return maxFileSize;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentException();
                maxFileSize = value;
            }
        }

        /// <summary>
        /// Свойство для предоставления в окружение стандартного объекта-логгера
        /// </summary>
        public static ILogger Instance { get { return getInstance(); } }

        /// <summary>
        /// Метод для предоставления в окружение стандартного объекта-логгера
        /// </summary>
        /// <returns>Объект-логгер</returns>
        public static ILogger getInstance()
        {
            return getInstance(System.IO.Directory.GetCurrentDirectory(), "log.txt", TextFormatter.Instance);
        }

        /// <summary>
        /// Метод для предоставления в программу объекта-логгера по параметрам
        /// </summary>
        /// <param name="filePath">Путь к файлу логов</param>
        /// <param name="fileName">Имя файла логов</param>
        /// <param name="formatter">Экземпляр класса, предназначенного для форматирования лога</param>
        /// <returns>Объект-логгер</returns>
        /// <remarks>На один файл можно создать только один логгер. После первого обращения к логгеру изменение типа форматирования не возможно</remarks>
        public static ILogger getInstance(string filePath, string fileName, IFormatter formatter)
        {
            string fullName = filePath + "\\" + fileName;
            if (!loggers.ContainsKey(fullName))
                loggers.Add(fullName, new FileLogger(filePath, fileName, formatter));
            else
            {
                if (loggers[fullName].Formatter.GetType() != formatter.GetType())
                    throw new InvalidOperationException();
            }
                
            return loggers[fullName];
        }

        private FileLogger() { }

        private FileLogger(string path, string name, IFormatter formatter) : this()
        {
            this.filePath = path;
            this.fileName = name;
            this.Formatter = formatter;
            this.writer = Formatter.getWriter(filePath + "\\" + fileName);
        }

        /// <summary>
        /// Запись форматированных данных в файл
        /// </summary>
        /// <param name="text">Форматированные данные</param>
        protected override void log(string text)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(filePath + "\\" + fileName);
            if (!fi.Directory.Exists) System.IO.Directory.CreateDirectory(filePath);

            if (fi.Exists && fi.Length > MaxFileSize) //not more then maxFileSize Mb
            {
                fi.Delete();
            }

            if (!fi.Exists)
            {
                writer.createFile();
            }

            writer.writeData(text);

            //Formatter.writeData(this, text);
        }

        /// <summary>
        /// Запись форматированных данных в файл
        /// </summary>
        /// <param name="log"><inheritdoc/></param>
        public override void log(LogData log)
        {
            this.log(Formatter.format(log));
        }
    }
}
