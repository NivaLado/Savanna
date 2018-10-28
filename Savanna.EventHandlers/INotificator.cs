using System;

namespace Savanna.EventHandlers
{
    public interface INotificator
    {
        void OnAnimalMoved(object source, EventArgs e);

        void OnAnimalDied(object source, EventArgs e);

        void OnAnimalBorned(object source, EventArgs e);
    }
}
