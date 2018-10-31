using System.Collections.Generic;
using Savanna.Abstract;
using Savanna.Interfaces;

namespace Savanna.Fauna
{
    public class GrassEater : AnimalBase, IAnimal, IGrassEater
    {
        private bool run;
        private ICellBase runFrom;

        public GrassEater(
            int x, int y,
            int speed, int runSpeed,
            INotificator notificator,
            ISavannaField field,
            IPathfinder pathfinder,
            IRenderer renderer)
        : base(x, y, speed, runSpeed, notificator, field, pathfinder, renderer)
        {
            data.IsPredator = false;
            data.Vision = 4;
            data.Speed = speed;
        }

        public override void Behave()
        {
            if (CanAction)
            {
                CanAction = false;
                _pathfinder.ClearOldData();

                LookForAPredator();
                IdleOrRun();
                OnAnimalMoved(data);
            }
        }

        private void LookForAPredator()
        {
            runFrom = LookAroundFor<Predator>();
            run = (runFrom != null) ? true : false;
        }

        private void IdleOrRun()
        {
            if (run)
            {
                RunFrom();
            }
            else
            {
                Idle();
            }
        }

        private void RunFrom()
        {
            for (int i = 0; i <= data.RunSpeed; i++)
            {
                List<ICellBase> directions = PossibleDirections();
                ChooseBestWayToRun(directions);
            }
        }

        private void ChooseBestWayToRun(List<ICellBase> direction)
        {
            ICellBase winner = null;
            double distance = 0;

            for (int i = 0; i < direction.Count; i++)
            {
                var current = _pathfinder.Heuristic(direction[i], runFrom);
                if (current > distance)
                {
                    distance = current;

                    winner = direction[i];
                }
            }
            Swap(winner._x, winner._y);
        }

        public void TakeDamage()
        {
            _savanna.Field[_x, _y] = new Ground(_x, _y, _savanna);
        }
    }
}
