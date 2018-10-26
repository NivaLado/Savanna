using Savanna.Flora.Models;

namespace Savanna.Interfaces
{
    public interface IRenderer
    {
        void WriteMessage(string message);

        void WriteErrorMessage(string errorMessage);

        void WriteCenteredMessage(string message, int xOffset, int yOffset);

        void StartTransition();

        void EndTransition(SavannaField savanna);

        void DrawGame(SavannaField savanna, int xOffset = 0, int yOffset = 0);
    }
}