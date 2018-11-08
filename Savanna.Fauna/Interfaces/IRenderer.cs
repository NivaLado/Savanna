using System;

namespace Savanna.Entities.Interfaces
{
    public interface IRenderer
    {
        void CursorVisible(bool visible);
        void WriteMessage(string message);
        void WriteErrorMessage(string errorMessage);
        void Transition(ISavannaField savanna = null);
        void DrawAtXyWithColor(int width, int height, ConsoleColor color);
        void WriteCenteredMessage(string message, int xOffset, int yOffset);
        void ForegroundColor(ConsoleColor color);
        void DrawGame(ISavannaField savanna, int xOffset = 0, int yOffset = 0);
        void DrawGameBorders(int width, int height, int xOffset = 0, int yOffset = 0);
    }
}