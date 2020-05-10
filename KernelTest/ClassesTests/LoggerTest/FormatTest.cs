using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Classes.User;
using ProjectKernel.Classes.Logger;

namespace KernelTest.ClassesTest
{
    [TestClass]
    public class FormatTest
    {
        internal void runTests()
        {
            new TextFormatTest().runTests();
            new XMLFormatTest().runTests();
        }
    }

    [TestClass]
    public class TextFormatTest
    {
        IFormatter formatter;
        DateTime dateTimeNow;

        public TextFormatTest()
        {
            formatter = TextFormatter.Instance;
            dateTimeNow = DateTime.Now;
        }

        public void runTests()
        {
            canCreateTextFormatter();
            canFormatValidLog();
            canFormatValidLogWithException();
            formatLogThrowExceptionWhenUserIsNull();
        }

        [TestMethod]
        public void canCreateTextFormatter()
        {
            Assert.IsNotNull(formatter);
        }

        [TestMethod]
        public void canFormatValidLog()
        {
            string message = "All is OK";
            string expectedText = CurrentUser.Instance.FullName + ". " + dateTimeNow.ToString() + ". " + LogLevel.Debug.ToString() + ": " + message;
            Assert.AreEqual(expectedText, formatter.format(new LogData(CurrentUser.Instance, Environment.MachineName, dateTimeNow, LogLevel.Debug, message, null)));
        }

        [TestMethod]
        public void canFormatValidLogWithException()
        {
            string message = "All is OK";
            string exMess = "Exception is OK";
            Exception ex = new DebugException(exMess);
            string expectedText = CurrentUser.Instance.FullName + ". " + dateTimeNow.ToString() + ". " + LogLevel.Debug.ToString() + ": " + message + ". \r\n" + ex.ToString();
            Assert.AreEqual(expectedText, formatter.format(new LogData(CurrentUser.Instance, Environment.MachineName, dateTimeNow, LogLevel.Debug, message, ex)));
        }

        [TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        public void formatLogThrowExceptionWhenUserIsNull()
        {
            string message = "All is OK";
            string expectedText = CurrentUser.Instance.FullName + ". " + dateTimeNow.ToString() + ". " + LogLevel.Debug.ToString() + ": " + message;
            try
            {
                formatter.format(new LogData(null, Environment.MachineName, dateTimeNow, LogLevel.Debug, message, new DebugException("Exception is OK")));
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(NullReferenceException));
                Assert.AreEqual("Ссылка на объект не указывает на экземпляр объекта.", ex.Message);
                return;
            }
            Assert.Fail();
        }
    }

    [TestClass]
    public class XMLFormatTest
    {
        IFormatter formatter;
        DateTime dateTimeNow;

        public XMLFormatTest()
        {
            formatter = XMLFormatter.Instance;
            dateTimeNow = DateTime.Now;
        }

        public void runTests()
        {
            canCreateXMLFormatter();
            canFormatValidLog();
            canFormatValidLogWithException();
            formatLogThrowExceptionWhenUserIsNull();
            canFormatValidLogWithExceptionAndTrace();
        }

        [TestMethod]
        public void canCreateXMLFormatter()
        {
            Assert.IsNotNull(formatter);
        }

        [TestMethod]
        public void canFormatValidLog()
        {
            formatter = XMLFormatter.Instance;
            string message = "All is OK";
            string expectedText = String.Format("<Log>\r\n  <User>{0}</User>\r\n  <Date>{1}</Date>\r\n  <Level>{2}</Level>\r\n  <Message>{3}</Message>\r\n</Log>", CurrentUser.Instance.FullName, dateTimeNow.ToString(), LogLevel.Debug.ToString(), message);
            Assert.AreEqual(expectedText, formatter.format(new LogData(CurrentUser.Instance, Environment.MachineName, dateTimeNow, LogLevel.Debug, message, null)));
        }

        [TestMethod]
        public void canFormatValidLogWithException()
        {
            string message = "All is OK";
            string exMess = "Exception is OK";
            Exception ex = new DebugException(exMess);
            string expectedText = String.Format("<Log>\r\n  <User>{0}</User>\r\n  <Date>{1}</Date>\r\n  <Level>{2}</Level>\r\n  <Message>{3}</Message>\r\n  <Exception Message=\"Exception is OK\">\r\n    <Type>KernelTest.DebugException</Type>\r\n    <StackTrace></StackTrace>\r\n  </Exception>\r\n</Log>", CurrentUser.Instance.FullName, dateTimeNow.ToString(), LogLevel.Debug.ToString(), message);
            //string expectedText = User.Instance.FullName + ". " + DateTime.Now.ToString() + ". " + LogLevel.Debug.ToString() + ": " + message + ". \r\n" + ex.ToString();
            Assert.AreEqual(expectedText, formatter.format(new LogData(CurrentUser.Instance, Environment.MachineName, dateTimeNow, LogLevel.Debug, message, ex)));
        }

        [TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        public void formatLogThrowExceptionWhenUserIsNull()
        {
            string message = "All is OK";
            string expectedText = CurrentUser.Instance.FullName + ". " + dateTimeNow.ToString() + ". " + LogLevel.Debug.ToString() + ": " + message;
            try
            {
                formatter.format(new LogData(null, Environment.MachineName, dateTimeNow, LogLevel.Debug, message, new DebugException("Exception is OK")));
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(NullReferenceException));
                Assert.AreEqual("Ссылка на объект не указывает на экземпляр объекта.", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void canFormatValidLogWithExceptionAndTrace()
        {
            string message = "All is OK";
            string exMess = "Exception is OK";
            try
            {
                DebugException.throwDebugException(exMess);
            }
            catch (Exception ex)
            {
                string expectedText = String.Format("<Log>\r\n  <User>{0}</User>\r\n  <Date>{1}</Date>\r\n  <Level>{2}</Level>\r\n  <Message>{3}</Message>\r\n  <Exception Message=\"{4}\">\r\n    <Type>{5}</Type>\r\n    <StackTrace>{6}</StackTrace>\r\n  </Exception>\r\n</Log>", CurrentUser.Instance.FullName, dateTimeNow.ToString(), LogLevel.Debug.ToString(), message, exMess, ex.GetType(), ex.StackTrace);
                //string expectedText = User.Instance.FullName + ". " + DateTime.Now.ToString() + ". " + LogLevel.Debug.ToString() + ": " + message + ". \r\n" + ex.ToString();
                Assert.AreEqual(expectedText, formatter.format(new LogData(CurrentUser.Instance, Environment.MachineName, dateTimeNow, LogLevel.Debug, message, ex)));
                return;
            }
            Assert.Fail();
        }
    }
}
