using System;

namespace Savanna.Entities.Interfaces
{
    public interface INotificator
    {
        void OnAnimalStartMoving(object source, AnimalEventArgs e);

        void OnAnimalStartRunning(object source, AnimalEventArgs e);

        void OnAnimalDied(object source, AnimalEventArgs e);

        void OnAnimalBorned(object source, AnimalEventArgs e);

        void OnNewDay(int num);

    }

    public class AnimalEventArgs : EventArgs
    {
        public IEntityData AnimalData { get; set; }
    }
}