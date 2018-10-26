using Autofac;
using Savanna.Services;
using Savanna.Flora;
using System;

namespace Savanna
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var startup = new StartupManager(); //Fabric  Factory may be
            startup.RegisterIoCContainers();
            startup.StartTrackingKeyboard();
            startup.DrawGameBorders();

            //var dialogWithUser = startup.DialogContainer.Resolve<DialogWithUser>();
            //dialogWithUser.GameMenu();

            var savannaField = new SavannaFieldManager();
            savannaField.GenerateEmptyField();
            savannaField.AddLionToField();

            var game = new GameManager(new ConsoleRenderer(), savannaField.savanna);
            game.StartGame();
            game.ShowGame();
            //game.Render();
            //game.GameLoop();

            while (true)
            {

            }
        }
    }
}