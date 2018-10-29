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
            //Idle();
            Test();
            OnAnimalMoved(data);
        }

        public void Idle()
        {
            for (int i = 0; i < neighbors.Count; i++)
            {
                if(!neighbors[i].IsObstacle)
                {
                    _savanna.Field[_x, _y] = _savanna.Field[neighbors[i]._x, neighbors[i]._y];

                    _x = neighbors[i]._x;
                    _y = neighbors[i]._y;

                    break;
                }
            }
        }

        public void Test()
        {
            var temp = _savanna.Field[_x - 5, _y];

            _savanna.Field[_x - 5, _y] = this;
            _savanna.Field[_x, _y] = temp;
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
