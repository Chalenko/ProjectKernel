using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Controls;
using ProjectKernel.Classes;

namespace KernelTest.ControlsTests
{
    [TestClass]
    public class TableControlTest
    {
        TableControl tc;
        DatabaseContext context = DatabaseContext.Instance;

        [TestInitialize]
        public void TestInit()
        {
            tc = new TableControl();
        }

        [TestMethod]
        public void CanCreateTableControl()
        {
            //Act
            tc = new TableControl();

            //Assert
            Assert.IsNotNull(tc);
            Assert.IsInstanceOfType(tc, typeof(TableControl));
        }

        [TestMethod]
        public void CanChangeDataSource()
        {
            //Act
            tc.DataSource = context.LoadFromDatabase("SELECT * FROM [dbo].[User] WHERE department LIKE N'ННГУ' ORDER BY login", System.Data.CommandType.Text);

            //Assert
            Assert.IsNotNull(tc.DataSource);
            Assert.IsInstanceOfType(tc.DataSource, typeof(System.Data.DataTable));
            Assert.IsFalse(((System.Data.DataTable)(tc.DataSource)).Rows.Count == 0);
        }

        [TestMethod]
        public void AvailableActionsIsCorrectForEmptyTable()
        {
            //Act
            tc.DataSource = new System.Data.DataTable();// context.LoadFromDatabase("SELECT * FROM [dbo].[User] WHERE department LIKE N'ННГУУУУУУ' ORDER BY login", System.Data.CommandType.Text);

            //Assert
            Assert.IsTrue(tc.AddButton.Enabled);
            Assert.IsFalse(tc.OpenButton.Enabled);
            Assert.IsFalse(tc.RemoveButton.Enabled);
        }

        [TestMethod]
        public void AvailableActionsIsCorrectForNotEmptyTable()
        {
            //Act
            tc.DataSource = context.LoadFromDatabase("SELECT * FROM [dbo].[User] WHERE department LIKE N'ННГУ' ORDER BY login", System.Data.CommandType.Text);

            //Assert
            Assert.IsTrue(tc.AddButton.Enabled);
            Assert.IsTrue(tc.OpenButton.Enabled);
            Assert.IsTrue(tc.RemoveButton.Enabled);
        }

        [TestMethod]
        public void AvailableActionsIsCorrectForEmptyTableWhenCantEdit()
        {
            //Arrange
            tc.DataSource = new System.Data.DataTable();

            //Act
            tc.CanEdit = false;

            //Assert
            Assert.IsFalse(tc.AddButton.Enabled);
            Assert.IsFalse(tc.OpenButton.Enabled);
            Assert.IsFalse(tc.RemoveButton.Enabled);
        }

        [TestMethod]
        public void AvailableActionsIsCorrectForNotEmptyTableWhenCantEdit()
        {
            //Arrange
            tc.DataSource = context.LoadFromDatabase("SELECT * FROM [dbo].[User] WHERE department LIKE N'ННГУ' ORDER BY login", System.Data.CommandType.Text);

            //Act
            tc.CanEdit = false;

            //Assert
            Assert.IsFalse(tc.AddButton.Enabled);
            Assert.IsFalse(tc.OpenButton.Enabled);
            Assert.IsFalse(tc.RemoveButton.Enabled);
        }

        [TestMethod]
        public void AvailableActionsIsCorrectForEmptyTableWhenCanEdit()
        {
            //Arrange
            tc.DataSource = new System.Data.DataTable();
            tc.CanEdit = false;

            //Act
            tc.CanEdit = true;

            //Assert
            Assert.IsTrue(tc.AddButton.Enabled);
            Assert.IsFalse(tc.OpenButton.Enabled);
            Assert.IsFalse(tc.RemoveButton.Enabled);
        }

        [TestMethod]
        public void AvailableActionsIsCorrectForNotEmptyTableWhenCanEdit()
        {
            //Arrange
            tc.DataSource = context.LoadFromDatabase("SELECT * FROM [dbo].[User] WHERE department LIKE N'ННГУ' ORDER BY login", System.Data.CommandType.Text);
            tc.CanEdit = false;
            
            //Act
            tc.CanEdit = true;

            //Assert
            Assert.IsTrue(tc.AddButton.Enabled);
            Assert.IsTrue(tc.OpenButton.Enabled);
            Assert.IsTrue(tc.RemoveButton.Enabled);
        }

        [TestMethod]
        public void AvailableActionsIsCorrectForEmptyTableWhenAddNewRow()
        {
            //Arrange
            System.Data.DataTable dt = new System.Data.DataTable();
            tc.DataSource = dt;
            dt.Columns.AddRange(new System.Data.DataColumn[] { new System.Data.DataColumn("name") });
            tc.CanEdit = false;
            tc.CanEdit = true;
            
            //Act
            dt.Rows.Add(dt.NewRow());

            //Assert
            Assert.IsTrue(tc.AddButton.Enabled);
            Assert.IsTrue(tc.OpenButton.Enabled);
            Assert.IsTrue(tc.RemoveButton.Enabled);
        }

        [TestMethod]
        public void AvailableActionsIsCorrectForEmptyTableAfterExternalChange()
        {
            //Arrange
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.AddRange(new System.Data.DataColumn[] { new System.Data.DataColumn("name") });
            tc.DataSource = dt;
            tc.CanEdit = false;
            tc.CanEdit = true;

            //Act
            //dt.Rows.Add(dt.NewRow());
            tc.AddButton.Enabled = false;
            tc.OpenButton.Enabled = true;
            tc.RemoveButton.Enabled = true;

            //Assert
            Assert.IsFalse(tc.AddButton.Enabled);
            Assert.IsFalse(tc.OpenButton.Enabled);
            Assert.IsFalse(tc.RemoveButton.Enabled);
        }

        [TestMethod]
        public void AvailableActionsIsCorrectForNotEmptyTableAfterExternalChange()
        {
            //Arrange
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.AddRange(new System.Data.DataColumn[] { new System.Data.DataColumn("name") });
            dt.Rows.Add(dt.NewRow());
            tc.DataSource = dt;

            //Act
            //
            tc.AddButton.Enabled = false;
            tc.OpenButton.Enabled = false;
            tc.RemoveButton.Enabled = true;

            //Assert
            Assert.IsFalse(tc.AddButton.Enabled);
            Assert.IsFalse(tc.OpenButton.Enabled);
            Assert.IsTrue(tc.RemoveButton.Enabled);
        }

        [TestMethod]
        public void AvailableActionsIsCorrectForNotEmptyTableAfterChangeSelectionPosition()
        {
            //Arrange
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.AddRange(new System.Data.DataColumn[] { new System.Data.DataColumn("name") });
            dt.Rows.Add(dt.NewRow());
            dt.Rows.Add(dt.NewRow());
            tc.DataSource = dt;
            tc.AddButton.Enabled = false;
            tc.OpenButton.Enabled = true;
            tc.RemoveButton.Enabled = false;

            //Act
            //
            tc.DataGridView.CurrentCell = tc.DataGridView[0, 1];

            //Assert
            Assert.IsFalse(tc.AddButton.Enabled);
            Assert.IsTrue(tc.OpenButton.Enabled);
            Assert.IsFalse(tc.RemoveButton.Enabled);
        }
    }
}
