using System;
using Savanna.Interfaces;

namespace Savanna.Services
{
    public class ConsoleRenderer : IRenderer
    {
        public void WriteErrorMessage(string errorMessage)
        {
            ForegroundColor(ConsoleColor.DarkRed);
            Console.WriteLine(errorMessage);
        }

        public void WriteMessage(string message)
        {
            ForegroundColor(ConsoleColor.White);
            Console.WriteLine(message);
        }

        private void ForegroundColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }
}