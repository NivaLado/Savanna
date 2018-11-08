using System;
using Savanna.Constants;
using Savanna.Entities.Interfaces;

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

        public void OnAnimalStartMoving(object source, AnimalEventArgs e)
        {
            _renderer.WriteCenteredMessage(
                    e.AnimalData.Type + e.AnimalData.ID + " start moving with "
                    + e.AnimalData.Health + " Health.", 46, rows);
            rows++;
        }

        public void OnAnimalStartRunning(object source, AnimalEventArgs e)
        {
            _renderer.WriteCenteredMessage(
                    e.AnimalData.Type + e.AnimalData.ID + " start running with "
                    + e.AnimalData.Health + " Health.", 46, rows);
            rows++;
        }

        public void OnAnimalDied(object source, AnimalEventArgs e)
        {
            _renderer.WriteCenteredMessage(
                    e.AnimalData.Type + e.AnimalData.ID + " has died.", 46, rows);
            rows++;
        }

        public void OnAnimalBorned(object source, AnimalEventArgs e)
        {
            _renderer.WriteCenteredMessage(
                e.AnimalData.Type + e.AnimalData.ID + " was borned.", 46, rows);
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
