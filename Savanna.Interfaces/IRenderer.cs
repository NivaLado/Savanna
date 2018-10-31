using System;

namespace Savanna.Interfaces
{
    public interface IRenderer
    {
        void StartTransition();

        void EndTransition(ISavannaField savanna);

        void WriteMessage(string message);

        void WriteErrorMessage(string errorMessage);

        void WriteCenteredMessage(string message, int xOffset, int yOffset);

        void DrawGame(ISavannaField savanna, int xOffset = 0, int yOffset = 0);

        void DrawGameBorders(int width, int height, int xOffset = 0, int yOffset = 0);

        void DrawAtXyWithColor(int width, int height, ConsoleColor color);

        void CursorVisible(bool visible);
    }
}