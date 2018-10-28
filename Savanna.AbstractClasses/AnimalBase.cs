using System;

namespace Savanna.AbstractClasses
{
    public abstract class AnimalBase : CellBase
    {
        public delegate void AnimalMoveEventHandler(object source, EventArgs args);
        public event AnimalMoveEventHandler AnimalMoved;
        public event AnimalMoveEventHandler AnimalBorned;

        public AnimalBase(INotificator notificator)
        {

        }

        public void Move()
        {
            OnAnimalMoved();
        }

        protected virtual void OnAnimalMoved()
        {
            AnimalMoved?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnAnimalBorned()
        {
            AnimalBorned?.Invoke(this, EventArgs.Empty);
        }
    }
}
