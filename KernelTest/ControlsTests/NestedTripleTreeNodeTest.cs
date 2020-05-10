using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Controls;

namespace KernelTest.ControlsTests
{
    [TestClass]
    public class NestedTripleTreeNodeTest
    {
        NestedTripleTreeNode node;

        [TestInitialize]
        public void TestInit()
        {
            node = new NestedTripleTreeNode("Root", 0, System.Windows.Forms.CheckState.Checked);
        }

        [TestMethod]
        public void CanCreateNestedTripleTreeNode()
        {
            node = new NestedTripleTreeNode();

            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(node, typeof(TripleTreeNode));
            Assert.IsInstanceOfType(node, typeof(System.Windows.Forms.TreeNode));
        }

        [TestMethod]
        public void CanCreateNestedTripleTreeNodeByTextParametr()
        {
            //Arrange

            //Act
            node = new NestedTripleTreeNode("Node 1");

            //Assert
            Assert.IsNotNull(node);
            Assert.AreEqual("Node 1", node.Text);
        }

        [TestMethod]
        public void CanCreateNestedTripleTreeNodeByTextAndValueParametr()
        {
            //Arrange

            //Act
            node = new NestedTripleTreeNode("Node 2", 2);

            //Assert
            Assert.IsNotNull(node);
            Assert.AreEqual("Node 2", node.Text);
            Assert.AreEqual(2, node.Value);
        }

        [TestMethod]
        public void CanCreateNestedTripleTreeNodeByTextValueAndStateParametr()
        {
            //Arrange

            //Act
            node = new NestedTripleTreeNode("Node 3", 3, System.Windows.Forms.CheckState.Checked);

            //Assert
            Assert.IsNotNull(node);
            Assert.AreEqual("Node 3", node.Text);
            Assert.AreEqual(3, node.Value);
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, node.CheckState);
        }

        [TestMethod]
        public void CanCreateNestedTripleTreeNodeByTextAndValueParametrWithChildren()
        {
            //Arrange
            NestedTripleTreeNode child1 = new NestedTripleTreeNode("Child 1");

            //Act
            node = new NestedTripleTreeNode("Node 4", 4, new NestedTripleTreeNode[] { child1 });

            //Assert
            Assert.IsNotNull(node);
            Assert.AreEqual("Node 4", node.Text);
            Assert.AreEqual(4, node.Value);
            Assert.AreEqual(1, node.Nodes.Count);
            Assert.AreSame(child1, node.Nodes[0]);
        }

        [TestMethod]
        public void CanCreateNestedTripleTreeNodeByTextValueAndStateParametrWithChildren()
        {
            //Arrange
            NestedTripleTreeNode child1 = new NestedTripleTreeNode("Child 1");

            //Act
            node = new NestedTripleTreeNode("Node 4", 4, System.Windows.Forms.CheckState.Indeterminate, new NestedTripleTreeNode[] { child1 });

            //Assert
            Assert.IsNotNull(node);
            Assert.AreEqual("Node 4", node.Text);
            Assert.AreEqual(4, node.Value);
            Assert.AreEqual(System.Windows.Forms.CheckState.Indeterminate, node.CheckState);
            Assert.AreEqual(1, node.Nodes.Count);
            Assert.AreSame(child1, node.Nodes[0]);
        }

        [TestMethod]
        public void CanAddChildNode()
        {
            //Arrange
            NestedTripleTreeNode child1 = new NestedTripleTreeNode("Child 1");

            //Act
            node.AddChild(child1);

            //Assert
            Assert.AreSame(child1, node.Nodes[0]);
            Assert.AreSame(node, child1.Parent);
        }

        [TestMethod]
        public void AddUncheckedChildToCheckedParentChangeParentCheckStateToUnchecked()
        {
            //Arrange
            node = new NestedTripleTreeNode("Root", null, System.Windows.Forms.CheckState.Checked);
            NestedTripleTreeNode child = new NestedTripleTreeNode("Child 1", null, System.Windows.Forms.CheckState.Unchecked);

            //Act
            node.AddChild(child);

            //Assert
            Assert.AreEqual(System.Windows.Forms.CheckState.Unchecked, node.CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Unchecked, child.CheckState);
        }

        [TestMethod]
        public void AddTwoDifferentChildsChangeParentCheckStateToIndeterminate()
        {
            //Arrange
            node = new NestedTripleTreeNode("Root", null, System.Windows.Forms.CheckState.Checked);
            NestedTripleTreeNode child1 = new NestedTripleTreeNode("Child 1", null, System.Windows.Forms.CheckState.Unchecked);
            NestedTripleTreeNode child2 = new NestedTripleTreeNode("Child 2", null, System.Windows.Forms.CheckState.Checked);

            //Act
            node.AddChild(child1);
            node.AddChild(child2);

            //Assert
            Assert.AreEqual(System.Windows.Forms.CheckState.Indeterminate, node.CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Unchecked, child1.CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, child2.CheckState);
        }

        [TestMethod]
        public void AddChildNodeCreateParentReference()
        {
            //Arrange
            NestedTripleTreeNode child = new NestedTripleTreeNode("child");
            node.Nodes.Add(child);

            //Act
            System.Windows.Forms.TreeNode parent = child.Parent;

            //Assert
            Assert.IsNotNull(parent);
            Assert.IsInstanceOfType(parent, typeof(NestedTripleTreeNode));
            Assert.AreSame(node, parent);
        }

        [TestMethod]
        public void AddChildThrowArgumentNullExceptionWhenChildIsNull()
        {
            //Act
            try
            {
                node.AddChild(null);
            }
            catch (ArgumentNullException e)
            {
                //Assert
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: child", e.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void CanGetChildNode()
        {
            //Arrange
            NestedTripleTreeNode child1 = new NestedTripleTreeNode("Child 1");
            node.AddChild(child1);

            //Act
            NestedTripleTreeNode actualNode = node.GetChild(0);

            //Assert
            Assert.AreSame(child1, actualNode);
        }

        [TestMethod]
        public void GetChildThrowArgumentOutOfRangeExceptionWhenDoesNotExistNodeWithThisIndex()
        {
            //Arrange
            NestedTripleTreeNode child1 = new NestedTripleTreeNode("Child 1");
            node.AddChild(child1);

            try
            {
                //Act
                NestedTripleTreeNode actualNode = node.GetChild(5);
            }
            catch (ArgumentOutOfRangeException e)
            {
                //Assert
                Assert.AreEqual("Заданный аргумент находится вне диапазона допустимых значений.\r\nИмя параметра: index", e.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void GetChildThrowArgumentOutOfRangeExceptionWhenChildIndexIsNegative()
        {
            //Arrange
            NestedTripleTreeNode child1 = new NestedTripleTreeNode("Child 1");
            node.AddChild(child1);

            try
            {
                //Act
                NestedTripleTreeNode actualNode = node.GetChild(-5);
            }
            catch (ArgumentOutOfRangeException e)
            {
                //Assert
                Assert.AreEqual("Заданный аргумент находится вне диапазона допустимых значений.\r\nИмя параметра: index", e.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void CanRemoveChildNodeByNodeParametr()
        {
            //Arrange
            NestedTripleTreeNode child1 = new NestedTripleTreeNode("Child 1");
            node.AddChild(child1);

            //Act
            node.RemoveChild(child1);

            //Assert
            Assert.AreEqual(0, node.Nodes.Count);
        }

        [TestMethod]
        public void CanRemoveChildNodeByIndexParametr()
        {
            //Arrange
            NestedTripleTreeNode child1 = new NestedTripleTreeNode("Child 1");
            node.AddChild(child1);

            //Act
            node.RemoveChild(0);

            //Assert
            Assert.AreEqual(0, node.Nodes.Count);
        }

        [TestMethod]
        public void CanRemoveChildNodeByTextParametr()
        {
            //Arrange
            NestedTripleTreeNode child1 = new NestedTripleTreeNode("Child 1");
            NestedTripleTreeNode child2 = new NestedTripleTreeNode("Child 2");
            node.AddChild(child1);
            node.AddChild(child2);

            //Act
            node.RemoveChild("Child 1");

            //Assert
            Assert.AreEqual(1, node.Nodes.Count);
        }

        [TestMethod]
        public void CanRemoveChildsWithEqualsTextByTextParametr()
        {
            //Arrange
            NestedTripleTreeNode child1 = new NestedTripleTreeNode("Child 1");
            NestedTripleTreeNode child2 = new NestedTripleTreeNode("Child 1");
            NestedTripleTreeNode child3 = new NestedTripleTreeNode("Child 2");
            node.AddChild(child1);
            node.AddChild(child2);
            node.AddChild(child3);

            //Act
            node.RemoveChild("Child 1");

            //Assert
            Assert.AreEqual(1, node.Nodes.Count);
        }

        [TestMethod]
        public void CanRemoveChildsWithEqualsTextOnStartAndEndOfListByTextParametr()
        {
            //Arrange
            NestedTripleTreeNode child1 = new NestedTripleTreeNode("Child 1");
            NestedTripleTreeNode child2 = new NestedTripleTreeNode("Child 2");
            NestedTripleTreeNode child3 = new NestedTripleTreeNode("Child 1");
            node.AddChild(child1);
            node.AddChild(child2);
            node.AddChild(child3);

            //Act
            node.RemoveChild("Child 1");

            //Assert
            Assert.AreEqual(1, node.Nodes.Count);
        }

        [TestMethod]
        public void RemoveChildByIndexThrowArgumentOutOfRangeExceptionWhenDoesNotExistNodeWithThisIndex()
        {
            //Arrange
            NestedTripleTreeNode child1 = new NestedTripleTreeNode("Child 1");
            node.AddChild(child1);

            try
            {
                //Act
                node.RemoveChild(5);
            }
            catch (ArgumentOutOfRangeException e)
            {
                //Assert
                Assert.AreEqual("Заданный аргумент находится вне диапазона допустимых значений.\r\nИмя параметра: index", e.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void NodesAreRenumeringAfterRemovingChild()
        {
            //Arrange
            NestedTripleTreeNode child1 = new NestedTripleTreeNode("Child 1");
            NestedTripleTreeNode child2 = new NestedTripleTreeNode("Child 2");
            NestedTripleTreeNode child3 = new NestedTripleTreeNode("Child 3");
            node.AddChild(child1);
            node.AddChild(child2);
            node.AddChild(child3);

            //Act
            node.RemoveChild(1);

            //Assert
            Assert.AreEqual(2, node.Nodes.Count);
            Assert.AreSame(child1, node.Nodes[0]);
            Assert.AreSame(child3, node.Nodes[1]);
        }

        [TestMethod]
        public void RemoveOneOfTwoDifferentChildrenFromIndeterminateParentChangeParentCheckState()
        {
            //Arrange
            node = new NestedTripleTreeNode("Root", value: null);
            NestedTripleTreeNode child1 = new NestedTripleTreeNode("Child 1", value: null, state: System.Windows.Forms.CheckState.Unchecked);
            NestedTripleTreeNode child2 = new NestedTripleTreeNode("Child 2", value: null, state: System.Windows.Forms.CheckState.Checked);
            node.AddChild(child1);
            node.AddChild(child2);

            //Act
            node.RemoveChild(child1);

            //Assert
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, node.CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, child2.CheckState);
        }

        [TestMethod]
        public void RemoveOneOfThreeDifferentChildrenFromIndeterminateParentNotChangeParentCheckState()
        {
            //Arrange
            node = new NestedTripleTreeNode("Root", value: null);
            NestedTripleTreeNode child1 = new NestedTripleTreeNode("Child 1", value: null, state: System.Windows.Forms.CheckState.Unchecked);
            NestedTripleTreeNode child2 = new NestedTripleTreeNode("Child 2", value: null, state: System.Windows.Forms.CheckState.Checked);
            NestedTripleTreeNode child3 = new NestedTripleTreeNode("Child 3", value: null, state: System.Windows.Forms.CheckState.Checked);
            node.AddChild(child1);
            node.AddChild(child2);
            node.AddChild(child3);

            //Act
            node.RemoveChild(child2);

            //Assert
            Assert.AreEqual(System.Windows.Forms.CheckState.Indeterminate, node.CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Unchecked, node.GetChild(0).CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, node.GetChild(1).CheckState);
        }

        [TestMethod]
        public void RemoveSingleChildrenFromParentNotChangeParentCheckState()
        {
            //Arrange
            node = new NestedTripleTreeNode("Root", value: null);
            NestedTripleTreeNode child1 = new NestedTripleTreeNode("Child 1", value: null, state: System.Windows.Forms.CheckState.Unchecked);
            node.AddChild(child1);

            //Act
            node.RemoveChild(child1);

            //Assert
            Assert.AreEqual(System.Windows.Forms.CheckState.Unchecked, node.CheckState);
        }

        [TestMethod]
        public void CanCreateChildNodeByTextParametr()
        {
            //Act
            node.CreateChild("Child 1");

            //Assert
            Assert.IsNotNull(node.Nodes[0]);
            Assert.IsInstanceOfType(node.Nodes[0], typeof(NestedTripleTreeNode));
            Assert.AreEqual("Child 1", node.Nodes[0].Text);
        }

        [TestMethod]
        public void CanCreateChildNodeByTextAndValueParametr()
        {
            //Act
            node.CreateChild("Child 12", 12);

            //Assert
            Assert.IsNotNull(node.Nodes[0]);
            Assert.IsInstanceOfType(node.Nodes[0], typeof(NestedTripleTreeNode));
            Assert.AreEqual("Child 12", node.GetChild(0).Text);
            Assert.AreEqual(12, node.GetChild(0).Value);
        }

        [TestMethod]
        public void CanCreateChildNodeByTextValueAndStateParametr()
        {
            //Act
            node.CreateChild("Child True", true, System.Windows.Forms.CheckState.Checked);

            //Assert
            Assert.IsNotNull(node.Nodes[0]);
            Assert.IsInstanceOfType(node.Nodes[0], typeof(NestedTripleTreeNode));
            Assert.AreEqual("Child True", node.GetChild(0).Text);
            Assert.AreEqual(true, node.GetChild(0).Value);
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, node.GetChild(0).CheckState);
        }

        [TestMethod]
        public void ReturnsNullParentWhenItIsNotInstanceOfNestedTripleTreeNodeType()
        {
            //Arrange
            TripleTreeNode superRoot = new TripleTreeNode("Super Root");
            superRoot.Nodes.Add(node);

            //Act
            System.Windows.Forms.TreeNode parent = node.Parent;

            //Assert
            Assert.IsNull(parent);
        }

        [TestMethod]
        public void AllSubnodesIsCheckedWhenParentIsChecked()
        {
            //Arrange
            node = NestedTripleTreeNodeTest.CreateTestTree();
            NestedTripleTreeNode child2 = node.GetChild(1);

            //Act
            child2.CheckState = System.Windows.Forms.CheckState.Checked;

            //Assert
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, child2.CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, child2.GetChild(0).CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, child2.GetChild(0).GetChild(0).CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, child2.GetChild(0).GetChild(1).CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, child2.GetChild(1).CheckState);
        }

        [TestMethod]
        public void AllSubnodesIsUncheckedWhenParentIsUnchecked()
        {
            //Arrange
            node = NestedTripleTreeNodeTest.CreateTestTree();
            node.CheckState = System.Windows.Forms.CheckState.Checked;
            NestedTripleTreeNode child2 = node.GetChild(1);

            //Act
            child2.CheckState = System.Windows.Forms.CheckState.Unchecked;

            //Assert
            Assert.AreEqual(System.Windows.Forms.CheckState.Unchecked, child2.CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Unchecked, child2.GetChild(0).CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Unchecked, child2.GetChild(0).GetChild(0).CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Unchecked, child2.GetChild(0).GetChild(1).CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Unchecked, child2.GetChild(1).CheckState);
        }

        [TestMethod]
        public void IfChildChangeCheckedStateToAnotherThenParentsAlsoChangeCheckState()
        {
            //Arrange
            node = NestedTripleTreeNodeTest.CreateTestTree();
            NestedTripleTreeNode child21 = node.GetChild(1).GetChild(0);

            //Act
            child21.CheckState = System.Windows.Forms.CheckState.Checked;

            //Assert
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, child21.GetChild(0).CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, child21.GetChild(1).CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, child21.CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Indeterminate, child21.Parent.CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Indeterminate, child21.Parent.Parent.CheckState);
        }

        [TestMethod]
        public void IfChildChangeCheckedStateToAnotherThenParentsAlsoChangeCheckState2()
        {
            //Arrange
            node = NestedTripleTreeNodeTest.CreateTestTree();
            NestedTripleTreeNode child21 = node.GetChild(1).GetChild(0);
            child21.CheckState = System.Windows.Forms.CheckState.Checked;

            //Act
            child21.CheckState = System.Windows.Forms.CheckState.Unchecked;

            //Assert
            Assert.AreEqual(System.Windows.Forms.CheckState.Unchecked, child21.GetChild(0).CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Unchecked, child21.GetChild(1).CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Unchecked, child21.CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Unchecked, child21.Parent.CheckState);
            Assert.AreEqual(System.Windows.Forms.CheckState.Unchecked, child21.Parent.Parent.CheckState);
        }

        /// <summary>
        /// Создает тестовое дерево, которое можно отобразить на TreeView.
        /// </summary>
        /// <returns>Корень тестового дерева.</returns>
        public static NestedTripleTreeNode CreateTestTree()
        {
            NestedTripleTreeNode node = new NestedTripleTreeNode("Root");
            NestedTripleTreeNode node1 = new NestedTripleTreeNode("1");

            NestedTripleTreeNode node211 = new NestedTripleTreeNode("2.1.1");
            NestedTripleTreeNode node212 = new NestedTripleTreeNode("2.1.2");

            NestedTripleTreeNode node21 = new NestedTripleTreeNode("2.1");
            node21.AddChild(node211);
            node21.AddChild(node212);

            NestedTripleTreeNode node22 = new NestedTripleTreeNode("2.2");

            NestedTripleTreeNode node2 = new NestedTripleTreeNode("2");
            node2.AddChild(node21);
            node2.AddChild(node22);

            NestedTripleTreeNode node31 = new NestedTripleTreeNode("3.1");
            NestedTripleTreeNode node32 = new NestedTripleTreeNode("3.2");
            NestedTripleTreeNode node33 = new NestedTripleTreeNode("3.3");

            NestedTripleTreeNode node3 = new NestedTripleTreeNode("3");
            node3.AddChild(node31);
            node3.AddChild(node32);
            node3.AddChild(node33);

            NestedTripleTreeNode node4 = new NestedTripleTreeNode("4");

            node.AddChild(node1);
            node.AddChild(node2);
            node.AddChild(node3);
            node.AddChild(node4);

            return node;
        }

        [TestMethod]
        public void ToStringReturnNestedTripleTreeNodeClassNameTextValueAndState()
        {
            //Arrange
            node.Text = "Root";
            node.Value = 7;
            node.CheckState = System.Windows.Forms.CheckState.Indeterminate;

            //Act
            string actual = node.ToString();

            //Assert
            Assert.AreEqual("NestedTripleTreeNode: Text = [Root]; Value = [7]; CheckState = [Indeterminate]", actual);
        }

    }
}
