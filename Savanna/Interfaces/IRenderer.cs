namespace Savanna.Interfaces
{
    public interface IRenderer
    {
        void WriteMessage(string message);

        void WriteErrorMessage(string errorMessage);
    }
}