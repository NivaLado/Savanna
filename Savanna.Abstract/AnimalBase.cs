using System;
using System.Collections.Generic;
using Savanna.Constants;
using Savanna.Interfaces;
using Savanna.Models;

namespace Savanna.Abstract
{
    public abstract class AnimalBase : CellBase
    {
        public event EventHandler<AnimalEventArgs> AnimalBorned;
        public event EventHandler<AnimalEventArgs> AnimalMoved;

        protected INotificator _notificator;
        protected ISavannaField _savanna;
        protected IPathfinder _pathfinder;
        protected IRenderer _renderer;
        protected IAnimalData data;
        static int id = 0;

        public AnimalBase(
            int x, int y,
            int speed, int runSpeed,
            INotificator notificator,
            ISavannaField savanna,
            IPathfinder pathfinder,
            IRenderer renderer)
            : base(x, y)
        {
            id++;
            data = new AnimalData()
            {
                ID = id,
                RunSpeed = runSpeed,
                Speed = speed,
            };


            _savanna = savanna;
            _notificator = notificator;
            _pathfinder = pathfinder;
            _renderer = renderer;

            AnimalMoved += notificator.OnAnimalMoved;
            AnimalBorned += notificator.OnAnimalBorned;

            OnAnimalBorned(data);
        }

        public ICellBase LookAroundFor<AnimalType>()
        {
            ICellBase target = null;
            List<ICellBase> vision = new List<ICellBase>();
            vision = VisualizeRecursiveVision<AnimalType>(neighbors, vision, data.Vision, 0);

            foreach (var item in vision)
            {
                if (item is AnimalType)
                {
                    target = item;
                }
            }

            //#region Visualization
            //foreach (var item in vision)
            //{
            //    if (item is AnimalType)
            //    {
            //        _renderer.DrawAtXyWithColor(item._x, item._y, ConsoleColor.DarkRed);
            //    }
            //    else if (item == this)
            //    {

            //    }
            //    else
            //    {
            //        _renderer.DrawAtXyWithColor(item._x, item._y, ConsoleColor.Blue);
            //    }
            //}
            //System.Threading.Thread.Sleep(Globals.VisionDelay);
            //#endregion

            return target;
        }

        public List<ICellBase> VisualizeRecursiveVision<T>(
                List<ICellBase> myNeighbors,
                List<ICellBase> vision,
                int stop, int pass)
        {
            if (stop == pass)
                return vision;
            pass++;
            foreach (var item in myNeighbors)
            {
                if (!vision.Contains(item))
                {
                    if (item is T)
                    {
                        vision.Add(item);
                        return vision;
                    }
                    else if (!item.IsObstacle)
                    {
                        vision.Add(item);
                        VisualizeRecursiveVision<T>(item.neighbors, vision, stop, pass);
                    }
                }
            }
            return vision;
        }

        public void Idle()
        {
            Random random = new Random();
            int steps = 0;

            while (steps != data.Speed)
            {
                List<ICellBase> direction = PossibleDirections();
                int index = random.Next(0, direction.Count);
                Swap(direction[index]._x, direction[index]._y);
                steps++;
            }
        }

        public void Swap(int x, int y)
        {
            var newLocation = _savanna.Field[x, y];
            if (!newLocation.IsObstacle)
            {
                _savanna.Field[x, y] = this;
                _savanna.Field[_x, _y] = newLocation;
                int tempX = _x; int tempY = _y;

                _x = newLocation._x;
                _y = newLocation._y;

                newLocation._x = tempX;
                newLocation._y = tempY;
                _renderer.DrawGame(_savanna, Globals.XOffset, Globals.YOffset); //Test
                System.Threading.Thread.Sleep(Globals.SwapDelay);
            }
        }

        public List<ICellBase> PossibleDirections()
        {
            List<ICellBase> direction = new List<ICellBase>();
            var field = _savanna.Field;
            if (_x < _savanna.Width - 1)
            {
                if (!Obstacle(1, 0))
                {
                    direction.Add(field[_x + 1, _y]);
                }
            }
            if (_x > 0)
            {
                if (!Obstacle(-1, 0))
                {
                    direction.Add(field[_x - 1, _y]);
                }
            }
            if (_y < _savanna.Height - 1)
            {
                if (!Obstacle(0, 1))
                {
                    direction.Add(field[_x, _y + 1]);
                }
            }
            if (_y > 0)
            {
                if (!Obstacle(0, -1))
                {
                    direction.Add(field[_x, _y - 1]);
                }
            }
            return direction;
        }

        private bool Obstacle(int x, int y)
        {
            return _savanna.Field[_x + x, _y + y].IsObstacle;
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
