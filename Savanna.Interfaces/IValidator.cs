namespace Savanna.Interfaces
{
    public interface IValidator
    {
        bool IsInteger(string input);
        bool LessThanMin(string input, int min);
        bool GreaterThanMax(string input, int max);
    }
}