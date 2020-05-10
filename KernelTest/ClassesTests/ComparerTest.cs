using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Classes.Comparer;

namespace KernelTest.ClassesTest
{
    [TestClass]
    public class ComparerTest
    {
        private ComparerType comparer;

        public ComparerTest()
        {
            TestInit();
        }

        internal void runTests()
        {
            NullLessThanFive();
            FiveMoreThanNull();
            NullEqualsNull();
            new NumberComparerTest().runTests();
            new TextComparerTest().runTests();
            new DateComparerTest().runTests();
            new NaturalComparerTest().runTests();
            new BooleanComparerTest().runTests();
        }

        [TestInitialize]
        public void TestInit()
        {
            comparer = new NumberComparer();
        }

        [TestMethod]
        public void NullLessThanFive()
        {
            int compareResult = comparer.Compare(null, 5.ToString());

            Assert.AreEqual(-1, compareResult);
        }

        [TestMethod]
        public void FiveMoreThanNull()
        {
            int compareResult = comparer.Compare(5.ToString(), null);

            Assert.AreEqual(1, compareResult);
        }

        [TestMethod]
        public void NullEqualsNull()
        {
            int compareResult = comparer.Compare(null, null);

            Assert.AreEqual(0, compareResult);
        }

        [TestMethod]
        public void CanCreateNumberComparer()
        {
            comparer = ComparerType.Number;

            Assert.IsInstanceOfType(comparer, typeof(NumberComparer));
        }

        [TestMethod]
        public void CanCreateTextComparer()
        {
            comparer = ComparerType.Text;

            Assert.IsInstanceOfType(comparer, typeof(TextComparer));
        }

        [TestMethod]
        public void CanCreateDateComparer()
        {
            comparer = ComparerType.Date;

            Assert.IsInstanceOfType(comparer, typeof(DateComparer));
        }

        [TestMethod]
        public void CanCreateNaturalComparer()
        {
            comparer = ComparerType.Natural;

            Assert.IsInstanceOfType(comparer, typeof(NaturalComparer));
        }

        [TestMethod]
        public void CanCreateBooleanComparer()
        {
            comparer = ComparerType.Boolean;

            Assert.IsInstanceOfType(comparer, typeof(BooleanComparer));
        }
    }

    [TestClass]
    public class NumberComparerTest
    {
        private ComparerType comparer;

        public NumberComparerTest()
        {
            TestInit();
        }

        internal void runTests()
        {
            CanCreateNumberComparer();
            TwoLessThanThreeInNumber();
            ThreeMoreThanTwoInNumber();
            TwoEqualsTwoInNumber();
            TwoMoreThenThreeForDescendingInNumber();
            TenMoreThanTwoInNumber();
            CompareTwoAndAAInNumberThrowFormatException();
            CompareAAAndTwoInNumberThrowFormatException();
            GetDescriptionReturnNumberDescription();
        }

        [TestInitialize]
        public void TestInit()
        {
            comparer = new NumberComparer();
        }

        [TestMethod]
        public void CanCreateNumberComparer()
        {
            Assert.IsNotNull(comparer);
        }

        [TestMethod]
        public void TwoLessThanThreeInNumber()
        {
            int compareResult = comparer.Compare(2.ToString(), 3.ToString());
            
            Assert.AreEqual(-1, compareResult);
        }

        [TestMethod]
        public void ThreeMoreThanTwoInNumber()
        {
            int compareResult = comparer.Compare(3.ToString(), 2.ToString());
            
            Assert.AreEqual(1, compareResult);
        }

        [TestMethod]
        public void TwoEqualsTwoInNumber()
        {
            int compareResult = comparer.Compare(2.ToString(), 2.ToString());
            
            Assert.AreEqual(0, compareResult);
        }

        [TestMethod]
        public void TwoMoreThenThreeForDescendingInNumber()
        {
            comparer = new NumberComparer(System.ComponentModel.ListSortDirection.Descending);
            int compareResult = comparer.Compare(2.ToString(), 3.ToString());
            
            Assert.AreEqual(1, compareResult);
        }

        [TestMethod]
        public void TenMoreThanTwoInNumber()
        {
            int compareResult = comparer.Compare(2.ToString(), 10.ToString());
            
            Assert.AreEqual(-1, compareResult);
        }

        [TestMethod]
        public void CompareTwoAndAAInNumberThrowFormatException()
        {
            try
            {
                int compareResult = comparer.Compare(2.ToString(), "AA");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Входная строка str2 имела неверный формат.", ex.Message);
                Assert.IsInstanceOfType(ex.InnerException, typeof(FormatException));
                Assert.AreEqual("Входная строка имела неверный формат.", ex.InnerException.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void CompareAAAndTwoInNumberThrowFormatException()
        {
            try
            {
                int compareResult = comparer.Compare("AA", 2.ToString());
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Входная строка str1 имела неверный формат.", ex.Message);
                Assert.IsInstanceOfType(ex.InnerException, typeof(FormatException));
                Assert.AreEqual("Входная строка имела неверный формат.", ex.InnerException.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void GetDescriptionReturnNumberDescription()
        {
            string actual = comparer.GetDescription();

            Assert.AreEqual("Числовой", actual);
        }
    }

    [TestClass]
    public class TextComparerTest
    {
        private ComparerType comparer;

        public TextComparerTest()
        {
            TestInit();
        }

        internal void runTests()
        {
            CanCreateTextComparer();
            TwoMoreThanThreeInText();
            ThreeLessThanTwoInText();
            TwoEqualsTwoInText();
            TwoLessThenThreeForDescendingInText();
            TwoMoreThenThreeForAscendingInText();
            AAAMoreThanAAInText();
            Text22MoreThanText111InText();
            ComparerReturnZeroIfArgumentsAreNull();
            TextIsMoreThanNull();
            GetDescriptionReturnTextDescription();
        }

        [TestInitialize]
        public void TestInit()
        {
            comparer = new TextComparer();
        }

        [TestMethod]
        public void CanCreateTextComparer()
        {
            Assert.IsNotNull(comparer);
        }

        [TestMethod]
        public void TwoMoreThanThreeInText()
        {
            int compareResult = comparer.Compare("two", "three");
            
            Assert.AreEqual(1, compareResult);
        }

        [TestMethod]
        public void ThreeLessThanTwoInText()
        {
            int compareResult = comparer.Compare("three", "two");
            
            Assert.AreEqual(-1, compareResult);
        }

        [TestMethod]
        public void TwoEqualsTwoInText()
        {
            int compareResult = comparer.Compare("two", "two");
            
            Assert.AreEqual(0, compareResult);
        }

        [TestMethod]
        public void TwoLessThenThreeForDescendingInText()
        {
            comparer = new TextComparer(System.ComponentModel.ListSortDirection.Descending);
            int compareResult = comparer.Compare("two", "three");
            
            Assert.AreEqual(-1, compareResult);
        }

        [TestMethod]
        public void TwoMoreThenThreeForAscendingInText()
        {
            comparer = new TextComparer(System.ComponentModel.ListSortDirection.Ascending);
            int compareResult = comparer.Compare("two", "three");
            
            Assert.AreEqual(1, compareResult);
        }

        [TestMethod]
        public void AAAMoreThanAAInText()
        {
            int compareResult = comparer.Compare("AAA", "AA");

            Assert.AreEqual(1, compareResult);
        }

        [TestMethod]
        public void Text22MoreThanText111InText()
        {
            int compareResult = comparer.Compare("text22", "text111");

            Assert.AreEqual(1, compareResult);
        }

        [TestMethod]
        public void ComparerReturnZeroIfArgumentsAreNull()
        {
            int compareResult = comparer.Compare(null, null);
            
            Assert.AreEqual(0, compareResult);
        }

        [TestMethod]
        public void TextIsMoreThanNull()
        {
            int compareResult = comparer.Compare(null, "text");
            
            Assert.AreEqual(-1, compareResult);
        }

        [TestMethod]
        public void GetDescriptionReturnTextDescription()
        {
            string actual = comparer.GetDescription();

            Assert.AreEqual("Текстовый", actual);
        }
    }

    [TestClass]
    public class DateComparerTest
    {
        private ComparerType comparer;

        public DateComparerTest()
        {
            TestInit();
        }

        internal void runTests()
        {
            CanCreateDateComparer();
            TodayLessThanTomorrowInDate();
            TodayMoreThanYesterdayInDate();
            CompareNowAndAAThrowArgumentException();
            CompareAAAndNowThrowArgumentException();
            NowEqualsNowInDate();
            TodayMoreThenTomorrowForDescendingInText();
            SameDateInDifferentFormatAreEquals();
            GetDescriptionReturnDateDescription();
        }

        [TestInitialize]
        public void TestInit()
        {
            comparer = new DateComparer();
        }

        [TestMethod]
        public void CanCreateDateComparer()
        {
            Assert.IsNotNull(comparer);
        }

        [TestMethod]
        public void TodayLessThanTomorrowInDate()
        {
            int compareResult = comparer.Compare(DateTime.Now.ToString(), DateTime.Now.AddDays(1).ToString());
            
            Assert.AreEqual(-1, compareResult);
        }

        [TestMethod]
        public void TodayMoreThanYesterdayInDate()
        {
            int compareResult = comparer.Compare(DateTime.Now.ToString(), DateTime.Now.AddDays(-1).ToString());
            
            Assert.AreEqual(1, compareResult);
        }

        [TestMethod]
        public void NowEqualsNowInDate()
        {
            string dateText = DateTime.Now.ToString();
            int compareResult = comparer.Compare(dateText, dateText);
            
            Assert.AreEqual(0, compareResult);
        }

        [TestMethod]
        public void TodayMoreThenTomorrowForDescendingInText()
        {
            comparer = new DateComparer(System.ComponentModel.ListSortDirection.Descending);
            int compareResult = comparer.Compare(DateTime.Now.ToString(), DateTime.Now.AddDays(1).ToString());
            
            Assert.AreEqual(1, compareResult);
        }

        [TestMethod]
        public void CompareNowAndAAThrowArgumentException()
        {
            try
            {
                int compareResult = comparer.Compare(DateTime.Now.ToString(), "AA");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Входная строка str2 имела неверный формат.", ex.Message);
                Assert.IsInstanceOfType(ex.InnerException, typeof(FormatException));
                Assert.AreEqual("Данная строка не распознана как действительное значение DateTime. Обнаружено неизвестное слово, начинающееся с индекса 0.", ex.InnerException.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void CompareAAAndNowThrowArgumentException()
        {
            try
            {
                int compareResult = comparer.Compare("AA", DateTime.Now.ToString());
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Входная строка str1 имела неверный формат.", ex.Message);
                Assert.IsInstanceOfType(ex.InnerException, typeof(FormatException));
                Assert.AreEqual("Данная строка не распознана как действительное значение DateTime. Обнаружено неизвестное слово, начинающееся с индекса 0.", ex.InnerException.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void SameDateInDifferentFormatAreEquals()
        {
            string date1 = "22.12.2012";
            string date2 = "2012-12-22";

            int result = comparer.Compare(date1, date2);

            Assert.AreNotEqual(date1, date2);
            Assert.AreEqual(0,result);
        }

        [TestMethod]
        public void GetDescriptionReturnDateDescription()
        {
            string actual = comparer.GetDescription();

            Assert.AreEqual("Дата", actual);
        }
    }

    [TestClass]
    public class NaturalComparerTest
    {
        private ComparerType comparer;

        public NaturalComparerTest()
        {
            TestInit();
        }

        internal void runTests()
        {
            canCreateNaturalComparer();
            Text1EqualsText1InNatural();
            Text2LessThanText11InNatural();
            Text22Text22MoreThanText22TextInNatural();
            Text22TextLessThanText22Text22InNatural();
            TextWithWhitespaceMoreThanTextWithoutWihtespaceInNatural();
            Text2MoreThanText11ForDescendingInNatural();
            NumberLessThanTextInNatural();
            GetDescriptionReturnNaturalDescription();
        }

        [TestInitialize]
        public void TestInit()
        {
            comparer = new NaturalComparer();
        }

        [TestMethod]
        public void canCreateNaturalComparer()
        {
            Assert.IsNotNull(comparer);
        }

        [TestMethod]
        public void Text1EqualsText1InNatural()
        {
            int compareResult = comparer.Compare("text1", "text1");
            
            Assert.AreEqual(0, compareResult);
        }

        [TestMethod]
        public void Text2LessThanText11InNatural()
        {
            int compareResult = comparer.Compare("text2", "text11");

            Assert.AreEqual(-1, compareResult);
        }

        [TestMethod]
        public void Text22Text22MoreThanText22TextInNatural()
        {
            int compareResult = comparer.Compare("text22text22", "text22text");

            Assert.AreEqual(1, compareResult);
        }

        [TestMethod]
        public void Text22TextLessThanText22Text22InNatural()
        {
            int compareResult = comparer.Compare("text22text", "text22text22");

            Assert.AreEqual(-1, compareResult);
        }

        [TestMethod]
        public void TextWithWhitespaceMoreThanTextWithoutWihtespaceInNatural()
        {
            int compareResult = comparer.Compare("text 22 text", "text22text");

            Assert.AreEqual(1, compareResult);
        }

        [TestMethod]
        public void Text2MoreThanText11ForDescendingInNatural()
        {
            comparer = new NaturalComparer(System.ComponentModel.ListSortDirection.Descending);
            int compareResult = comparer.Compare("text2", "text11");
            
            Assert.AreEqual(1, compareResult);
        }

        [TestMethod]
        public void NumberLessThanTextInNatural()
        {
            int compareResult = comparer.Compare("22text22", "text22text");

            Assert.AreEqual(-1, compareResult);
        }

        [TestMethod]
        public void GetDescriptionReturnNaturalDescription()
        {
            string actual = comparer.GetDescription();

            Assert.AreEqual("Естественное", actual);
        }
    }

    [TestClass]
    public class BooleanComparerTest
    {
        private ComparerType comparer;

        public BooleanComparerTest()
        {
            TestInit();
        }

        internal void runTests()
        {
            CanCreateBooleanComparer();
            FalseLessThanTrueInBoolean();
            TrueMoreThanFalseInBoolean();
            TrueEqualsTrueInBoolean();
            FalseMoreThenTrueForDescendingInBoolean();
            CompareTrueAndAAInNumberThrowFormatException();
            CompareAAAndFalseInNumberThrowFormatException();
            SameValueInDifferentFormatAreEquals();
            GetDescriptionReturnLogicDescription();
        }

        [TestInitialize]
        public void TestInit()
        {
            comparer = new BooleanComparer();
        }

        [TestMethod]
        public void CanCreateBooleanComparer()
        {
            Assert.IsNotNull(comparer);
        }

        [TestMethod]
        public void FalseLessThanTrueInBoolean()
        {
            int compareResult = comparer.Compare(false.ToString(), true.ToString());

            Assert.AreEqual(-1, compareResult);
        }

        [TestMethod]
        public void TrueMoreThanFalseInBoolean()
        {
            int compareResult = comparer.Compare(true.ToString(), false.ToString());

            Assert.AreEqual(1, compareResult);
        }

        [TestMethod]
        public void TrueEqualsTrueInBoolean()
        {
            int compareResult = comparer.Compare(true.ToString(), true.ToString());

            Assert.AreEqual(0, compareResult);
        }

        [TestMethod]
        public void FalseMoreThenTrueForDescendingInBoolean()
        {
            comparer = new BooleanComparer(System.ComponentModel.ListSortDirection.Descending);
            int compareResult = comparer.Compare(false.ToString(), true.ToString());

            Assert.AreEqual(1, compareResult);
        }

        [TestMethod]
        public void CompareTrueAndAAInNumberThrowFormatException()
        {
            try
            {
                int compareResult = comparer.Compare(true.ToString(), "AA");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Входная строка str2 имела неверный формат.", ex.Message);
                Assert.IsInstanceOfType(ex.InnerException, typeof(FormatException));
                Assert.AreEqual("Строка не распознана как действительное логическое значение.", ex.InnerException.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void CompareAAAndFalseInNumberThrowFormatException()
        {
            try
            {
                int compareResult = comparer.Compare("AA", false.ToString());
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Входная строка str1 имела неверный формат.", ex.Message);
                Assert.IsInstanceOfType(ex.InnerException, typeof(FormatException));
                Assert.AreEqual("Строка не распознана как действительное логическое значение.", ex.InnerException.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void SameValueInDifferentFormatAreEquals()
        {
            string v1 = "true";
            string v2 = "TRUE";

            int result = comparer.Compare(v1, v2);

            Assert.AreNotEqual(v1, v2);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetDescriptionReturnLogicDescription()
        {
            string actual = comparer.GetDescription();

            Assert.AreEqual("Логический", actual);
        }
    }
}
