using System;
using System.Threading.Tasks;
using Savanna.Constants;

namespace GameOfLife.Services
{
    public class InputManager
    {
        public void TrackUserUnput()
        {
            while (true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        Task.Delay(Globals.InputDelay).Wait();
                        break;

                    case ConsoleKey.P:
                        Console.WriteLine("Play");
                        Task.Delay(Globals.InputDelay).Wait();
                        break;

                    case ConsoleKey.S:
                        //Globals.Save = !Globals.Save;
                        Task.Delay(Globals.InputDelay).Wait();
                        break;

                    case ConsoleKey.D:
                        //Globals.ChangeVisibleGames = true;
                        Task.Delay(Globals.InputDelay).Wait();
                        break;

                    default:
                        break;
                }
            }
        }
    }
}