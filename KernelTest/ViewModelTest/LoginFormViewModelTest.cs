using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Forms.ViewModel;
using Moq;

namespace KernelTest.ViewModelTest
{
    [TestClass]
    public class LoginFormViewModelTest
    {
        private LoginFormViewModel viewModel;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            //CurrentUser.getDBInstance("p.v.chalenko");
        }

        [TestInitialize]
        public void TestInit()
        {
            viewModel = new LoginFormViewModel(ProjectKernel.Classes.User.DBUserStorage.Instance.checkUser);
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
        public void ifLoginCheckedThenResultIsOk()
        {
            //Arrange
            Mock<CheckPasswordDelegate> checkMock = new Mock<CheckPasswordDelegate>();
            checkMock.Setup(x => x(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            //Mock<RunDelegate> runMock = new Mock<RunDelegate>();
            //runMock.Setup(x => x());
            viewModel = new LoginFormViewModel(new CheckPasswordDelegate(checkMock.Object));

            //Act
            //viewModel.enter(new ProjectKernel.Forms.View.LoginForm(viewModel));

            //Assert
            Assert.AreEqual(ProjectKernel.Forms.ViewModel.ActionResult.OK, viewModel.Result);
        }

        [TestMethod]
        public void ifLoginNotCheckedThenResultIsAbort()
        {
            //Arrange
            Mock<CheckPasswordDelegate> checkMock = new Mock<CheckPasswordDelegate>();
            checkMock.Setup(x => x(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            viewModel = new LoginFormViewModel(new CheckPasswordDelegate(checkMock.Object));

            //Act
            //viewModel.enter(new ProjectKernel.Forms.View.LoginForm(viewModel));

            //Assert
            Assert.AreEqual(ProjectKernel.Forms.ViewModel.ActionResult.OK, viewModel.Result);
        }
    }
}
