using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Classes.Logger;
using ProjectKernel.Classes.User;
using ProjectKernel.Classes;

namespace KernelTest.ClassesTest
{
    [TestClass]
    public class DBLoggerTest
    {
        ILogger logger = DBLogger.Instance;

        internal void runTests()
        {
            canCreateDBLoggerByContext();
            canCreateDBLoggerByConnectionString();
            canCreateDefaultDBLogger();
            logMessageIntoDefaultDBLoggerByDBUser();
            logMessageIntoDefaultDBLoggerBySystemUser();
            logMessageAndException();
        }

        [ClassInitialize()]
        public static void InitClass(TestContext context)
        {
            CurrentUser.getDBInstance("a.s.ilichev");
        }

        [TestMethod]
        public void canCreateDBLoggerByContext()
        {
            ProjectKernel.Classes.DatabaseContext dbc = ProjectKernel.Classes.DatabaseContext.Instance;
            logger = DBLogger.getInstance(dbc);
            Assert.IsNotNull(logger);
        }

        [TestMethod]
        public void canCreateDBLoggerByConnectionString()
        {
            logger = DBLogger.getInstance("Persist Security Info=False; Data Source=vs-nmz-02; Initial Catalog=garazh; User ID=1; Password=''");
            Assert.IsNotNull(logger);
        }

        [TestMethod]
        public void canCreateDefaultDBLogger()
        {
            logger = DBLogger.Instance;
            Assert.IsNotNull(logger);
        }

        [TestMethod]
        public void logMessageIntoDefaultDBLoggerByDBUser()
        {
            string text = "Log function is OK.";
            CurrentUser.getDBInstance("a.s.ilichev");

            logger.log(LogLevel.Debug, text);

            System.Data.DataTable dt = DatabaseContext.Instance.LoadFromDatabase("SELECT TOP 1 * FROM [dbo].[Log] ORDER BY date DESC", System.Data.CommandType.Text);

            Assert.AreEqual("a.s.ilichev", dt.Rows[0]["user_login"]);
            Assert.AreEqual("Database", dt.Rows[0]["user_type"]);
            Assert.AreEqual("Debug", dt.Rows[0]["level"]);
            Assert.AreEqual(Environment.MachineName, dt.Rows[0]["workstation"]);
            Assert.AreEqual(text, dt.Rows[0]["message"]);
            Assert.AreEqual(typeof(DBNull), dt.Rows[0]["exception"].GetType());
        }

        [TestMethod]
        public void logMessageIntoDefaultDBLoggerBySystemUser()
        {
            string text = "Log function is OK.";
            CurrentUser.getSystemInstance();
            
            logger.log(LogLevel.Debug, text);

            System.Data.DataTable dt = DatabaseContext.Instance.LoadFromDatabase("SELECT TOP 1 * FROM [dbo].[Log] ORDER BY date DESC", System.Data.CommandType.Text);

            Assert.AreEqual("p.v.chalenko", dt.Rows[0]["user_login"]);
            Assert.AreEqual("System", dt.Rows[0]["user_type"]);
            Assert.AreEqual("Debug", dt.Rows[0]["level"]);
            Assert.AreEqual(Environment.MachineName, dt.Rows[0]["workstation"]);
            Assert.AreEqual(text, dt.Rows[0]["message"]);
            Assert.AreEqual(typeof(DBNull), dt.Rows[0]["exception"].GetType());
        }

        [TestMethod]
        public void logMessageAndException()
        {
            string text = "Log function is OK. Exception is OK.";
            DebugException ex = new DebugException("Debug Exception is OK.");
            logger.log(LogLevel.Debug, text, ex);

            System.Data.DataTable dt = DatabaseContext.Instance.LoadFromDatabase("SELECT TOP 1 * FROM [dbo].[Log] ORDER BY date DESC", System.Data.CommandType.Text);

            Assert.AreEqual("Debug", dt.Rows[0]["level"]);
            Assert.AreEqual(Environment.MachineName, dt.Rows[0]["workstation"]);
            Assert.AreEqual(text, dt.Rows[0]["message"]);
            Assert.IsNotNull(dt.Rows[0]["exception"]);
            Assert.AreEqual(ex.ToString(), dt.Rows[0]["exception"]);
        }
    }
}
