using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Controls;

namespace KernelTest.ControlsTests
{
    [TestClass]
    public class TripleTreeNodeTest
    {
        TripleTreeNode node;

        [TestInitialize]
        public void TestInit()
        {
            node = new TripleTreeNode("Root", 0, System.Windows.Forms.CheckState.Checked);
        }

        [TestMethod]
        public void CanCreateTripleTreeNode()
        {
            node = new TripleTreeNode();

            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(node, typeof(TripleTreeNode));
            Assert.IsInstanceOfType(node, typeof(System.Windows.Forms.TreeNode));
        }

        [TestMethod]
        public void CanCreateTripleTreeNodeByTextParametr()
        {
            //Arrange

            //Act
            node = new TripleTreeNode("Node 1");

            //Assert
            Assert.IsNotNull(node);
            Assert.AreEqual("Node 1", node.Text);
        }

        [TestMethod]
        public void CanCreateTripleTreeNodeByTextAndValueParametr()
        {
            //Arrange
			 
            //Act
			node = new TripleTreeNode("Node 2", 2);

            //Assert
            Assert.IsNotNull(node);
            Assert.AreEqual("Node 2", node.Text);
            Assert.AreEqual(2, node.Value);
        }

        [TestMethod]
        public void CanCreateTripleTreeNodeByTextValueAndStateParametr()
        {
            //Arrange
			 
            //Act
			node = new TripleTreeNode("Node 3", 3, System.Windows.Forms.CheckState.Checked);
            
            //Assert
            Assert.IsNotNull(node);
            Assert.AreEqual("Node 3", node.Text);
            Assert.AreEqual(3, node.Value);
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, node.CheckState);
        }

        [TestMethod]
        public void CanCreateTripleTreeNodeByTextAndValueParametrWithChildren()
        {
            //Arrange
            TripleTreeNode child1 = new TripleTreeNode("Child 1");

            //Act
            node = new TripleTreeNode("Node 4", 4, new TripleTreeNode[] { child1 });

            //Assert
            Assert.IsNotNull(node);
            Assert.AreEqual("Node 4", node.Text);
            Assert.AreEqual(4, node.Value);
            Assert.AreEqual(1, node.Nodes.Count);
            Assert.AreSame(child1, node.Nodes[0]);
        }

        [TestMethod]
        public void CanCreateTripleTreeNodeByTextValueAndStateParametrWithChildren()
        {
            //Arrange
            TripleTreeNode child1 = new TripleTreeNode("Child 1");

            //Act
            node = new TripleTreeNode("Node 4", 4, System.Windows.Forms.CheckState.Indeterminate, new TripleTreeNode[] { child1 });

            //Assert
            Assert.IsNotNull(node);
            Assert.AreEqual("Node 4", node.Text);
            Assert.AreEqual(4, node.Value);
            Assert.AreEqual(System.Windows.Forms.CheckState.Indeterminate, node.CheckState);
            Assert.AreEqual(1, node.Nodes.Count);
            Assert.AreSame(child1, node.Nodes[0]);
        }

        [TestMethod]
        public void CheckedStateIsCheckedBoolean()
        {
            //Arrange
            node.CheckState = System.Windows.Forms.CheckState.Checked;

            //Act
            bool actualChecked = node.Checked;

            //Assert
            Assert.IsTrue(actualChecked);
        }

        [TestMethod]
        public void IndeterminateStateIsNotCheckedBoolean()
        {
            //Arrange
            node.CheckState = System.Windows.Forms.CheckState.Indeterminate;

            //Act
            bool actualChecked = node.Checked;

            //Assert
            Assert.IsFalse(actualChecked);
        }

        [TestMethod]
        public void UncheckedStateIsNotCheckedBoolean()
        {
            //Arrange
            node.CheckState = System.Windows.Forms.CheckState.Unchecked;

            //Act
            bool actualChecked = node.Checked;

            //Assert
            Assert.IsFalse(actualChecked);
        }

        [TestMethod]
        public void CheckedBooleanIsCheckedState()
        {
            //Arrange
            node.Checked = true;

            //Act
            System.Windows.Forms.CheckState state = node.CheckState;

            //Assert
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, state);
        }

        [TestMethod]
        public void UncheckedBooleanIsUncheckedState()
        {
            //Arrange
            node.Checked = false;

            //Act
            System.Windows.Forms.CheckState state = node.CheckState;

            //Assert
            Assert.AreEqual(System.Windows.Forms.CheckState.Unchecked, state);
        }

        [TestMethod]
        public void CanAddChildNode()
        {
            //Arrange
            TripleTreeNode child1 = new TripleTreeNode("Child 1");

            //Act
            node.AddChild(child1);

            //Assert
            Assert.AreSame(child1, node.Nodes[0]);
            Assert.AreSame(node, child1.Parent);
        }

        [TestMethod]
        public void AddChildNodeCreateParentReference()
        {
            //Arrange
            TripleTreeNode child = new TripleTreeNode("child");
            node.Nodes.Add(child);

            //Act
            System.Windows.Forms.TreeNode parent = child.Parent;

            //Assert
            Assert.IsNotNull(parent);
            Assert.IsInstanceOfType(parent, typeof(TripleTreeNode));
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
            TripleTreeNode child1 = new TripleTreeNode("Child 1");
            node.AddChild(child1);

            //Act
            TripleTreeNode actualNode = node.GetChild(0);

            //Assert
            Assert.AreSame(child1, actualNode);
        }

        [TestMethod]
        public void GetChildThrowArgumentOutOfRangeExceptionWhenDoesNotExistNodeWithThisIndex()
        {
            //Arrange
            TripleTreeNode child1 = new TripleTreeNode("Child 1");
            node.AddChild(child1);

            try
            {
                //Act
                TripleTreeNode actualNode = node.GetChild(5);
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
            TripleTreeNode child1 = new TripleTreeNode("Child 1");
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
            TripleTreeNode child1 = new TripleTreeNode("Child 1");
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
            TripleTreeNode child1 = new TripleTreeNode("Child 1");
            TripleTreeNode child2 = new TripleTreeNode("Child 2");
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
            TripleTreeNode child1 = new TripleTreeNode("Child 1");
            TripleTreeNode child2 = new TripleTreeNode("Child 1");
            TripleTreeNode child3 = new TripleTreeNode("Child 2");
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
            TripleTreeNode child1 = new TripleTreeNode("Child 1");
            TripleTreeNode child2 = new TripleTreeNode("Child 2");
            TripleTreeNode child3 = new TripleTreeNode("Child 1");
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
            TripleTreeNode child1 = new TripleTreeNode("Child 1");
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
            TripleTreeNode child1 = new TripleTreeNode("Child 1");
            TripleTreeNode child2 = new TripleTreeNode("Child 2");
            TripleTreeNode child3 = new TripleTreeNode("Child 3");
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
            TripleTreeNode child1 = new TripleTreeNode("Child 1");
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
            TripleTreeNode child1 = new TripleTreeNode("Child 1");
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
            Assert.IsInstanceOfType(node.Nodes[0], typeof(TripleTreeNode));
            Assert.AreEqual("Child 1", node.Nodes[0].Text);
        }

        [TestMethod]
        public void CanCreateChildNodeByTextAndValueParametr()
        {
            //Act
            node.CreateChild("Child 12", 12);

            //Assert
            Assert.IsNotNull(node.Nodes[0]);
            Assert.IsInstanceOfType(node.Nodes[0], typeof(TripleTreeNode));
            Assert.AreEqual("Child 12", node.GetChild(0).Text);
            Assert.AreEqual(12, node.GetChild(0).Value);
            Assert.AreEqual(1, node.Nodes.Count);
        }

        [TestMethod]
        public void CanCreateChildNodeByTextValueAndStateParametr()
        {
            //Act
            node.CreateChild("Child True", true, System.Windows.Forms.CheckState.Checked);

            //Assert
            Assert.IsNotNull(node.Nodes[0]);
            Assert.IsInstanceOfType(node.Nodes[0], typeof(TripleTreeNode));
            Assert.AreEqual("Child True", node.GetChild(0).Text);
            Assert.AreEqual(true, node.GetChild(0).Value);
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, node.GetChild(0).CheckState);
            Assert.AreEqual(1, node.Nodes.Count);
        }

        [TestMethod]
        public void ReturnsNullParentWhenItIsNotInstanceOfTripleTreeNodeType()
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
        public void CanCloneTripleTreeNode()
        {
            //Arrange
            TripleTreeNode child1 = new TripleTreeNode("Child 1", 5);
            TripleTreeNode child2 = new TripleTreeNode("CHILD 2", true, System.Windows.Forms.CheckState.Indeterminate);
            node.AddChild(child1);
            node.Nodes.Add(child2);

            //Act
            TripleTreeNode clone = (TripleTreeNode)(node.Clone());

            //Assert
            Assert.ReferenceEquals(node, clone);
            Assert.AreEqual(node.Text, clone.Text);
            Assert.AreEqual(node.Value, clone.Value);
            Assert.AreEqual(node.CheckState, clone.CheckState);
            Assert.AreEqual(node.Nodes.Count, clone.Nodes.Count);
            Assert.ReferenceEquals(node.Nodes[0], clone.Nodes[0]);
            Assert.ReferenceEquals(node.Nodes[1], clone.Nodes[1]);
            Assert.AreNotSame(node, clone);
            Assert.AreNotSame(node.Nodes[0], clone.Nodes[0]);
            Assert.AreNotSame(node.Nodes[1], clone.Nodes[1]);
        }

        [TestMethod]
        public void CanCloneNode()
        {
            //Arrange
            node = new TripleTreeNode("Root", 55, System.Windows.Forms.CheckState.Checked);

            //Act
            object actualCopy = node.Clone();

            //Assert
            Assert.ReferenceEquals(node, actualCopy);
            Assert.AreNotSame(node, actualCopy);
            Assert.AreEqual("Root", ((TripleTreeNode)actualCopy).Text);
            Assert.AreEqual(55, ((TripleTreeNode)actualCopy).Value);
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, ((TripleTreeNode)actualCopy).CheckState);
        }

        [TestMethod]
        public void CanCloneChildNodes()
        {
            //Arrange
            node = new TripleTreeNode("Root", 55, System.Windows.Forms.CheckState.Checked);
            TripleTreeNode child1 = new TripleTreeNode("Child 1", 551, System.Windows.Forms.CheckState.Unchecked);
            TripleTreeNode child2 = new TripleTreeNode("Child 2", 552, System.Windows.Forms.CheckState.Indeterminate);
            TripleTreeNode child21 = new TripleTreeNode("Child 21", 5551, System.Windows.Forms.CheckState.Checked);

            node.AddChild(child1);
            node.AddChild(child2);
            child2.AddChild(child21);

            //Act
            object actualCopy = node.Clone();

            //Assert
            Assert.AreEqual(node.Nodes.Count, ((TripleTreeNode)actualCopy).Nodes.Count);

            Assert.ReferenceEquals(child1, ((TripleTreeNode)actualCopy).GetChild(0));
            Assert.AreNotSame(child1, ((TripleTreeNode)actualCopy).GetChild(0));
            Assert.AreEqual("Child 1", ((TripleTreeNode)actualCopy).GetChild(0).Text);
            Assert.AreEqual(551, ((TripleTreeNode)actualCopy).GetChild(0).Value);
            Assert.AreEqual(System.Windows.Forms.CheckState.Unchecked, ((TripleTreeNode)actualCopy).GetChild(0).CheckState);

            Assert.ReferenceEquals(child2, ((TripleTreeNode)actualCopy).GetChild(1));
            Assert.AreNotSame(child2, ((TripleTreeNode)actualCopy).GetChild(1));
            Assert.AreEqual("Child 2", ((TripleTreeNode)actualCopy).GetChild(1).Text);
            Assert.AreEqual(552, ((TripleTreeNode)actualCopy).GetChild(1).Value);
            Assert.AreEqual(System.Windows.Forms.CheckState.Indeterminate, ((TripleTreeNode)actualCopy).GetChild(1).CheckState);

            Assert.ReferenceEquals(child21, ((TripleTreeNode)actualCopy).GetChild(1).GetChild(0));
            Assert.AreNotSame(child21, ((TripleTreeNode)actualCopy).GetChild(1).GetChild(0));
            Assert.AreEqual("Child 21", ((TripleTreeNode)actualCopy).GetChild(1).GetChild(0).Text);
            Assert.AreEqual(5551, ((TripleTreeNode)actualCopy).GetChild(1).GetChild(0).Value);
            Assert.AreEqual(System.Windows.Forms.CheckState.Checked, ((TripleTreeNode)actualCopy).GetChild(1).GetChild(0).CheckState);
        }

        [TestMethod]
        public void ToStringReturnTripleTreeNodeClassNameTextValueAndState()
        {
            //Arrange
            node.Text = "Root";
            node.Value = 7;
            node.CheckState = System.Windows.Forms.CheckState.Indeterminate;

            //Act
            string actual = node.ToString();

            //Assert
            Assert.AreEqual("TripleTreeNode: Text = [Root]; Value = [7]; CheckState = [Indeterminate]", actual);
        }

    }
}
