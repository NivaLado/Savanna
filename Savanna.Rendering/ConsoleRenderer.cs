using System;
using System.Threading;
using Savanna.Constants;
using Savanna.Fauna;
using Savanna.Interfaces;

namespace Savanna.Rendering
{
    public class ConsoleRenderer : IRenderer
    {
        #region Singleton
        private static readonly Lazy<ConsoleRenderer> lazy =
                            new Lazy<ConsoleRenderer>(() => new ConsoleRenderer());
        public string Name { get; private set; }

        private ConsoleRenderer()
        {
            Name = Guid.NewGuid().ToString();
        }

        public static ConsoleRenderer GetInstance()
        {
            return lazy.Value;
        }
        #endregion

        public void WriteErrorMessage(string errorMessage)
        {
            ForegroundColor(ConsoleColor.DarkRed);
            Console.WriteLine(errorMessage);
        }

        public void CursorVisible(bool visible)
        {
            Console.CursorVisible = visible;
        }

        public void WriteMessage(string message)
        {
            ForegroundColor(ConsoleColor.White);
            Console.WriteLine(message);
        }

        public void WriteCenteredMessage(string message, int xOffset, int yOffset)
        {
            CenterText(message, Globals.Width, Globals.Height, xOffset, yOffset);
            Console.Write(message);
        }

        public void CenterText(string message, int width, int height, int xOffset, int yOffset)
        {
            int messageWidth = message.Length;
            int centerWidthConsideringMessage = width / 2 - messageWidth / 2;
            int centerHeightConsideringMessage = height / 2;
            Console.SetCursorPosition(
                centerWidthConsideringMessage + xOffset,
                centerHeightConsideringMessage + yOffset);
        }

        public void DrawGame(ISavannaField savanna, int xOffset = 0, int yOffset = 0)
        {
            var grid = savanna.Field;
            var height = grid.GetLength(1);
            var width = grid.GetLength(0);

            Console.SetCursorPosition(xOffset, yOffset);
            for (int y = 0; y < height; y++)
            {
                Console.SetCursorPosition(xOffset, Console.CursorTop);
                for (int x = 0; x < width; x++)
                {
                    SavannaVisualization(grid, x, y);

                    if (x == width - 1)
                    {
                        Console.WriteLine("\r");
                        Console.SetCursorPosition(xOffset, Console.CursorTop);
                    }
                }
            }
            Console.SetCursorPosition(0, 0);
        }

        public void DrawGameBorders(int width, int height, int xOffset = 0, int yOffset = 0)
        {
            DrawTopLine(width);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    DrawLeftBorder(x, xOffset);
                    Console.Write(" ");
                    DrawRightBorder(x, xOffset, width);
                }
            }
            DrawBottomLine(width);
            Console.SetCursorPosition(0, 0);
        }

        public void StartTransition()
        {
            int x = Globals.Width / 2;
            int y = Globals.Height / 2;
            int iterations = Globals.Width + Globals.Height - 1;
            int shiftY = 0, shiftX = 0;
            int h = 0, counter = 0;

            Console.SetCursorPosition(x + 1, y + 1);
            Console.Write("*");

            for (int u = 0; u < iterations; u++)
            {
                if (u == iterations - 1)
                {
                    h--;
                }
                for (int f = 0; f < 1 + h; f++)
                {
                    Conditions(u, ref shiftX, ref shiftY);
                    Console.SetCursorPosition(x + shiftX + 1, y + shiftY + 1);
                    Console.Write("*");
                    Thread.Sleep(1);
                }

                counter++;
                if (counter == 2)
                {
                    counter = 0;
                    h++;
                }
            }
        }

        public void EndTransition(ISavannaField savanna)
        {
            int x = Globals.Width / 2;
            int y = Globals.Height / 2;
            int shiftY = 0, shiftX = 0;
            int iterations = Globals.Width + Globals.Height - 1;
            int steps = 0; int counter = 0;

            Console.SetCursorPosition(x + 1, y + 1);
            SavannaVisualization(savanna.Field, x, y, shiftX, shiftY);

            for (int u = 0; u < iterations; u++)
            {
                if (u == iterations - 1)
                {
                    steps--;
                }
                for (int f = 0; f < 1 + steps; f++)
                {
                    Conditions(u, ref shiftX, ref shiftY);
                    Console.SetCursorPosition(x + shiftX + 1, y + shiftY + 1);
                    SavannaVisualization(savanna.Field, x, y, shiftX, shiftY);
                    Thread.Sleep(1);
                }
                counter++;
                if (counter == 2)
                {
                    counter = 0;
                    steps++;
                }
            }
        }

        public void DrawAtXyWithColor(int x, int y, ConsoleColor color)
        {
            ForegroundColor(color);
            Console.SetCursorPosition(x + 1, y + 1);
            Console.Write("*");
            ForegroundColor(ConsoleColor.White);
        }

        private void SavannaVisualization(ICellBase[,] field, int x, int y, int shiftX = 0, int shiftY = 0)
        {
            if (field[x + shiftX, y + shiftY] is Obstacle)
            {
                Console.Write("x");
            }
            else if (field[x + shiftX, y + shiftY] is Ground)
            {
                Console.Write(" ");
            }
            else if (field[x + shiftX, y + shiftY] is GrassEater)
            {
                Console.Write("A");
            }
            else if (field[x + shiftX, y + shiftY] is IAnimal)
            {
                Console.Write("L");
            }
        }

        private void Conditions(int direction, ref int shiftX, ref int shiftY)
        {
            int directionModule = direction % 4;
            switch (directionModule)
            {
                case 0:
                    shiftX -= 1;
                    break;
                case 1:
                    shiftY -= 1;
                    break;
                case 2:
                    shiftX += 1;
                    break;
                case 3:
                    shiftY += 1;
                    break;
                default:
                    break;
            }

        }

        private void ForegroundColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        private void DrawTopLine(int width)
        {
            Console.Write("╔");

            for (int i = 0; i < width; i++)
            {
                Console.Write("═");
            }

            Console.Write("╗");
            Console.WriteLine("\r");
        }

        private void DrawBottomLine(int width)
        {
            Console.Write("╚");

            for (int i = 0; i < width; i++)
                Console.Write("═");

            Console.Write("╝");
            Console.WriteLine("\r");
        }

        private void DrawLeftBorder(int width, int xOffset)
        {
            if (width == 0)
            {
                Console.SetCursorPosition(xOffset, Console.CursorTop);
                Console.Write("║");
            }
        }

        private void DrawRightBorder(int width, int xOffset, int gridWidth)
        {
            if (width == gridWidth - 1)
            {
                Console.Write("║");
                Console.WriteLine("\r");
                Console.SetCursorPosition(xOffset, Console.CursorTop);
            }
        }
    }
}