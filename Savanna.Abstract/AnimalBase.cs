using System;
using Savanna.Models;
using Savanna.Interfaces;
using System.Threading;
using System.Collections.Generic;

namespace Savanna.Abstract
{
    public abstract class AnimalBase : CellBase
    {
        public event EventHandler<AnimalEventArgs> AnimalBorned;
        public event EventHandler<AnimalEventArgs> AnimalMoved;

        readonly INotificator _notificator;

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
            _renderer  = renderer;

            AnimalMoved += notificator.OnAnimalMoved;
            AnimalBorned += notificator.OnAnimalBorned;

            OnAnimalBorned(data);
        }

        public void LookAround<AnimalType>()
        {
            List<ICellBase> vision = new List<ICellBase>();
            vision = RecursiveVision<AnimalType>(neighbors, vision, data.Vision, 0);
            
            #region Visualization
            foreach (var item in vision)
            {
                _renderer.DrawAtXyWithColor(item._x, item._y, ConsoleColor.Blue);
            }
            Thread.Sleep(200);
            #endregion
        }

        public List<ICellBase> RecursiveVision<T>(
                        List<ICellBase> myNeighbors, 
                        List<ICellBase> vision, int stop, int pass)
        {
            if (stop == pass)
                return vision;
            pass++;
            foreach (var item in myNeighbors)
            {
                if(item is T)
                {
                    vision.Add(item);
                    Console.WriteLine("Spoted " + item +  "at " + item._x);
                    return vision;
                }
                else if(!vision.Contains(item) && !item.IsObstacle)
                {
                    vision.Add(item);
                }
                RecursiveVision<T>(item.neighbors, vision, stop, pass);
            }
            return vision;
        }

        public void Idle()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int number = random.Next(0, 4);


            switch (number)
            {
                case 0:
                    MoveRight();
                    break;
                case 1:
                    MoveLeft();
                    break;
                case 2:
                    MoveTop();
                    break;
                case 3:
                    MoveDown();
                    break;
            }
        }

        public void Swap(int x, int y)
        {
           var newLocation = _savanna.Field[_x + x, _y + y];

            if(!newLocation.IsObstacle)
            {
                _savanna.Field[_x + x, _y + y] = this;
                _savanna.Field[_x, _y] = newLocation;

                int tempX = _x; int tempY = _y;
                double tempG = g; double tempH = h;
                double tempF = f; var tempNeighbors = neighbors; 

                _x = newLocation._x;
                _y = newLocation._y;
                g = newLocation.g;
                h = newLocation.h;
                f = newLocation.f;
                //neighbors = newLocation.neighbors;

                newLocation._x = tempX;
                newLocation._y = tempY;
                newLocation.g = tempG;
                newLocation.h = tempH;
                newLocation.f = tempF;
                //newLocation.neighbors = tempNeighbors;
            }
        }

        public void MoveRight()
        {
            if (_x < _savanna.Width - 1)
            {
                Swap(1, 0);
            }
        }

        public void MoveLeft()
        {
            if (_x > 0)
            {
                Swap(-1, 0);
            }
        }

        public void MoveTop()
        {
            if (_y < _savanna.Height - 1)
            {
                Swap(0, 1);
            }
        }

        public void MoveDown()
        {
            if (_y > 0)
            {
                Swap(0, -1);
            }
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
