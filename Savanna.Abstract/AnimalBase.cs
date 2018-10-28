using System;
using Savanna.Models;
using Savanna.Interfaces;

namespace Savanna.Abstract
{
    public abstract class AnimalBase : CellBase
    {
        public event EventHandler<AnimalEventArgs> AnimalBorned;
        public event EventHandler<AnimalEventArgs> AnimalMoved;

        readonly INotificator _notificator;

        protected ISavannaField _savanna;
        protected IPathfinder _pathfinder;
        protected IAnimalData data;
        static int id = 0;

        public AnimalBase(int x, int y,INotificator notificator, ISavannaField savanna, IPathfinder pathfinder) 
            : base(x, y)
        {
            id++;
            data = new AnimalData() { ID = id };

            _savanna = savanna;
            _notificator = notificator;
            _pathfinder = pathfinder;

            AnimalMoved += notificator.OnAnimalMoved;
            AnimalBorned += notificator.OnAnimalBorned;

            OnAnimalBorned(data);
        }

        public void Move()
        {
            _pathfinder.MoveFromTo(_savanna.Field[_x, _y], _savanna.Field[0, 0]);

            OnAnimalMoved(data);
        }

        public void DefinePosition(ICellBase position)
        {
            data.Position = position;
            var t = _x; 
        }

        protected virtual void OnAnimalBorned(IAnimalData data)
        {
            AnimalBorned?.Invoke(this, new AnimalEventArgs() { AnimalData = data });
        }

        protected virtual void OnAnimalMoved(IAnimalData data)
        {
            AnimalMoved?.Invoke(this, new AnimalEventArgs() { AnimalData = data });
        }
    }
}
