using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Classes.User;

namespace KernelTest.ClassesTest
{
    [TestClass]
    public class CurrentUserTest
    {
        User user;

        [ClassInitialize()]
        public static void InitClass(TestContext context)
        {
            ProjectKernel.Classes.DatabaseContext.Instance.ExecuteCommand("prepare_user_tables", System.Data.CommandType.StoredProcedure);
        }

        [TestMethod]
        public void CanCreateUserInstance()
        {
            User user = CurrentUser.Instance;

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void CanCreateSystemUser()
        {
            user = CurrentUser.getSystemInstance();

            Assert.IsNotNull(user);
            Assert.AreEqual("Чаленко", CurrentUser.Instance.Surname);
            Assert.AreEqual(UserType.System, user.Type);
        }

        [TestMethod]
        public void CanCreateDBUser()
        {
            user = CurrentUser.getDBInstance("a.s.ilichev");

            Assert.IsNotNull(user);
            Assert.AreEqual("Ильичев", CurrentUser.Instance.Surname);
            Assert.AreEqual(UserType.Database, user.Type);
        }

        [TestMethod]
        public void UserInstanceIsNotEmpty()
        {
            user = CurrentUser.getDBInstance("a.s.ilichev");

            Assert.IsNotNull(user);
            Assert.AreEqual("Андрей", user.Name);
            Assert.AreEqual("Ильичев", user.Surname);
            Assert.AreEqual("Ильичев Андрей", user.FullName);
            Assert.AreEqual("a.s.ilichev", user.Login);
            Assert.AreEqual("ОРПОиТП", user.Department);
            Assert.AreEqual(2, user.Groups.Count);
            Assert.AreEqual("Admin", user.Groups[0]);
            Assert.AreEqual("UIT", user.Groups[1]);
            Assert.AreEqual(UserType.Database, user.Type);
        }

        [TestMethod]
        public void KrylovaIsNotAdmin()
        {
            user = CurrentUser.getDBInstance("l.l.krylova");
            bool isAdmin = CurrentUser.Instance.IsAdmin;
            
            Assert.IsFalse(isAdmin);
        }

        [TestMethod]
        public void IlichevIsAdmin()
        {
            user = CurrentUser.getDBInstance("a.s.ilichev");
            bool isAdmin = CurrentUser.Instance.IsAdmin;
            
            Assert.IsTrue(isAdmin);
        }

        [TestMethod]
        public void CreateDBUserThrowExceptionIfLoginIsNull()
        {
            try
            {
                user = CurrentUser.getDBInstance(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: login", ex.Message);
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void CreateDBUserThrowExceptionIfIllegalLogin()
        {
            try
            {
                user = CurrentUser.getDBInstance("levanova");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("User with login 'levanova' is not exist", ex.Message);
                return;
            }

            Assert.Fail();
        }
    }
}
