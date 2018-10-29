﻿using System;
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
            if(CanAction)
            {
                CanAction = false;
                Idle();
                OnAnimalMoved(data);
            }
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

                _x = newLocation._x;
                _y = newLocation._y;

                newLocation._x = tempX;
                newLocation._y = tempY;
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
