using System;
using System.Threading.Tasks;
using Savanna.Entities;
using Savanna.Entities.Interfaces;

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
                            break;

                        case ConsoleKey.L:
                            _savannaManager.
                                AddAnimalToTheFieldAtRandom<Predator>("Lion");
                            break;

                        case ConsoleKey.A:
                            _savannaManager.
                                AddAnimalToTheFieldAtRandom<GrassEater>("Antelope");
                            Task.WaitAll();
                            break;

                        case ConsoleKey.E:
                            _savannaManager.
                                AddAnimalToTheFieldAtRandom<GrassEater>("Elephant");
                            Task.WaitAll();
                            break;

                        default:
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