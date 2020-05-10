using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Classes;
using ProjectKernel.Classes.User;

namespace KernelTest.ClassesTest
{
    [TestClass]
    public class PasswordTest
    {
        public void runTests()
        {
            AD53EIsSlowEqualsAD53E();
            ASDIsNotSlowEqualsSDF();
            generateSaltReturnedNotNullValue();
            generateSaltReturnedNotEmptyString();
            generateHashReturnedNotNullValue();
            generateHashReturnedNotEmptyValue();
            generateHashIsWorked();
        }

        [TestMethod]
        public void AD53EIsSlowEqualsAD53E()
        {
            bool actualValue = PasswordVerificator.SlowEquals("AD53E", "AD53E");
            
            Assert.IsTrue(actualValue);
        }

        [TestMethod]
        public void ASDIsNotSlowEqualsSDF()
        {
            bool actualValue = PasswordVerificator.SlowEquals("ASD", "SDF");
            
            Assert.IsFalse(actualValue);
        }

        [TestMethod]
        public void generateSaltReturnedNotNullValue()
        {
            string salt = Password.GenerateSalt();
            
            Assert.IsNotNull(salt);
        }

        [TestMethod]
        public void generateSaltReturnedNotEmptyString()
        {
            string salt = Password.GenerateSalt();
            
            Assert.AreEqual(4 * Math.Ceiling((double)Password.SALT_BYTES / 3), salt.Length);
        }

        [TestMethod]
        public void generateHashReturnedNotNullValue()
        {
            string salt = "i3WNDvP49h/qTOF7pJrHp5LUCt74WFn1fgpJr5ffRAo=";
            string password = "password";
            
            Assert.IsNotNull(Password.GenerateHash(salt, password));
        }

        [TestMethod]
        public void generateHashReturnedNotEmptyValue()
        {
            string salt = "i3WNDvP49h/qTOF7pJrHp5LUCt74WFn1fgpJr5ffRAo=";
            string password = "password";
            string hash = Password.GenerateHash(salt, password);
            
            Assert.AreEqual(4 * Math.Ceiling((double)Password.HASH_BYTES / 3), hash.Length);
        }

        [TestMethod]
        public void generateHashIsWorked()
        {
#if DEBUG
            string expectedValue = "oewsqaGeMe9IDub0Suit5OlNOEQDe71iE9AN+/6azq2fT8mexMw0dP25aMF407bfwnP5MsmZDqJTXhEzZemYiQ==";
#else
            string expectedValue = "K3yPrAI/5bNd1y1ldbxiY1dtEPDklh6yBVpiaX/XwWrqY3xPjYsVT+T1E3uWEBTJ+0SGq0xBg6IHMWvIAFADIg==";
#endif
            string salt = "i3WNDvP49h/qTOF7pJrHp5LUCt74WFn1fgpJr5ffRAo=";
            string password = "password";
            string hash = Password.GenerateHash(salt, password);
            
            Assert.AreEqual(expectedValue, hash);
        }
    }
}
