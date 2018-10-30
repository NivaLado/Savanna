using System;
using Savanna.Interfaces;
using Savanna.Rendering;

namespace Savanna.Services
{
    public class ConsoleHelper
    {
        public static int MultipleChoice(bool canCancel, params string[] options)
        {
            const int optionsPerLine = 1;
            int currentSelection = 0;

            ConsoleKey key;
            IRenderer _renderer = ConsoleRenderer.GetInstance();

            Console.CursorVisible = false;

            do
            {
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == currentSelection)
                        Console.ForegroundColor = ConsoleColor.Red;

                    _renderer.WriteCenteredMessage(options[i], 2, i);

                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            if (currentSelection % optionsPerLine > 0)
                                currentSelection--;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            if (currentSelection % optionsPerLine < optionsPerLine - 1)
                                currentSelection++;
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (currentSelection >= optionsPerLine)
                                currentSelection -= optionsPerLine;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (currentSelection + optionsPerLine < options.Length)
                                currentSelection += optionsPerLine;
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            if (canCancel)
                                return -1;
                            break;
                        }
                }
            } while (key != ConsoleKey.Enter);

            Console.CursorVisible = true;

            return currentSelection;
        }
    }
}