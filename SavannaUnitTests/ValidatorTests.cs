using Microsoft.VisualStudio.TestTools.UnitTesting;
using Savanna.Services;

namespace SavannaUnitTests
{
    [TestClass]
    public class ValidatorTests
    {
        [TestMethod]
        public void IsInteger_NumericInput_ReturnsTrue()
        {
            var validator = new Validator();

            var result = validator.IsInteger("77");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsInteger_StringInput_ReturnFalse()
        {
            var validator = new Validator();

            var result = validator.IsInteger("qwe");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Max_MoreThatMax_ReturnTrue()
        {
            var validator = new Validator();

            var result = validator.GreaterThanMax("7", 5);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Max_LessThatMax_ReturnFalse()
        {
            var validator = new Validator();

            var result = validator.GreaterThanMax("3", 5);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Max_EqualToMax_ReturnFalse()
        {
            var validator = new Validator();

            var result = validator.GreaterThanMax("5", 5);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Min_MoreThatMin_ReturnFalse()
        {
            var validator = new Validator();

            var result = validator.LessThanMin("7", 5);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Min_LessThatMin_ReturnTrue()
        {
            var validator = new Validator();

            var result = validator.LessThanMin("3", 5);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Min_EqualToMin_ReturnTrue()
        {
            var validator = new Validator();

            var result = validator.LessThanMin("5", 5);

            Assert.IsFalse(result);
        }
    }
}