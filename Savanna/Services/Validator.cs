using Savanna.Interfaces;

namespace Savanna.Services
{
    public class Validator : IValidator
    {
        public bool IsInteger(string input)
        {
            return int.TryParse(input, out int number);
        }

        public bool GreaterThanMax(string input, int max)
        {
            int numInput = int.Parse(input);
            return (numInput > max);
        }

        public bool LessThanMin(string input, int min)
        {
            int numInput = int.Parse(input);
            return (numInput < min);
        }
    }
}