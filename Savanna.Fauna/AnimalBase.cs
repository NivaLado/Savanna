using System;
using System.Collections.Generic;
using Savanna.Constants;
using Savanna.Entities.Interfaces;

namespace Savanna.Entities
{
    public abstract class AnimalBase : CellBase
    {
        public event EventHandler<AnimalEventArgs> AnimalBorned;
        public event EventHandler<AnimalEventArgs> AnimalMoving;
        public event EventHandler<AnimalEventArgs> AnimalRunning;
        public event EventHandler<AnimalEventArgs> AnimalDied;

        protected INotificator _notificator;
        protected IPathfinder _pathfinder;
        protected IRenderer _renderer;

        public AnimalBase(
            ISavannaFieldManager savanna,
            INotificator notificator,
            IPathfinder pathfinder,
            IRenderer renderer) : base(savanna)
        {
            _notificator = notificator;
            _pathfinder = pathfinder;
            _renderer = renderer;

            AnimalMoving += _notificator.OnAnimalStartMoving;
            AnimalRunning += _notificator.OnAnimalStartRunning;
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
            if (data.Health > 0)
            {
                Swap(x, y);
                TakeDamage(Globals.ExhaustDamage);
                Show();
            }
        }

        public List<ICellBase> PossibleDirections()
        {
            List<ICellBase> direction = new List<ICellBase>();
            var field = _savanna.Area.Field;
            if (xPos < _savanna.Area.Width - 1)
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
            if (yPos < _savanna.Area.Height - 1)
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
            _renderer.DrawGame(_savanna.Area, Globals.XOffset, Globals.YOffset);
        }

        public List<ICellBase> AreaVision<AnimalType>(List<ICellBase> vision)
        {
            for (int x = 0; x < _savanna.Area.Width; x++)
            {
                for (int y = 0; y < _savanna.Area.Height; y++)
                {
                    var euclidean = _pathfinder.GetDistance(this, _savanna.Area.Field[x, y]);
                    if (euclidean <= data.Vision)
                    {
                        if (!_savanna.Area.Field[x, y].IsObstacle)
                            vision.Add(_savanna.Area.Field[x, y]);
                    }
                }
            }
            return vision;
        }

        private void Swap(int x, int y)
        {
            var newLocation = _savanna.Area.Field[x, y];
            if (!newLocation.IsObstacle)
            {
                _savanna.Area.Field[x, y] = this;
                _savanna.Area.Field[xPos, yPos] = newLocation;
                int tempX = xPos; int tempY = yPos;
                xPos = newLocation.xPos;
                yPos = newLocation.yPos;
                newLocation.xPos = tempX;
                newLocation.yPos = tempY;
                System.Threading.Thread.Sleep(Globals.SwapDelay);
            }
        }

        protected bool IsAlive()
        {
            return data.Health > 0;
        }

        private void Die()
        {
            OnAnimalDied(data);
            _savanna.Area.Field[xPos, yPos] = new Ground(_savanna);
            _savanna.Area.Field[xPos, yPos].SetPosition(xPos, yPos);
        }

        private bool Obstacle(int x, int y)
        {
            return _savanna.Area.Field[xPos + x, yPos + y].IsObstacle;
        }

        protected virtual void OnAnimalBorned(IEntityData data)
        {
            AnimalBorned?.Invoke(this, new AnimalEventArgs() { AnimalData = data });
        }

        protected virtual void OnAnimalStartMoving(IEntityData data)
        {
            AnimalMoving?.Invoke(this, new AnimalEventArgs() { AnimalData = data });
        }

        protected virtual void OnAnimalStartRunning(IEntityData data)
        {
            AnimalRunning?.Invoke(this, new AnimalEventArgs() { AnimalData = data });
        }

        protected virtual void OnAnimalDied(IEntityData data)
        {
            AnimalDied?.Invoke(this, new AnimalEventArgs() { AnimalData = data });
        }
    }
}
