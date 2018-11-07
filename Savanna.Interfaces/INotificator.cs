using System;

namespace Savanna.Interfaces
{
    public interface INotificator
    {
        void OnAnimalMoved(object source, AnimalEventArgs e);

        void OnAnimalDied(object source, AnimalEventArgs e);

        void OnAnimalBorned(object source, AnimalEventArgs e);

        void OnNewDay(int num);

    }

    public class AnimalEventArgs : EventArgs
    {
        public IEntityData AnimalData { get; set; }
    }
}