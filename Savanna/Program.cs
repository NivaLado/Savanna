using System;
using Savanna.Services;
using Savanna.Rendering;
using System.Threading;
using Savanna.Constants;

namespace Savanna
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var startup = new StartupManager();
            startup.RegisterIoCContainers();
            startup.StartTrackingKeyboard();
            startup.InitializeUserDialog();
            startup.DrawGameBorders();

            var game = new GameManager(
                ConsoleRenderer.GetInstance(),
                SavannaFieldManager.GetInstance(),
                startup.dialog
                );

            game.MainMenu();
            
            while(true)
            {
                game.GameLoop();
            }
        }
    }
}