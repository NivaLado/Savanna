using System;
using System.Collections.Generic;
using Savanna.Constants;
using Savanna.Interfaces;
using Savanna.Models;

namespace Savanna.Entities
{
    public abstract class AnimalBase : CellBase
    {
        public event EventHandler<AnimalEventArgs> AnimalBorned;
        public event EventHandler<AnimalEventArgs> AnimalMoved;
        public event EventHandler<AnimalEventArgs> AnimalDied;

        protected INotificator _notificator;
        protected IPathfinder _pathfinder;
        protected IRenderer _renderer;

        public IAnimalData data;
        static int id = 0;

        public AnimalBase(
            ISavannaFieldManager savanna,
            INotificator notificator,
            IPathfinder pathfinder,
            IRenderer renderer) : base(savanna)
        {
            data = new AnimalData()
            {
                ID = id++,
                RunSpeed = 10,
                Speed = 5,
                Health = 30
            };

            _notificator = notificator;
            _pathfinder = pathfinder;
            _renderer = renderer;

            AnimalMoved += _notificator.OnAnimalMoved;
            AnimalBorned += _notificator.OnAnimalBorned;
            AnimalDied += _notificator.OnAnimalDied;
        }

        public ICellBase LookAroundFor<AnimalType>(bool breeding = false)
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
                    if (breeding)
                    {
                        _renderer.DrawAtXyWithColor(item.xPos, item.yPos, ConsoleColor.Magenta);
                    }
                    else
                    {
                        _renderer.DrawAtXyWithColor(item.xPos, item.yPos, ConsoleColor.Blue);
                    }
                }
            }
            System.Threading.Thread.Sleep(Globals.VisionDelay);
            #endregion

            return target;
        }

        public void Idle()
        {
            Random random = new Random();
            int steps = 0;

            while (steps != data.Speed)
            {
                List<ICellBase> direction = PossibleDirections();
                if (direction.Capacity == 0)
                {
                    break;
                }
                int index = random.Next(0, direction.Count);
                SwapTakeDamageAndShow(direction[index].xPos, direction[index].yPos);
                steps++;
            }
        }

        public void SwapTakeDamageAndShow(int x, int y)
        {
            Swap(x, y);
            TakeDamage(Globals.ExhaustDamage);
            Show();
        }

        public List<ICellBase> PossibleDirections()
        {
            List<ICellBase> direction = new List<ICellBase>();
            var field = _savanna.area.Field;
            if (xPos < _savanna.area.Width - 1)
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
            if (yPos < _savanna.area.Height - 1)
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

        public void TakeDamage(float damage)
        {
            data.Health -= damage;
            if (data.Health <= 0)
            {
                Die();
            }
        }

        public void Show()
        {
            _renderer.DrawGame(_savanna.area, Globals.XOffset, Globals.YOffset);
        }

        public List<ICellBase> AreaVision<AnimalType>(List<ICellBase> vision)
        {
            for (int x = 0; x < _savanna.area.Width; x++)
            {
                for (int y = 0; y < _savanna.area.Height; y++)
                {
                    var euclidean = _pathfinder.GetDistance(this, _savanna.area.Field[x, y]);
                    if (euclidean <= data.Vision)
                    {
                        if (!_savanna.area.Field[x, y].IsObstacle)
                            vision.Add(_savanna.area.Field[x, y]);
                    }
                }
            }
            return vision;
        }

        private void Swap(int x, int y)
        {
            var newLocation = _savanna.area.Field[x, y];
            if (!newLocation.IsObstacle)
            {
                _savanna.area.Field[x, y] = this;
                _savanna.area.Field[xPos, yPos] = newLocation;
                int tempX = xPos; int tempY = yPos;
                xPos = newLocation.xPos;
                yPos = newLocation.yPos;
                newLocation.xPos = tempX;
                newLocation.yPos = tempY;
                System.Threading.Thread.Sleep(Globals.SwapDelay);
            }
        }

        private void Die()
        {
            OnAnimalDied(data);
            _savanna.area.Field[xPos, yPos] = new Ground(_savanna);
        }

        private bool Obstacle(int x, int y)
        {
            return _savanna.area.Field[xPos + x, yPos + y].IsObstacle;
        }

        protected virtual void OnAnimalBorned(IAnimalData data)
        {
            AnimalBorned?.Invoke(this, new AnimalEventArgs() { AnimalData = data });
        }

        protected virtual void OnAnimalMoved(IAnimalData data)
        {
            AnimalMoved?.Invoke(this, new AnimalEventArgs() { AnimalData = data });
        }

        protected virtual void OnAnimalDied(IAnimalData data)
        {
            AnimalDied?.Invoke(this, new AnimalEventArgs() { AnimalData = data });
        }
    }
}
