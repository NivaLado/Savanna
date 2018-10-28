using System;

namespace Savanna.EventHandlers
{
    public class GameNotifications : INotificator
    {
        IRenderer _renderer;

        public  GameNotifications(IRenderer renderer)
        {

        }

        public void OnAnimalMoved(object source, EventArgs e)
        {
            Console.WriteLine("Lion Moved. Great Job.");
        }

        public void OnAnimalDied(object source, EventArgs e)
        {
            Console.WriteLine("Lion Died. So Sad.");
        }

        public void OnAnimalBorned(object source, EventArgs e)
        {
            Console.WriteLine("Lion was Borned.");
        }
    }
}
