using System.Threading.Tasks;
using Autofac;
using Savanna.Constants;
using Savanna.Interfaces;
using Savanna.Rendering;

namespace Savanna.Services
{
    public class StartupManager
    {
        public IContainer DialogContainer;

        public void DrawGameBorders()
        {
            IRenderer renderer = ConsoleRenderer.GetInstance();
            renderer.DrawGameBorders(Globals.Width, Globals.Height);
        }

        public void StartTrackingKeyboard()
        {
            InputManager keyboardTracking = new InputManager();
            Task.Factory.StartNew(() => keyboardTracking.TrackUserUnput());
        }

        public void RegisterIoCContainers()
        {
            DialogContainer = IoCBuilder.BuildDialogWithUser();
        }
    }
}
