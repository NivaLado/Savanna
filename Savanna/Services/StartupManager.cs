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
        public DialogWithUser dialog;

        public void DrawGameBorders()
        {
            IRenderer renderer = ConsoleRenderer.GetInstance();
            renderer.DrawGameBorders(Globals.Width, Globals.Height);
        }

        public void StartTrackingKeyboard()
        {
            InputManager.TrackUserInputTask();
            InputManager.SetPauseTo(true);
        }

        public void RegisterIoCContainers()
        {
            DialogContainer = IoCBuilder.BuildDialogWithUser();
        }

        public void InitializeUserDialog()
        {
            dialog = DialogContainer.Resolve<DialogWithUser>();
        }
    }
}
