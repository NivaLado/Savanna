//using Autofac;
using System;
using Savanna.Services;
using Savanna.Rendering;
using Autofac;

namespace Savanna
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var startup = new StartupManager(); //Fabric Factory may be
            startup.RegisterIoCContainers();
            startup.StartTrackingKeyboard();
            startup.DrawGameBorders();

            //var dialogWithUser = startup.DialogContainer.Resolve<DialogWithUser>();
            //dialogWithUser.GameMenu();

            var savannaField = new SavannaFieldManager();
            savannaField.GenerateEmptyField();
            savannaField.AddNeightbors();
            savannaField.AddLionToField();

            var game = new GameManager(ConsoleRenderer.GetInstance(), savannaField.savanna);
            //game.StartGame();
            //game.ShowGame();
            game.Render();
            game.GameLoop();

            while (true)
            {

            }
        }
    }
}