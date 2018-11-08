using Savanna.Services;

namespace Savanna
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var startup = new StartupManager();
            startup.RegisterIoCContainers();
            startup.StartTrackingKeyboard();
            startup.InitializeGame();
            startup.DrawGameBorders();

            var game = startup.gameManager;
            game.MainMenu();

            while (true)
            {
                game.GameLoop();
            }
        }
    }
}