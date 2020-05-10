using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Classes.User;

namespace KernelTest
{
    [TestClass]
    public class UserCreatorTest
    {
        User user;

        internal void runTests()
        {
            CanCreateSystemUserByCommonFunction();
            CanCreateSystemUser();
            CanCreateDBUser();
            KrylovaIsNotAdmin();
            IlichevIsAdmin();
            CreateDBUserThrowExceptionIfLoginIsNull();
            CreateDBUserThrowExceptionIfIllegalLogin();
        }

        [TestMethod]
        public void CanCreateUserInstance()
        {
            UserCreator.create();
            User user = CurrentUser.Instance;

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void CanCreateSystemUserByCommonFunction()
        {
            UserCreator.create();
            user = CurrentUser.Instance;

            Assert.AreEqual("Чаленко", CurrentUser.Instance.Surname);
        }

        [TestMethod]
        public void CanCreateSystemUser()
        {
            UserCreator.createSystemUser();
            user = CurrentUser.Instance;

            Assert.AreEqual("Чаленко", CurrentUser.Instance.Surname);
            Assert.AreEqual(UserType.System, user.Type);
        }

        [TestMethod]
        public void CanCreateDBUser()
        {
            UserCreator.createDBUser("a.s.ilichev");
            user = CurrentUser.Instance;

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void UserInstanceIsNotEmpty()
        {
            UserCreator.createDBUser("a.s.ilichev");
            user = CurrentUser.Instance;

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
            UserCreator.create(UserType.Database, "l.l.krylova");
            bool isAdmin = CurrentUser.Instance.IsAdmin;
            Assert.IsFalse(isAdmin);
        }

        [TestMethod]
        public void IlichevIsAdmin()
        {
            UserCreator.create(UserType.Database, "a.s.ilichev");
            bool isAdmin = CurrentUser.Instance.IsAdmin;
            Assert.IsTrue(isAdmin);
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void CreateDBUserThrowExceptionIfLoginIsNull()
        {
            try
            {
                UserCreator.createDBUser(null);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
                Assert.AreEqual("Illegal login value", ex.Message);
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void CreateDBUserThrowExceptionIfIllegalLogin()
        {
            try
            {
                UserCreator.createDBUser("levanova");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
                Assert.AreEqual("User is not exist", ex.Message);
                return;
            }

            Assert.Fail();
        }
    }
}
