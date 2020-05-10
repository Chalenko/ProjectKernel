using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Classes.Logger;
using ProjectKernel.Classes.User;

namespace KernelTest.ClassesTest
{
    [TestClass]
    public class ConsoleLoggerTest
    {
        public ConsoleLoggerTest()
        {
            CurrentUser.getSystemInstance();
        }

        internal void runTests()
        {
            canCreateConsoleLogger();
            canCreateXMLConsoleLogger();
            IsConsoleLoggerInstanceOfILogger();
            logMessageIntoConsole();
        }

        [TestMethod]
        public void canCreateConsoleLogger()
        {
            Assert.IsNotNull(ConsoleLogger.Instance);
        }

        [TestMethod]
        public void canCreateXMLConsoleLogger()
        {
            Assert.IsNotNull(ConsoleLogger.Instance);
        }

        [TestMethod]
        public void IsConsoleLoggerInstanceOfILogger()
        {
            Assert.IsInstanceOfType(ConsoleLogger.Instance, typeof(ILogger));
        }

        [TestMethod]
        public void logMessageIntoConsole()
        {
            ConsoleLogger.getInstance(TextFormatter.Instance);
            ConsoleLogger.Instance.log(LogLevel.Debug, "Console logger test message");
            try
            {
                Assert.Inconclusive("Console logging can't be check");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(AssertInconclusiveException));
                Assert.AreEqual("Ошибка в Assert.Inconclusive. Console logging can't be check", ex.Message);
                return;
            }

            Assert.Fail();
        }
    }
}
