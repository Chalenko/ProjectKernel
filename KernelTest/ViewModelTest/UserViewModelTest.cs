using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Forms;
using ProjectKernel.Classes.User;
using ProjectKernel.Forms.ViewModel;
using Moq;

namespace KernelTest.ViewModelTest
{
    [TestClass]
    public class UserViewModelTest
    {
        private UserViewModel viewModel;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            CurrentUser.getDBInstance("p.v.chalenko");
        }

        [TestInitialize]
        public void TestInit()
        {
            viewModel = new AddUserViewModel();
        }

        [TestCleanup]
        public void TestClear()
        {
            viewModel = null;
        }

        [TestMethod]
        public void canCreateDefaultModel()
        {
            //Assert
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void canCreateEditModel()
        {
            //Act
            viewModel = new EditUserViewModel(new DBUser("surname", "name", "patronymic", "login", "department"));

            //Assert
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void canCreateModelWithDefaultValues()
        {
            //Assert
            Assert.AreEqual("", viewModel.Surname);
            Assert.AreEqual("", viewModel.Name);
            Assert.AreEqual("", viewModel.Patronymic);
            Assert.AreEqual("", viewModel.Login);
            Assert.AreEqual("", viewModel.Department);
            Assert.IsTrue(viewModel.canEdit);
            Assert.IsFalse(viewModel.canDropPassword);
            Assert.IsNotNull(viewModel.Roles);
            Assert.AreEqual(0, viewModel.Roles.Rows.Count);
        }

        [TestMethod]
        public void canCreateEditModelWithValues()
        {
            //Act
            viewModel = new EditUserViewModel(new DBUser("surname", "name", "patronymic", "login", "department"));

            //Assert
            Assert.AreEqual("surname", viewModel.Surname);
            Assert.AreEqual("name", viewModel.Name);
            Assert.AreEqual("patronymic", viewModel.Patronymic);
            Assert.AreEqual("login", viewModel.Login);
            Assert.AreEqual("department", viewModel.Department);
            Assert.IsNotNull(viewModel.Roles);
            Assert.AreEqual(0, viewModel.Roles.Rows.Count);
        }

        [TestMethod]
        public void canCreateEditModelForExistingDBUser()
        {
            //Act
            viewModel = new EditUserViewModel(DBUserStorage.Instance.getUser("s.e.sergienko"));

            //Assert
            Assert.AreEqual("Сергиенко", viewModel.Surname);
            Assert.AreEqual("Сергей", viewModel.Name);
            Assert.AreEqual("Эдуардович", viewModel.Patronymic);
            Assert.AreEqual("s.e.sergienko", viewModel.Login);
            Assert.AreEqual("Freemake", viewModel.Department);
            Assert.IsNotNull(viewModel.Roles);
            Assert.AreEqual(2, viewModel.Roles.Rows.Count);
            Assert.AreEqual("Friends", viewModel.Roles.Rows[0][0].ToString());
            Assert.AreEqual("NNSU", viewModel.Roles.Rows[1][0].ToString());
        }

        [TestMethod]
        public void editIsAvailableForAdmin()
        {
            //Act
            viewModel = new EditUserViewModel(DBUserStorage.Instance.getUser("p.v.chalenko"));

            //Assert
            Assert.IsTrue(viewModel.canEdit);
        }

        [TestMethod]
        public void editIsUnavailableForNotAdmin()
        {
            //Arrange
            CurrentUser.getDBInstance("t.a.levanova");

            //Act
            viewModel = new EditUserViewModel(DBUserStorage.Instance.getUser("p.v.chalenko"));

            //Assert
            Assert.IsFalse(viewModel.canEdit);

            CurrentUser.getDBInstance("p.v.chalenko");
        }

        [TestMethod]
        public void dropPasswordIsAvailableForAdmin()
        {
            //Act
            viewModel = new EditUserViewModel(DBUserStorage.Instance.getUser("p.v.chalenko"));

            //Assert
            Assert.IsTrue(viewModel.canDropPassword);
        }

        [TestMethod]
        public void dropPasswordIsUnavailableForNotAdmin()
        {
            //Arrange
            CurrentUser.getDBInstance("t.a.levanova");

            //Act
            viewModel = new EditUserViewModel(DBUserStorage.Instance.getUser("p.v.chalenko"));

            //Assert
            Assert.IsFalse(viewModel.canDropPassword);

            CurrentUser.getDBInstance("p.v.chalenko");
        }
        
        [TestMethod]
        public void canDropPasswordForUserChalenko()
        {
            //Arrange
            viewModel = new EditUserViewModel(DBUserStorage.Instance.getUser("p.v.chalenko"));

            //Act
            viewModel.dropPassword(null);

            //Assert
            Assert.IsTrue(DBUserStorage.Instance.checkUser("p.v.chalenko", "qwerty"));
        }

        [TestMethod]
        public void canAddRoleForUser()
        {
            //Act
            viewModel.addRole(null);

            //Assert
            Assert.AreEqual(1, viewModel.Roles.Rows.Count);
            Assert.AreEqual("Spy", viewModel.Roles.Rows[0][0].ToString());
        }

    }
}
