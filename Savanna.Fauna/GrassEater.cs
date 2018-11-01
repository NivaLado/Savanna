using System.Collections.Generic;
using Savanna.Abstract;
using Savanna.Constants;
using Savanna.Interfaces;

namespace Savanna.Fauna
{
    public class GrassEater : AnimalBase
    {
        private ICellBase runFrom;
        private bool run;

        public GrassEater(
            int x, int y,
            INotificator notificator,
            ISavannaField field,
            IPathfinder pathfinder,
            IRenderer renderer)
        : base(x, y, notificator, field, pathfinder, renderer)
        {
            data.IsPredator = false;
            data.Vision = 6;
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
                var current = _pathfinder.GetDistance(direction[i], runFrom);
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
            _renderer.DrawGame(_savanna, Globals.XOffset, Globals.YOffset);
        }
    }
}
