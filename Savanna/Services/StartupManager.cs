using Autofac;
using Savanna.Constants;
using Savanna.Containers;
using Savanna.Interfaces;

namespace Savanna.Services
{
    public class StartupManager
    {
        public GameManager gameManager;
        private IContainer container;

        public void RegisterIoCContainers()
        {
            container = IoCBuilder.Build();
        }

        public void StartTrackingKeyboard()
        {
            var inputManager = container.Resolve<InputManager>();
            inputManager.TrackUserInputTask();
        }

        public void InitializeGame()
        {
            gameManager = container.Resolve<GameManager>();
        }

        public void DrawGameBorders()
        {
            IRenderer renderer = container.Resolve<IRenderer>();
            renderer.DrawGameBorders(Globals.Width, Globals.Height);

            renderer.DrawGameBorders(Globals.Width, Globals.Height,
                Globals.Width + Globals.XOffset + Globals.XOffset);
        }
    }
}
