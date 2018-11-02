using System;
using Savanna.Interfaces;

namespace Savanna.Services
{
    public class ConsoleHelper
    {
        static IRenderer _renderer;

        public static int MultipleChoice(IRenderer renderer, bool canCancel, params string[] options)
        {
            const int optionsPerLine = 1;
            int currentSelection = 0;

            ConsoleKey key;
            _renderer = renderer;
            _renderer.CursorVisible(false);

            do
            {
                _renderer.WriteCenteredMessage("Savanna", 0, -1);
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == currentSelection)
                        Console.ForegroundColor = ConsoleColor.Red;

                    _renderer.WriteCenteredMessage(options[i], 0, i);

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

            return currentSelection;
        }
    }
}