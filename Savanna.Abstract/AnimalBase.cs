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
        protected IPathfinder _pathfinder;
        protected IRenderer _renderer;

        public IAnimalData data;
        static int id = 0;

        public AnimalBase(
            ISavannaField savanna,
            INotificator notificator,
            IPathfinder pathfinder,
            IRenderer renderer) : base(savanna)
        {
            data = new AnimalData()
            {
                ID = id++,
                RunSpeed = 10,
                Speed = 5,
            };

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
            vision = AreaVision<AnimalType>(vision);

            foreach (var item in vision)
            {
                if (item is AnimalType)
                {
                    target = item;
                }
            }
            #region Visualization
            foreach (var item in vision)
            {
                if (item is AnimalType)
                {
                    _renderer.DrawAtXyWithColor(item.xPos, item.yPos, ConsoleColor.DarkRed);
                }
                else if (item == this)
                {

                }
                else
                {
                    _renderer.DrawAtXyWithColor(item.xPos, item.yPos, ConsoleColor.Blue);
                }
            }
            System.Threading.Thread.Sleep(Globals.VisionDelay);
            #endregion

            return target;
        }

        public List<ICellBase> AreaVision<AnimalType>(List<ICellBase> vision)
        {
            for (int x = 0; x < _savanna.Width; x++)
            {
                for (int y = 0; y < _savanna.Height; y++)
                {
                    var euclidean = _pathfinder.GetDistance(this, _savanna.Field[x, y]);
                    if (euclidean <= data.Vision)
                    {
                        if (!_savanna.Field[x, y].IsObstacle)
                            vision.Add(_savanna.Field[x, y]);
                    }

                    //Testing
                    //var manhattan = _pathfinder.Heuristic(this, _savanna.Field[x, y]);
                    //if (manhattan <= data.Vision)
                    //{
                    //    if (!_savanna.Field[x, y].IsObstacle)
                    //        vision.Add(_savanna.Field[x, y]);
                    //}
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
                SwapAndShow(direction[index].xPos, direction[index].yPos);
                steps++;
            }
        }

        public void SwapAndShow(int x, int y)
        {
            var newLocation = _savanna.Field[x, y];
            if (!newLocation.IsObstacle)
            {
                _savanna.Field[x, y] = this;
                _savanna.Field[xPos, yPos] = newLocation;
                int tempX = xPos; int tempY = yPos;

                xPos = newLocation.xPos;
                yPos = newLocation.yPos;

                newLocation.xPos = tempX;
                newLocation.yPos = tempY;
                System.Threading.Thread.Sleep(Globals.SwapDelay);
            }

            Show();
        }

        public List<ICellBase> PossibleDirections()
        {
            List<ICellBase> direction = new List<ICellBase>();
            var field = _savanna.Field;
            if (xPos < _savanna.Width - 1)
            {
                if (!Obstacle(1, 0))
                {
                    direction.Add(field[xPos + 1, yPos]);
                }
            }
            if (xPos > 0)
            {
                if (!Obstacle(-1, 0))
                {
                    direction.Add(field[xPos - 1, yPos]);
                }
            }
            if (yPos < _savanna.Height - 1)
            {
                if (!Obstacle(0, 1))
                {
                    direction.Add(field[xPos, yPos + 1]);
                }
            }
            if (yPos > 0)
            {
                if (!Obstacle(0, -1))
                {
                    direction.Add(field[xPos, yPos - 1]);
                }
            }
            return direction;
        }

        private bool Obstacle(int x, int y)
        {
            return _savanna.Field[xPos + x, yPos + y].IsObstacle;
        }

        public void Show()
        {
            _renderer.DrawGame(_savanna, Globals.XOffset, Globals.YOffset);
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
