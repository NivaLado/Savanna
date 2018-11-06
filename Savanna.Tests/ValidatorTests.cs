using Savanna.Services;
using Xunit;

namespace Savanna.Tests
{
    public class ValidatorTests
    {
        [Theory]
        [InlineData("77", true)]
        [InlineData("qwe", false)]
        public void IsInteger_ShouldReturnBasedOnInput(string input, bool expected)
        {
            var validator = new Validator();

            var result = validator.IsInteger(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("7", 5, true)]
        [InlineData("3", 5, false)]
        [InlineData("5", 5, false)]
        public void Max_ShouldReturnBasedOnInput(string input, int max, bool expected)
        {
            var validator = new Validator();

            var result = validator.GreaterThanMax(input, max);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("7", 5, false)]
        [InlineData("3", 5, true)]
        [InlineData("5", 5, false)]
        public void Min_ShouldReturnBasedOnInput(string input, int min, bool expected)
        {
            var validator = new Validator();

            var result = validator.LessThanMin(input, min);

            Assert.Equal(expected, result);
        }
    }
}
