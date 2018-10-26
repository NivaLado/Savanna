using System;
using Savanna.Interfaces;

namespace Savanna.Services
{
    public class DialogWithUser
    {
        private IRenderer _renderer;
        private IValidator _validator;

        public DialogWithUser(IRenderer renderer, IValidator validator)
        {
            _renderer = renderer;
            _validator = validator;
        }

        public string ValidInt()
        {
            string input = Console.ReadLine();
            while (!_validator.IsInteger(input))
            {
                WriteErrorMessage("Please input valid number");
                input = Console.ReadLine();
            }
            return input;
        }

        public string ValidMinInt(int min)
        {
            string input = ValidInt();
            while (_validator.LessThanMin(input, min))
            {
                WriteErrorMessage("Min is " + min);
                input = ValidInt();
            }
            return input;
        }

        public string ValidMaxInt(int max)
        {
            string input = ValidInt();
            while (_validator.GreaterThanMax(input, max))
            {
                WriteErrorMessage("Max is " + max);
                input = ValidInt();
            }
            return input;
        }

        public string ValidIntInRange(int min, int max)
        {
            string input = ValidInt();
            while (_validator.LessThanMin(input, min) || _validator.GreaterThanMax(input, max))
            {
                WriteErrorMessage(
                    "Minimum is " + min +
                    " and Maximum is " + max +
                    " you entered " + input
                );
                input = ValidInt();
            }
            return input;
        }

        public void WriteMessage(string message)
        {
            _renderer.WriteMessage(message);
        }

        public void WriteErrorMessage(string message)
        {
            _renderer.WriteErrorMessage(message);
        }

        public void GameMenu()
        {
            _renderer.WriteCenteredMessage("Savanna", 2, 0);
            _renderer.WriteCenteredMessage("P) Start Game", 2, 1);
            _renderer.WriteCenteredMessage("ESC) Exit", 2, 2);
        }
    }
}