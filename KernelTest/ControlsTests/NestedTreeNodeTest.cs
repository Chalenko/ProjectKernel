using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Controls;

namespace KernelTest.ControlsTests
{
    [TestClass]
    public class NestedTreeNodeTest
    {
        NestedTreeNode node;

        [TestInitialize]
        public void TestInit()
        {
            node = new NestedTreeNode("Root");
        }

        [TestMethod]
        public void CanCreateNestedTreeNode()
        {
            //Act
            node = new NestedTreeNode();

            //Assert
            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(node, typeof(NestedTreeNode));
            Assert.IsInstanceOfType(node, typeof(System.Windows.Forms.TreeNode));
        }

        [TestMethod]
        public void CanCreateNestedTreeNodeByTextParametr()
        {
            //Act
			node = new NestedTreeNode("Node 1");

            //Assert
            Assert.IsNotNull(node);
            Assert.AreEqual("Node 1", node.Text);
        }

        [TestMethod]
        public void CanCreateNestedTreeNodeByTextAndStateParametr()
        {
            //Act
            node = new NestedTreeNode("Node 1", true);

            //Assert
            Assert.IsNotNull(node);
            Assert.AreEqual("Node 1", node.Text);
            Assert.IsTrue(node.Checked);
        }

        [TestMethod]
        public void CanCreateNestedTreeNodeWhithChildren()
        {
            //Arrange
            NestedTreeNode child1 = new NestedTreeNode("Child 1");
            NestedTreeNode child2 = new NestedTreeNode("Child 2");
            
            //Act
            node = new NestedTreeNode("Node 1", new NestedTreeNode[] { child1, child2 });

            //Assert
            Assert.IsNotNull(node);
            Assert.AreSame(child1, node.Nodes[0]);
            Assert.AreSame(child2, node.Nodes[1]);
            Assert.AreSame(node, child1.Parent);
            Assert.AreSame(node, child2.Parent);
        }

        [TestMethod]
        public void CanAddChildNode()
        {
            //Arrange
            NestedTreeNode child1 = new NestedTreeNode("Child 1");

            //Act
            node.AddChild(child1);

            //Assert
            Assert.AreSame(child1, node.Nodes[0]);
            Assert.AreSame(node, child1.Parent);
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
            NestedTreeNode child1 = new NestedTreeNode("Child 1");
            node.AddChild(child1);

            //Act
            NestedTreeNode actualNode = node.GetChild(0);

            //Assert
            Assert.AreSame(child1, actualNode);
        }

        [TestMethod]
        public void GetChildThrowArgumentOutOfRangeExceptionWhenDoesNotExistNodeWithThisIndex()
        {
            //Arrange
            NestedTreeNode child1 = new NestedTreeNode("Child 1");
            node.AddChild(child1);

            try
            {
                //Act
                NestedTreeNode actualNode = node.GetChild(5);
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
            NestedTreeNode child1 = new NestedTreeNode("Child 1");
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
            NestedTreeNode child1 = new NestedTreeNode("Child 1");
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
            NestedTreeNode child1 = new NestedTreeNode("Child 1");
            NestedTreeNode child2 = new NestedTreeNode("Child 2");
            node.AddChild(child1);
            node.AddChild(child2);

            //Act
            node.RemoveChild("Child 1");

            //Assert
            Assert.AreEqual(1, node.Nodes.Count);
            Assert.AreEqual("Child 2", node.Nodes[0].Text);
        }

        [TestMethod]
        public void CanRemoveChildsWithEqualsTextByTextParametr()
        {
            //Arrange
            NestedTreeNode child1 = new NestedTreeNode("Child 1");
            NestedTreeNode child2 = new NestedTreeNode("Child 1");
            NestedTreeNode child3 = new NestedTreeNode("Child 2");
            node.AddChild(child1);
            node.AddChild(child2);
            node.AddChild(child3);

            //Act
            node.RemoveChild("Child 1");

            //Assert
            Assert.AreEqual(1, node.Nodes.Count);
            Assert.AreEqual("Child 2", node.Nodes[0].Text);
        }

        [TestMethod]
        public void CanRemoveChildsWithEqualsTextOnStartAndEndOfListByTextParametr()
        {
            //Arrange
            NestedTreeNode child1 = new NestedTreeNode("Child 1");
            NestedTreeNode child2 = new NestedTreeNode("Child 2");
            NestedTreeNode child3 = new NestedTreeNode("Child 1");
            node.AddChild(child1);
            node.AddChild(child2);
            node.AddChild(child3);

            //Act
            node.RemoveChild("Child 1");

            //Assert
            Assert.AreEqual(1, node.Nodes.Count);
            Assert.AreEqual("Child 2", node.Nodes[0].Text);
        }

        [TestMethod]
        public void RemoveChildByIndexThrowArgumentOutOfRangeExceptionWhenDoesNotExistNodeWithThisIndex()
        {
            //Arrange
            NestedTreeNode child1 = new NestedTreeNode("Child 1");
            node.AddChild(child1);

            try
            {
                //Act
                node.RemoveChild(5);
            }
            catch (ArgumentOutOfRangeException e)
            {
                //Assert
                Assert.IsInstanceOfType(e, typeof(ArgumentOutOfRangeException));
                Assert.AreEqual("Заданный аргумент находится вне диапазона допустимых значений.\r\nИмя параметра: index", e.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void NodesAreRenumeringAfterRemovingChild()
        {
            //Arrange
            NestedTreeNode child1 = new NestedTreeNode("Child 1");
            NestedTreeNode child2 = new NestedTreeNode("Child 2");
            NestedTreeNode child3 = new NestedTreeNode("Child 3");
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
        public void RemoveChildByIndexNotFreeMemoryOfNode()
        {
            //Arrange
            NestedTreeNode child1 = new NestedTreeNode("Child 1");
            node.AddChild(child1);

            //Act
            node.RemoveChild(0);

            //Assert
            Assert.IsNotNull(child1);
        }

        [TestMethod]
        public void RemoveChildByTextNotFreeMemoryOfNode()
        {
            //Arrange
            NestedTreeNode child1 = new NestedTreeNode("Child 1");
            node.AddChild(child1);

            //Act
            node.RemoveChild("Child 1");

            //Assert
            Assert.IsNotNull(child1);
        }

        [TestMethod]
        public void CanCreateChildNodeByTextParametr()
        {
            //Act
            node.CreateChild("Child 1");

            //Assert
            Assert.IsNotNull(node.Nodes[0]);
            Assert.AreEqual("Child 1", node.Nodes[0].Text);
        }

        [TestMethod]
        public void CanCreateChildNodeByTextAndStateParametr()
        {
            //Act
            node.CreateChild("Child 1", state: true);

            //Assert
            Assert.IsNotNull(node.Nodes[0]);
            Assert.AreEqual("Child 1", node.Nodes[0].Text);
            Assert.IsTrue(node.Nodes[0].Checked);
        }

        [TestMethod]
        public void AllSubnodesIsCheckedWhenParentIsChecked()
        {
            //Arrange
            node = NestedTreeNodeTest.CreateTestTree();
            NestedTreeNode child2 = node.GetChild(1);

            //Act
            child2.Checked = true;

            //Assert
            Assert.IsFalse(child2.Parent.Checked);
            Assert.IsTrue(child2.Checked);
            Assert.IsTrue(child2.Nodes[0].Checked);
            Assert.IsTrue(child2.Nodes[0].Nodes[0].Checked);
            Assert.IsTrue(child2.Nodes[0].Nodes[1].Checked);
            Assert.IsTrue(child2.Nodes[1].Checked);
        }

        [TestMethod]
        public void AllSubnodesAreUncheckedWhenParentIsUnchecked()
        {
            //Arrange
            node = NestedTreeNodeTest.CreateTestTree();
            node.Checked = true;
            NestedTreeNode child2 = node.GetChild(1);

            //Act
            child2.Checked = false;

            //Assert
            Assert.IsTrue(child2.Parent.Checked);
            Assert.IsFalse(child2.Checked);
            Assert.IsFalse(child2.Nodes[0].Checked);
            Assert.IsFalse(child2.Nodes[0].Nodes[0].Checked);
            Assert.IsFalse(child2.Nodes[0].Nodes[1].Checked);
            Assert.IsFalse(child2.Nodes[1].Checked);
        }

        [TestMethod]
        public void OnlySubnodesChangeState()
        {
            //Arrange
            node = NestedTreeNodeTest.CreateTestTree();
            NestedTreeNode child2 = node.GetChild(1);
            NestedTreeNode child21 = child2.GetChild(0);

            //Act
            child2.Checked = true;
            child21.Checked = false;

            //Assert
            Assert.IsFalse(child2.Parent.Checked);
            Assert.IsTrue(child2.Checked);
            Assert.IsFalse(child2.Nodes[0].Checked);
            Assert.IsFalse(child2.Nodes[0].Nodes[0].Checked);
            Assert.IsFalse(child2.Nodes[0].Nodes[1].Checked);
            Assert.IsTrue(child2.Nodes[1].Checked);
        }

        [TestMethod]
        public void ParentNodeWorkAsNestedTreeNode()
        {
            //Arrange
            node = NestedTreeNodeTest.CreateTestTree();
            NestedTreeNode child21 = node.GetChild(1).GetChild(0);

            //Act
            child21.Parent.Checked = true;

            //Assert
            Assert.IsInstanceOfType(child21.Parent, typeof(NestedTreeNode));
            Assert.IsFalse(child21.Parent.Parent.Checked);
            Assert.IsTrue(child21.Parent.Checked);
            Assert.IsTrue(child21.Parent.Nodes[0].Checked);
            Assert.IsTrue(child21.Parent.Nodes[0].Nodes[0].Checked);
            Assert.IsTrue(child21.Parent.Nodes[0].Nodes[1].Checked);
            Assert.IsTrue(child21.Parent.Nodes[1].Checked);
        }

        [TestMethod]
        public void ReturnsNullParentWhenItIsNotInstanceOfNestedTreeNodeType()
        {
            //Arrange
            System.Windows.Forms.TreeNode superRoot = new System.Windows.Forms.TreeNode("Super Root");
            superRoot.Nodes.Add(node);

            //Act
            System.Windows.Forms.TreeNode parent = node.Parent;

            //Assert
            Assert.IsNull(parent);
        }

        [TestMethod]
        public void ToStringReturnNestedTreeNodeClassNameAndText()
        {
            //Arrange
            node.Text = "Root";

            //Act
            string actual = node.ToString();

            //Assert
            Assert.AreEqual("NestedTreeNode: Root", actual);
        }

        [TestMethod]
        public void CanCloneNode()
        {
            //Arrange
            node = new NestedTreeNode("Root", 55, true);

            //Act
            object actualCopy = node.Clone();

            //Assert
            Assert.ReferenceEquals(node, actualCopy);
            Assert.AreNotSame(node, actualCopy);
            Assert.AreEqual("Root", ((NestedTreeNode)actualCopy).Text);
            Assert.AreEqual(55, ((NestedTreeNode)actualCopy).Value);
            Assert.IsTrue(((NestedTreeNode)actualCopy).Checked);
        }

        [TestMethod]
        public void CanCloneChildNodes()
        {
            //Arrange
            node = new NestedTreeNode("Root", 55, true);
            NestedTreeNode child1 = new NestedTreeNode("Child 1", 551, false);
            NestedTreeNode child2 = new NestedTreeNode("Child 2", 552, false);
            NestedTreeNode child21 = new NestedTreeNode("Child 21", 5551, true);

            node.AddChild(child1);
            node.AddChild(child2);
            child2.AddChild(child21);

            //Act
            object actualCopy = node.Clone();

            //Assert
            Assert.AreEqual(node.Nodes.Count, ((NestedTreeNode)actualCopy).Nodes.Count);

            Assert.ReferenceEquals(child1, ((NestedTreeNode)actualCopy).GetChild(0));
            Assert.AreNotSame(child1, ((NestedTreeNode)actualCopy).GetChild(0));
            Assert.AreEqual("Child 1", ((NestedTreeNode)actualCopy).GetChild(0).Text);
            Assert.AreEqual(551, ((NestedTreeNode)actualCopy).GetChild(0).Value);
            Assert.IsFalse(((NestedTreeNode)actualCopy).GetChild(0).Checked);

            Assert.ReferenceEquals(child2, ((NestedTreeNode)actualCopy).GetChild(1));
            Assert.AreNotSame(child2, ((NestedTreeNode)actualCopy).GetChild(1));
            Assert.AreEqual("Child 2", ((NestedTreeNode)actualCopy).GetChild(1).Text);
            Assert.AreEqual(552, ((NestedTreeNode)actualCopy).GetChild(1).Value);
            Assert.IsFalse(((NestedTreeNode)actualCopy).GetChild(1).Checked);

            Assert.ReferenceEquals(child21, ((NestedTreeNode)actualCopy).GetChild(1).GetChild(0));
            Assert.AreNotSame(child21, ((NestedTreeNode)actualCopy).GetChild(1).GetChild(0));
            Assert.AreEqual("Child 21", ((NestedTreeNode)actualCopy).GetChild(1).GetChild(0).Text);
            Assert.AreEqual(5551, ((NestedTreeNode)actualCopy).GetChild(1).GetChild(0).Value);
            Assert.IsTrue(((NestedTreeNode)actualCopy).GetChild(1).GetChild(0).Checked);
        }

        [TestMethod]
        public void CanInvertCheckedState()
        {
            //Arrange
            node.Checked = true;

            //Act
            node.Invert();

            //Assert
            Assert.IsFalse(node.Checked);
        }

        [TestMethod]
        public void CanInvertUncheckedState()
        {
            //Arrange
            node.Checked = false;

            //Act
            node.Invert();

            //Assert
            Assert.IsTrue(node.Checked);
        }



        /// <summary>
        /// Создает тестовое дерево, которое можно отобразить на TreeView.
        /// </summary>
        /// <returns>Корень тестового дерева.</returns>
        public static NestedTreeNode CreateTestTree()
        {
            NestedTreeNode node = new NestedTreeNode("Root");
            NestedTreeNode node1 = new NestedTreeNode("1");

            NestedTreeNode node211 = new NestedTreeNode("2.1.1");
            NestedTreeNode node212 = new NestedTreeNode("2.1.2");

            NestedTreeNode node21 = new NestedTreeNode("2.1");
            node21.AddChild(node211); 
            node21.AddChild(node212);

            NestedTreeNode node22 = new NestedTreeNode("2.2");

            NestedTreeNode node2 = new NestedTreeNode("2");
            node2.AddChild(node21);
            node2.AddChild(node22);

            NestedTreeNode node31 = new NestedTreeNode("3.1");
            NestedTreeNode node32 = new NestedTreeNode("3.2");
            NestedTreeNode node33 = new NestedTreeNode("3.3");

            NestedTreeNode node3 = new NestedTreeNode("3");
            node3.AddChild(node31);
            node3.AddChild(node32);
            node3.AddChild(node33);

            NestedTreeNode node4 = new NestedTreeNode("4");

            node.AddChild(node1);
            node.AddChild(node2);
            node.AddChild(node3);
            node.AddChild(node4);

            return node;
        }

    }
}
