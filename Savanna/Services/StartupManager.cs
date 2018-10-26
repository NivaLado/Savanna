using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using GameOfLife.Services;
using Savanna.Constants;

namespace Savanna.Services
{
    public class StartupManager
    {
        public IContainer DialogContainer;

        public void DrawGameBorders()
        {
            ConsoleRenderer renderer = new ConsoleRenderer();
            renderer.DrawGameBorders(new int[Globals.Width,Globals.Height]);
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
