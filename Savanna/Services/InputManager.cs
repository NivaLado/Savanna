using System;
using System.Threading.Tasks;
using Savanna.Constants;
using Savanna.Interfaces;

namespace Savanna.Services
{
    public class InputManager : IInputManager
    {
        ISavannaFieldManager _savannaManager;

        private static bool Unpaused { get; set; }

        public InputManager(ISavannaFieldManager savannaManager)
        {
            _savannaManager = savannaManager;
        }

        public void Unpause()
        {
            Unpaused = true;
        }

        public void Pause()
        {
            Unpaused = false;
        }

        private void TrackUserUnput()
        {
            while (true)
            {
                if (Unpaused)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Escape:
                            Environment.Exit(0);
                            Task.Delay(Globals.InputDelay).Wait();
                            break;

                        case ConsoleKey.L:
                            _savannaManager.
                                CreateAndAddAnimalToTheFieldAtRandom(1);
                            break;

                        case ConsoleKey.A:
                            _savannaManager.
                                CreateAndAddAnimalToTheFieldAtRandom(2);
                            break;

                        default:
                            Task.Delay(Globals.InputDelay).Wait();
                            break;
                    }
                }
            }
        }

        public void TrackUserInputTask()
        {
            Task.Factory.StartNew(() => TrackUserUnput());
        }
    }
}