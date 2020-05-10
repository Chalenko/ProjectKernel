using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel;

namespace KernelTest.ClassesTests
{
    [TestClass]
    public class ExtensionsTest
    {
        [TestMethod]
        public void IsNullOrEmptyReturnsTrueIfListIsNull()
        {
            //Arrange
            System.Collections.Generic.List<int> list = null;

            //Act
            bool actualNullOrEmpty = list.IsNullOrEmpty();

            //Assert
            Assert.IsTrue(actualNullOrEmpty);
        }

        [TestMethod]
        public void IsNullOrEmptyReturnsTrueIfArrayIsEmpty()
        {
            //Arrange
            int[] array = new int[0];

            //Act
            bool actualNullOrEmpty = array.IsNullOrEmpty();

            //Assert
            Assert.IsTrue(actualNullOrEmpty);
        }

        [TestMethod]
        public void IsNullOrEmptyReturnsFalseIfDictionaryContainsOneElement()
        {
            //Arrange
            System.Collections.Generic.Dictionary<int, string> dic = new System.Collections.Generic.Dictionary<int, string>();
            dic.Add(1, "First");

            //Act
            bool actualNullOrEmpty = dic.IsNullOrEmpty();

            //Assert
            Assert.IsFalse(actualNullOrEmpty);
        }

        [TestMethod]
        public void IsNullOrWhitespaceReturnsTrueIfStringIsNull()
        {
            //Arrange
            string text = null;

            //Act
            bool actualNullOrWhitespace = text.IsNullOrWhitespace();

            //Assert
            Assert.IsTrue(actualNullOrWhitespace);
        }

        [TestMethod]
        public void IsNullOrWhitespaceReturnsTrueIfStringIsEmpty()
        {
            //Arrange
            string text = "";

            //Act
            bool actualNullOrWhitespace = text.IsNullOrWhitespace();

            //Assert
            Assert.IsTrue(actualNullOrWhitespace);
        }

        [TestMethod]
        public void IsNullOrWhitespaceReturnsTrueIfStringIsThreeSpace()
        {
            //Arrange
            string text = "   ";

            //Act
            bool actualNullOrWhitespace = text.IsNullOrWhitespace();

            //Assert
            Assert.IsTrue(actualNullOrWhitespace);
        }
        
        [TestMethod]
        public void IsNullOrWhitespaceReturnsFalseIfStringIsText()
        {
            //Arrange
            string text = "text";

            //Act
            bool actualNullOrWhitespace = text.IsNullOrWhitespace();

            //Assert
            Assert.IsFalse(actualNullOrWhitespace);
        }

        [TestMethod]
        public void ListToStringReturnsCorrectStringForFiveNumbers()
        {
            //Arrange
            string expected = "1; 2; 3; 4; 5";
            System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>() { 1, 2, 3, 4, 5 };

            //Act
            string actual = list.ListToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ListToStringReturnsCorrectStringForFiveFloatNumber()
        {
            //Arrange
            string expected = "1,1; 2; 3,333333; 4; 0,5";
            System.Collections.Generic.List<float> list = new System.Collections.Generic.List<float>() { 1.1f, 2, 10f/3, 4.0f, .5f };

            //Act
            string actual = list.ListToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ListToStringReturnsCorrectStringForSixWords()
        {
            //Arrange
            string expected = "In; the; beginning; was; the; Word";
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            list.Add("In");
            list.Add("the");
            list.Add("beginning");
            list.Add("was");
            list.Add("the");
            list.Add("Word");

            //Act
            string actual = list.ListToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ListToStringReturnsCorrectStringForEmptyList()
        {
            //Arrange
            string expected = "";
            System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();

            //Act
            string actual = list.ListToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ListToStringThrowExceptionIfListIsNull()
        {
            //Arrange
            System.Collections.Generic.List<int> list = null;

            //Act
            try
            {
                string actual = list.ListToString();
            }

            //Assert
            catch (NullReferenceException ex)
            {
                Assert.AreEqual("Ссылка на объект не указывает на экземпляр объекта.", ex.Message);
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void ObjectPropertyToArray()
        {
            //Arrange

            //Act

            //Assert

        }

    }
}
