using System;
using Savanna.Constants;
using Savanna.Interfaces;

namespace Savanna.Services
{
    public class GameNotifications : INotificator
    {
        private static int rows = -Globals.Height / 2 + Globals.YOffset;
        private IRenderer _renderer;

        public GameNotifications(IRenderer renderer)
        {
            _renderer = renderer;
        }

        public void OnAnimalMoved(object source, AnimalEventArgs e)
        {
            //_renderer.WriteMessage("Lion with id " + e.AnimalData.ID + " Moved. Great Job.");
        }

        public void OnAnimalDied(object source, AnimalEventArgs e)
        {
            _renderer.WriteCenteredMessage(
                    e.AnimalData.Type + " with id "
                    + e.AnimalData.ID + " has died.", 46, rows);
            rows++;
        }

        public void OnAnimalBorned(object source, AnimalEventArgs e)
        {
            _renderer.WriteCenteredMessage(
                e.AnimalData.Type + " with id "
                + e.AnimalData.ID + " was borned.", 46, rows);
            rows++;
        }

        public void OnNewDay(int day)
        {
            _renderer.ForegroundColor(ConsoleColor.Green);
            _renderer.DrawGameBorders(Globals.Width, Globals.Height,
                Globals.Width + Globals.XOffset + Globals.XOffset);
            rows = -Globals.Height / 2 + Globals.YOffset;
            _renderer.WriteCenteredMessage("Day " + day, 46, rows);
            rows++;
        }
    }
}
