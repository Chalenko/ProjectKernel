using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Classes.User;

namespace KernelTest.ClassesTest
{
    [TestClass]
    public class SystemUserTest
    {
        User user;

        public SystemUserTest()
        {
            user = CurrentUser.getSystemInstance();
        }

        internal void runTests()
        {
            CanCreateUserInstance();
            IsUserSystemUser();
            UserInstanceIsNotEmpty();
            IsAdminCorrect();
        }

        //ClassInitialize используется для выполнения кода до запуска первого теста в классе
        //[ClassInitialize()]
        //public static void MyClassInitialize()
        //{
        //    UserCreator.create(UserType.System);
        //}

        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        //TestInitialize используется для выполнения кода перед запуском каждого теста 
        [TestInitialize()]
        public void Initialize()
        {
            user = CurrentUser.Instance; 
        }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }

        [TestMethod]
        public void CanCreateUserInstance()
        {
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void IsUserSystemUser()
        {
            Assert.AreEqual(UserType.System, user.Type);
        }

        [TestMethod]
        public void UserInstanceIsNotEmpty()
        {
            Assert.AreEqual("Павел", user.Name);
            Assert.AreEqual("Чаленко", user.Surname);
            Assert.AreEqual("Чаленко Павел", user.FullName);
            Assert.AreEqual("p.v.chalenko", user.Login);
            Assert.AreEqual("ОРПОиТП", user.Department);
            Assert.AreEqual(UserType.System, user.Type);
        }

        [TestMethod]
        public void IsAdminCorrect()
        {
            bool IsIAdmin = user.IsAdmin;
            Assert.IsTrue(IsIAdmin);
        }
    }
}
