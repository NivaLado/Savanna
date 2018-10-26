using System;
using System.Threading;
using Savanna.Constants;
using Savanna.Interfaces;
using Savanna.Flora.Models;

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

        public void WriteCenteredMessage(string message, int xOffset, int yOffset)
        {
            ForegroundColor(ConsoleColor.White);
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

        public void DrawGame(SavannaField savanna, int xOffset = 0, int yOffset = 0)
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
                    if (grid[y,x] == null)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("L");
                    }

                    if (x == width - 1)
                    {
                        Console.WriteLine("\r");
                        Console.SetCursorPosition(xOffset, Console.CursorTop);
                    }
                }
            }
            Console.SetCursorPosition(0, 0);
        }

        public void DrawGameBorders(int[,] input, int xOffset = 0, int yOffset = 0)
        {
            var grid = input;
            var height = grid.GetLength(1);
            var width = grid.GetLength(0);

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
            int shiftY = 0, shiftX = 0;
            int iterations = Globals.Width + Globals.Height - 1;

            int h = 0; int counter = 0;
            Console.SetCursorPosition(x + 1, y + 1); Console.Write("*");

            for (int u = 0; u < iterations; u++)
            {
                if ( u == iterations -1)
                {
                    h--;
                }
                for (int f = 0; f < 1 + h; f++)
                {
                    Conditions(u, ref shiftX, ref shiftY);
                    Console.SetCursorPosition(x + shiftX + 1, y + shiftY + 1);
                    Console.Write("*");
                    //Thread.Sleep(1);
                }

                counter++;
                if (counter == 2)
                {
                    counter = 0;
                    h++;
                }
            }

        }

        public void EndTransition(SavannaField savanna)
        {
            var field = savanna.Field;
            int x = Globals.Width / 2;
            int y = Globals.Height / 2;
            int shiftY = 0, shiftX = 0;
            int iterations = Globals.Width + Globals.Height - 1;

            int h = 0; int counter = 0;
            Console.SetCursorPosition(x + 1, y + 1); Console.Write("*");

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

                    if (field[y + shiftY, x + shiftX] == null)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("L");
                    }

                    //Thread.Sleep(5);
                }

                counter++;
                if (counter == 2)
                {
                    counter = 0;
                    h++;
                }
            }

        }

        private void ForegroundColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        private static void Conditions(int u, ref int shiftX, ref int shiftY)
        {
            int modU = u % 4;
            if (modU == 0)
                shiftX -= 1;
            else if (modU == 1)
                shiftY -= 1;
            else if (modU == 2)
                shiftX += 1;
            if (modU == 3)
                shiftY += 1;
        }

        private static void DrawTopLine(int width)
        {
            Console.Write("╔");

            for (int i = 0; i < width; i++)
            {
                Console.Write("═");
            }

            Console.Write("╗");
            Console.WriteLine("\r");
        }

        private static void DrawBottomLine(int width)
        {
            Console.Write("╚");

            for (int i = 0; i < width; i++)
                Console.Write("═");

            Console.Write("╝");
            Console.WriteLine("\r");
        }

        private static void DrawLeftBorder(int width, int xOffset)
        {
            if (width == 0)
            {
                Console.SetCursorPosition(xOffset, Console.CursorTop);
                Console.Write("║");
            }
        }

        private static void DrawRightBorder(int width, int xOffset, int gridWidth)
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