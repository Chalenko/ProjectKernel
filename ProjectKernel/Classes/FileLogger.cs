using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectKernel.Classes
{
    public enum LogLevel
    {
        Debug, Info, Warn, Error, Fatal
    }

    public interface ILogger
    {
        void log(LogLevel lvl, string text);
    }

    public class FileLogger : ILogger
    {

        private string filePath = System.Windows.Forms.Application.StartupPath.ToString();
        private static Dictionary<string, FileLogger> loggers = new Dictionary<string, FileLogger>();//FileLogger currentUser = new FileLogger(System.Windows.Forms.Application.StartupPath.ToString());
        private FileLogger(string path)
        {
            // TODO: Complete member initialization
            this.filePath = path;
        }
        
        public static ILogger Instance(string path)
        {
            if (!loggers.ContainsKey(path)) loggers.Add(path, new FileLogger(path));
            return loggers[path];
        }
        public void log(LogLevel lvl, string text)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(filePath + "\\log.txt");
            if (!fi.Directory.Exists) System.IO.Directory.CreateDirectory(filePath);

            if (fi.Exists && fi.Length > 5000000) //not more then 5Mb
            {
                clearLogFile();
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath + "\\log.txt", true))
            {
                try
                {
                    file.WriteLine(SystemUser.Instance.FullName + ". " + DateTime.Now.ToString() + ". " + lvl.ToString() + ": " + text);
                    file.WriteLine("");
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                }
                finally
                {
                    file.Close();
                }
            }
        }

        private void clearLogFile()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath + "\\log.txt", false))
            {
                try
                {
                    file.Write("");
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                }
                finally
                {
                    file.Close();
                }
            }
        }
    }
}
