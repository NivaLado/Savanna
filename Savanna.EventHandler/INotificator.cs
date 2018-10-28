using System;

namespace Savanna.EventHandler
{
    public interface INotificator
    {
        void OnAnimalMoved(object source, EventArgs e);

        void OnAnimalDied(object source, EventArgs e);

        void OnAnimalBorned(object source, EventArgs e);
    }
}
