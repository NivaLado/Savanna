using System;
using System.Threading.Tasks;
using Savanna.Constants;

namespace Savanna.Services
{
    public class InputManager
    {
        #region Singleton
        private static readonly Lazy<InputManager> lazy =
                            new Lazy<InputManager>(() => new InputManager());
        public string Name { get; private set; }

        private InputManager()
        {
            Name = Guid.NewGuid().ToString();
        }

        public static InputManager GetInstance()
        {
            return lazy.Value;
        }
        #endregion

        private static bool Pause { get; set; }

        public static void SetPauseTo(bool setTo)
        {
            Pause = setTo;
        }

        private static void TrackUserUnput()
        {
            while (true)
            {
                if (!Pause)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Escape:
                            Environment.Exit(0);
                            Task.Delay(Globals.InputDelay).Wait();
                            break;

                        case ConsoleKey.L:
                            SavannaFieldManager.GetInstance().
                                CreateAndAddAnimalToTheFieldAtRandom(1);
                            break;

                        case ConsoleKey.A:
                            SavannaFieldManager.GetInstance().
                                CreateAndAddAnimalToTheFieldAtRandom(2);
                            break;

                        default:
                            Task.Delay(Globals.InputDelay).Wait();
                            break;
                    }
                }
            }
        }

        public static void TrackUserInputTask()
        {
            Task.Factory.StartNew(() => TrackUserUnput());
        }
    }
}