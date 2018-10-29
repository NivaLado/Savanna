//using Autofac;
using System;
using Savanna.Services;
using Savanna.Rendering;
using Autofac;
using System.Threading;

namespace Savanna
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var startup = new StartupManager();
            startup.RegisterIoCContainers();
            startup.StartTrackingKeyboard();
            startup.DrawGameBorders();

            //var dialogWithUser = startup.DialogContainer.Resolve<DialogWithUser>();
            //dialogWithUser.GameMenu();

            var savannaField = SavannaFieldManager.GetInstance();
            savannaField.GenerateEmptyField();
            //Add Obstacles Before Creating Animals
            savannaField.CreateAndAddObstacleToTheFieldRandomly();
            //Add Animals
            savannaField.CreateAndAddAnimalToTheField(42, 42);
            //Initialize All Neigtbors
            savannaField.AddNeighbors();

            var game = new GameManager(ConsoleRenderer.GetInstance(), SavannaFieldManager.savanna);
            //game.StartGame();
            //game.ShowGame();
            game.Render();
            Thread.Sleep(500);
            game.GameLoop();

            while (true)
            {

            }
        }
    }
}