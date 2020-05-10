using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Classes.Logger;
using ProjectKernel.Classes.User;
using System.Xml.Linq;

namespace KernelTest.ClassesTest
{
    [TestClass]
    public class TextFileLoggerTest
    {
        private string filePath = System.IO.Directory.GetCurrentDirectory();
        ILogger logger = FileLogger.Instance;

        public TextFileLoggerTest()
        {
            CurrentUser.getSystemInstance();
        }

        internal void runTests()
        {
            canCreateDefaultLogger();
            canCreateTextFileLogger();
            logFunctionCreateDirectoryLogFile();
            logFunctionCreateTXTLogFile();
            logFunctionClearLogFileIfLengthMoreThen5Mb();
            logMessageIntoDefaultLogger();
            logMessage();
            logMessageAndException();
            debugMessage();
            debugMessageAndException();
            infoMessage();
            infoMessageAndException();
            warnMessage();
            warnMessageAndException();
            errorMessage();
            errorMessageAndException();
            fatalMessage();
            fatalMessageAndException();
        }

        [TestMethod]
        public void canCreateDefaultLogger()
        {
            Assert.IsNotNull(logger);
        }

        [TestMethod]
        public void canCreateTextFileLogger()
        {
            Assert.IsNotNull(FileLogger.getInstance("C:\\UIT", "log.txt", TextFormatter.Instance));
        }

        [TestMethod]
        public void logFunctionCreateTXTLogFile()
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(filePath + "\\log.txt");
            fi.Delete();
            
            logger.log(LogLevel.Debug, "First record after delete file");

            Assert.IsTrue(fi.Exists);
        }

        [TestMethod]
        public void logFunctionCreateDirectoryLogFile()
        {
            FileLogger.getInstance(filePath + "\\tmpForTest", "log.txt", TextFormatter.Instance).log(LogLevel.Debug, "First record after delete file");

            System.IO.FileInfo fi = new System.IO.FileInfo(filePath + "\\tmpForTest" + "\\log.txt");
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(filePath + "\\tmpForTest");
            Assert.IsTrue(di.Exists);
            Assert.IsTrue(fi.Exists);

            #region undo

            di.Delete(true);

            #endregion
        }

        [TestMethod]
        public void logFunctionClearLogFileIfLengthMoreThen5Mb()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath + "\\log.txt", true))
            {
                for (int i = 0; i < 500000; i++)
                {
                    file.WriteLine(i.ToString() + " line.");
                    file.WriteLine("");
                }
                file.Close();
            }

            logger.log(LogLevel.Debug, "First record after clear file");

            Assert.IsTrue(new System.IO.FileInfo(filePath + "\\log.txt").Length < 5000000);
        }

        [TestMethod]
        public void logMessageIntoDefaultLogger()
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(filePath + "\\log.txt");
            string text = "Log function is OK";
            string expectedValue = CurrentUser.Instance.FullName + ". " + DateTime.Now.ToString() + ". " + LogLevel.Debug.ToString() + ": " + text;
            logger.log(LogLevel.Debug, text);

            string actualValue = "";
            using (System.IO.StreamReader file = new System.IO.StreamReader(filePath + "\\log.txt", true))
            {
                while (!file.EndOfStream)
                {
                    actualValue = file.ReadLine();
                    file.ReadLine();
                }
                    
                file.Close();
            }

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void logMessage()
        {
            string path = "C:\\UIT";
            System.IO.FileInfo fi = new System.IO.FileInfo(path + "\\log.txt");
            string text = "Log function is OK";
            string expectedValue = CurrentUser.Instance.FullName + ". " + DateTime.Now.ToString() + ". " + LogLevel.Debug.ToString() + ": " + text;
            FileLogger.getInstance(path, "log.txt", TextFormatter.Instance).log(LogLevel.Debug, text);

            string actualValue = "";
            using (System.IO.StreamReader file = new System.IO.StreamReader(path + "\\log.txt", true))
            {
                while (!file.EndOfStream)
                {
                    actualValue = file.ReadLine();
                    file.ReadLine();
                }

                file.Close();
            }

            Assert.AreEqual(expectedValue, actualValue);
            fi.Delete();
        }

        [TestMethod]
        public void logMessageAndException()
        {
            string path = "C:\\UIT";
            System.IO.FileInfo fi = new System.IO.FileInfo(path + "\\log.txt");
            string text = "Log function is OK. Exception is also OK";
            DebugException ex = new DebugException("Someone divide 1 by 0");
            string expectedValue = CurrentUser.Instance.FullName + ". " + DateTime.Now.ToString() + ". " + LogLevel.Debug.ToString() + ": " + text + ". \r\n" + ex.ToString() + "\r\n";
            FileLogger.getInstance(path, "log.txt", TextFormatter.Instance).log(LogLevel.Debug, text, ex);

            string actualValue = "";
            using (System.IO.StreamReader file = new System.IO.StreamReader(path + "\\log.txt", true))
            {
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    actualValue = line;
                    while (line != "")
                    {
                        line = file.ReadLine();
                        actualValue += "\r\n" + line;
                    }
                }

                file.Close();
            }

            fi.Delete();
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void debugMessage()
        {
            string path = "C:\\UIT";
            System.IO.FileInfo fi = new System.IO.FileInfo(path + "\\log.txt");
            string text = "Log function is OK";
            string expectedValue = CurrentUser.Instance.FullName + ". " + DateTime.Now.ToString() + ". " + LogLevel.Debug.ToString() + ": " + text;
            FileLogger.getInstance(path, "log.txt", TextFormatter.Instance).debug(text);

            string actualValue = "";
            using (System.IO.StreamReader file = new System.IO.StreamReader(path + "\\log.txt", true))
            {
                while (!file.EndOfStream)
                {
                    actualValue = file.ReadLine();
                    file.ReadLine();
                }

                file.Close();
            }

            Assert.AreEqual(expectedValue, actualValue);
            fi.Delete();
        }

        [TestMethod]
        public void debugMessageAndException()
        {
            string path = "C:\\UIT";
            System.IO.FileInfo fi = new System.IO.FileInfo(path + "\\log.txt");
            string text = "Log function is OK. Exception is also OK";
            DebugException ex = new DebugException("Someone divide 1 by 0");
            string expectedValue = CurrentUser.Instance.FullName + ". " + DateTime.Now.ToString() + ". " + LogLevel.Debug.ToString() + ": " + text + ". \r\n" + ex.ToString() + "\r\n";
            FileLogger.getInstance(path, "log.txt", TextFormatter.Instance).debug(text, ex);

            string actualValue = "";
            using (System.IO.StreamReader file = new System.IO.StreamReader(path + "\\log.txt", true))
            {
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    actualValue = line;
                    while (line != "")
                    {
                        line = file.ReadLine();
                        actualValue += "\r\n" + line;
                    }
                }

                file.Close();
            }

            fi.Delete();
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void infoMessage()
        {
            string path = "C:\\UIT";
            System.IO.FileInfo fi = new System.IO.FileInfo(path + "\\log.txt");
            string text = "Log function is OK";
            string expectedValue = CurrentUser.Instance.FullName + ". " + DateTime.Now.ToString() + ". " + LogLevel.Info.ToString() + ": " + text;
            FileLogger.getInstance(path, "log.txt", TextFormatter.Instance).info(text);

            string actualValue = "";
            using (System.IO.StreamReader file = new System.IO.StreamReader(path + "\\log.txt", true))
            {
                while (!file.EndOfStream)
                {
                    actualValue = file.ReadLine();
                    file.ReadLine();
                }

                file.Close();
            }

            Assert.AreEqual(expectedValue, actualValue);
            fi.Delete();
        }

        [TestMethod]
        public void infoMessageAndException()
        {
            string path = "C:\\UIT";
            System.IO.FileInfo fi = new System.IO.FileInfo(path + "\\log.txt");
            string text = "Log function is OK. Exception is also OK";
            DebugException ex = new DebugException("Someone divide 1 by 0");
            string expectedValue = CurrentUser.Instance.FullName + ". " + DateTime.Now.ToString() + ". " + LogLevel.Info.ToString() + ": " + text + ". \r\n" + ex.ToString() + "\r\n";
            FileLogger.getInstance(path, "log.txt", TextFormatter.Instance).info(text, ex);

            string actualValue = "";
            using (System.IO.StreamReader file = new System.IO.StreamReader(path + "\\log.txt", true))
            {
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    actualValue = line;
                    while (line != "")
                    {
                        line = file.ReadLine();
                        actualValue += "\r\n" + line;
                    }
                }

                file.Close();
            }

            fi.Delete();
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void warnMessage()
        {
            string path = "C:\\UIT";
            System.IO.FileInfo fi = new System.IO.FileInfo(path + "\\log.txt");
            string text = "Log function is OK";
            string expectedValue = CurrentUser.Instance.FullName + ". " + DateTime.Now.ToString() + ". " + LogLevel.Warn.ToString() + ": " + text;
            FileLogger.getInstance(path, "log.txt", TextFormatter.Instance).warn(text);

            string actualValue = "";
            using (System.IO.StreamReader file = new System.IO.StreamReader(path + "\\log.txt", true))
            {
                while (!file.EndOfStream)
                {
                    actualValue = file.ReadLine();
                    file.ReadLine();
                }

                file.Close();
            }

            Assert.AreEqual(expectedValue, actualValue);
            fi.Delete();
        }

        [TestMethod]
        public void warnMessageAndException()
        {
            string path = "C:\\UIT";
            System.IO.FileInfo fi = new System.IO.FileInfo(path + "\\log.txt");
            string text = "Log function is OK. Exception is also OK";
            DebugException ex = new DebugException("Someone divide 1 by 0");
            string expectedValue = CurrentUser.Instance.FullName + ". " + DateTime.Now.ToString() + ". " + LogLevel.Warn.ToString() + ": " + text + ". \r\n" + ex.ToString() + "\r\n";
            FileLogger.getInstance(path, "log.txt", TextFormatter.Instance).warn(text, ex);

            string actualValue = "";
            using (System.IO.StreamReader file = new System.IO.StreamReader(path + "\\log.txt", true))
            {
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    actualValue = line;
                    while (line != "")
                    {
                        line = file.ReadLine();
                        actualValue += "\r\n" + line;
                    }
                }

                file.Close();
            }

            fi.Delete();
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void errorMessage()
        {
            string path = "C:\\UIT";
            System.IO.FileInfo fi = new System.IO.FileInfo(path + "\\log.txt");
            string text = "Log function is OK";
            string expectedValue = CurrentUser.Instance.FullName + ". " + DateTime.Now.ToString() + ". " + LogLevel.Error.ToString() + ": " + text;
            FileLogger.getInstance(path, "log.txt", TextFormatter.Instance).error(text);

            string actualValue = "";
            using (System.IO.StreamReader file = new System.IO.StreamReader(path + "\\log.txt", true))
            {
                while (!file.EndOfStream)
                {
                    actualValue = file.ReadLine();
                    file.ReadLine();
                }

                file.Close();
            }

            Assert.AreEqual(expectedValue, actualValue);
            fi.Delete();
        }

        [TestMethod]
        public void errorMessageAndException()
        {
            string path = "C:\\UIT";
            System.IO.FileInfo fi = new System.IO.FileInfo(path + "\\log.txt");
            string text = "Log function is OK. Exception is also OK";
            DebugException ex = new DebugException("Someone divide 1 by 0");
            string expectedValue = CurrentUser.Instance.FullName + ". " + DateTime.Now.ToString() + ". " + LogLevel.Error.ToString() + ": " + text + ". \r\n" + ex.ToString() + "\r\n";
            FileLogger.getInstance(path, "log.txt", TextFormatter.Instance).error(text, ex);

            string actualValue = "";
            using (System.IO.StreamReader file = new System.IO.StreamReader(path + "\\log.txt", true))
            {
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    actualValue = line;
                    while (line != "")
                    {
                        line = file.ReadLine();
                        actualValue += "\r\n" + line;
                    }
                }

                file.Close();
            }

            fi.Delete();
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void fatalMessage()
        {
            string path = "C:\\UIT";
            System.IO.FileInfo fi = new System.IO.FileInfo(path + "\\log.txt");
            string text = "Log function is OK";
            string expectedValue = CurrentUser.Instance.FullName + ". " + DateTime.Now.ToString() + ". " + LogLevel.Fatal.ToString() + ": " + text;
            FileLogger.getInstance(path, "log.txt", TextFormatter.Instance).fatal(text);

            string actualValue = "";
            using (System.IO.StreamReader file = new System.IO.StreamReader(path + "\\log.txt", true))
            {
                while (!file.EndOfStream)
                {
                    actualValue = file.ReadLine();
                    file.ReadLine();
                }

                file.Close();
            }

            Assert.AreEqual(expectedValue, actualValue);
            fi.Delete();
        }

        [TestMethod]
        public void fatalMessageAndException()
        {
            string path = "C:\\UIT";
            System.IO.FileInfo fi = new System.IO.FileInfo(path + "\\log.txt");
            string text = "Log function is OK. Exception is also OK";
            DebugException ex = new DebugException("Someone divide 1 by 0");
            string expectedValue = CurrentUser.Instance.FullName + ". " + DateTime.Now.ToString() + ". " + LogLevel.Fatal.ToString() + ": " + text + ". \r\n" + ex.ToString() + "\r\n";
            FileLogger.getInstance(path, "log.txt", TextFormatter.Instance).fatal(text, ex);

            string actualValue = "";
            using (System.IO.StreamReader file = new System.IO.StreamReader(path + "\\log.txt", true))
            {
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    actualValue = line;
                    while (line != "")
                    {
                        line = file.ReadLine();
                        actualValue += "\r\n" + line;
                    }
                }

                file.Close();
            }

            fi.Delete();
            Assert.AreEqual(expectedValue, actualValue);
        }
    }


    [TestClass]
    public class XMLFileLoggerTest
    {
        private string filePath = System.IO.Directory.GetCurrentDirectory();
        ILogger logger;

        public XMLFileLoggerTest()
        {
            CurrentUser.getSystemInstance();
            logger = FileLogger.getInstance(filePath, "log.xml", XMLFormatter.Instance);
        }

        internal void runTests()
        {
            canCreateXMLFileLogger();
            logFunctionCreateXMLLogFile();
            logMessage();
            logMessageAndException();
        }

        [TestMethod]
        public void canCreateXMLFileLogger()
        {
            Assert.IsNotNull(logger);
        }

        [TestMethod]
        public void logFunctionCreateXMLLogFile()
        {
            string path = "C:\\UIT";
            string file = "log.xml";
            System.IO.FileInfo fi = new System.IO.FileInfo(path + "\\" + file);
            fi.Delete();

            FileLogger.getInstance(path, file, XMLFormatter.Instance).log(LogLevel.Debug, "First record after delete file");

            Assert.IsTrue(fi.Exists);
            fi.Delete();
        }

        [TestMethod]
        public void logMessage()
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(filePath + "\\log.xml");
            string message = "Log function is OK";
            string expectedValue = String.Format("<Log>\r\n  <User>{0}</User>\r\n  <Date>{1}</Date>\r\n  <Level>{2}</Level>\r\n  <Message>{3}</Message>\r\n</Log>", CurrentUser.Instance.FullName, DateTime.Now.ToString(), LogLevel.Debug.ToString(), message);

            logger.log(LogLevel.Debug, message);

            string actualValue = "";
            XDocument xDoc = XDocument.Load(filePath + "\\log.xml");
            actualValue = xDoc.Root.LastNode.ToString();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void logMessageAndException()
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(filePath + "\\log.xml");
            string message = "Log function is OK. Exception is also OK";
            string exMess = "Exception is OK";
            DebugException ex = new DebugException(exMess);
            string expectedValue = String.Format("<Log>\r\n  <User>{0}</User>\r\n  <Date>{1}</Date>\r\n  <Level>{2}</Level>\r\n  <Message>{3}</Message>\r\n  <Exception Message=\"{4}\">\r\n    <Type>{5}</Type>\r\n    <StackTrace>{6}</StackTrace>\r\n  </Exception>\r\n</Log>", CurrentUser.Instance.FullName, DateTime.Now.ToString(), LogLevel.Debug.ToString(), message, exMess, ex.GetType(), ex.StackTrace);
            logger.log(LogLevel.Debug, message, ex);

            string actualValue = "";
            XDocument xDoc = XDocument.Load(filePath + "\\log.xml");
            actualValue = xDoc.Root.LastNode.ToString();

            //fi.Delete();
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void getInstanceThrowExceptionWhenAnotherFormatAlreadyRegistredForFile()
        {
            //arrange
            try
            {
                //act
                FileLogger.getInstance(filePath, "log.xml", TextFormatter.Instance);
            }
            catch(InvalidOperationException ex)
            {
                //assert
                Assert.AreEqual("Операция является недопустимой из-за текущего состояния объекта.", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void getInstanceAllowGetFileLoggerWithAlreadyRegistredFormatterForFile()
        {
            //arrange
            try
            {
                //act
                FileLogger.getInstance(filePath, "log.xml", XMLFormatter.Instance);
            }
            catch
            {
                Assert.Fail();
                return;
            }
            //assert
        }
    }
}
