using NUnit.Framework;
using Savanna.Services;

namespace SavannaUnitTests
{
    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        public void Min_MoreThatMin_ReturnFalse()
        {
            var validator = new Validator();

            var result = validator.LessThanMin("7", 5);

            Assert.IsFalse(result);
        }

        [Test]
        public void Min_LessThatMin_ReturnTrue()
        {
            var validator = new Validator();

            var result = validator.LessThanMin("3", 5);

            Assert.IsTrue(result);
        }

        [Test]
        public void Min_EqualToMin_ReturnTrue()
        {
            var validator = new Validator();

            var result = validator.LessThanMin("5", 5);

            Assert.IsFalse(result);
        }
    }
}