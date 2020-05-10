using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Classes;
using ProjectKernel.Classes.User;

namespace KernelTest.ClassesTests
{
    [TestClass]
    public class ActivityTest
    {
        [ClassInitialize()]
        public static void InitClass(TestContext context)
        {
            DatabaseContext.Instance.ExecuteCommand("prepare_user_tables", System.Data.CommandType.StoredProcedure);
            CurrentUser.getDBInstance("a.s.ilichev");
        }

        [TestMethod]
        public void canLogIn()
        {
            //Arrange

            //Act
            Activity.logIn();

            //Assert
            System.Data.DataTable dt = DatabaseContext.Instance.LoadFromDatabase(String.Format("SELECT * FROM Activity WHERE user_login = '{0}'", CurrentUser.Instance.Login), System.Data.CommandType.Text);

            Assert.AreEqual(1, dt.Rows.Count);
            Assert.AreEqual(CurrentUser.Instance.Login, dt.Rows[0]["user_login"]);
            Assert.AreEqual("logIn", dt.Rows[0]["state"]);
            Assert.AreEqual(DateTime.Now.Date, DateTime.Parse(dt.Rows[0]["last_login_datetime"].ToString()).Date);
            Assert.AreEqual(Environment.MachineName, dt.Rows[0]["last_login_workstation"]);
        }

        [TestMethod]
        public void onlyOneRecordForUser()
        {
            //Arrange

            //Act
            Activity.logIn();
            Activity.logIn();
            Activity.logIn();

            //Assert
            object rowCount = DatabaseContext.Instance.ExecuteScalar(String.Format("SELECT COUNT(*) FROM Activity WHERE user_login = '{0}'", CurrentUser.Instance.Login), System.Data.CommandType.Text);

            Assert.AreEqual(1, rowCount);
        }

        [TestMethod]
        public void canLogOut()
        {
            //Arrange

            //Act
            Activity.logIn();
            Activity.logOut();

            //Assert
            System.Data.DataTable dt = DatabaseContext.Instance.LoadFromDatabase(String.Format("SELECT * FROM Activity WHERE user_login = '{0}'", CurrentUser.Instance.Login), System.Data.CommandType.Text);

            Assert.AreEqual(1, dt.Rows.Count);
            Assert.AreEqual(CurrentUser.Instance.Login, dt.Rows[0]["user_login"]);
            Assert.AreEqual("logOut", dt.Rows[0]["state"]);
            Assert.AreEqual(DateTime.Now.Date, DateTime.Parse(dt.Rows[0]["last_logout_datetime"].ToString()).Date);
            Assert.AreEqual(Environment.MachineName, dt.Rows[0]["last_login_workstation"]);
        }

        [TestMethod]
        public void OrderOfUserDoesNotAffect()
        {
            //Arrange

            //Act
            CurrentUser.getDBInstance("a.s.ilichev");
            Activity.logIn();
            CurrentUser.getDBInstance("p.v.chalenko");
            Activity.logIn();
            CurrentUser.getDBInstance("a.s.ilichev");
            Activity.logOut();

            //Assert
            System.Data.DataTable dtIlichev = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM Activity WHERE user_login = 'a.s.ilichev'", System.Data.CommandType.Text);
            Assert.AreEqual(1, dtIlichev.Rows.Count);
            Assert.AreEqual("a.s.ilichev", dtIlichev.Rows[0]["user_login"]);
            Assert.AreEqual("logOut", dtIlichev.Rows[0]["state"]);
            Assert.AreEqual(DateTime.Now.Date, DateTime.Parse(dtIlichev.Rows[0]["last_logout_datetime"].ToString()).Date);
            Assert.AreEqual(Environment.MachineName, dtIlichev.Rows[0]["last_login_workstation"]);

            System.Data.DataTable dtChalenko = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM Activity WHERE user_login = 'p.v.chalenko'", System.Data.CommandType.Text);
            Assert.AreEqual(1, dtChalenko.Rows.Count);
            Assert.AreEqual("p.v.chalenko", dtChalenko.Rows[0]["user_login"]);
            Assert.AreEqual("logIn", dtChalenko.Rows[0]["state"]);
            Assert.AreEqual(DateTime.Now.Date, DateTime.Parse(dtChalenko.Rows[0]["last_login_datetime"].ToString()).Date);
            Assert.AreEqual(Environment.MachineName, dtChalenko.Rows[0]["last_login_workstation"]);
        }
    }
}
