using System.Collections.Generic;
using Savanna.Constants;
using Savanna.Interfaces;

namespace Savanna.Entities
{
    public class GrassEater : AnimalBase
    {
        private ICellBase runFrom;
        private bool run;
        private int breeding = 0;

        public GrassEater(
            ISavannaFieldManager savanna,
            INotificator notificator,
            IPathfinder pathfinder,
            IRenderer renderer)
        : base(savanna, notificator, pathfinder, renderer)
        {
            data.IsPredator = false;
            data.Vision = 6;
            data.Type = "Antelope";
        }

        public override void Behave()
        {
            if (CanAction)
            {
                CanAction = false;
                _pathfinder.ClearOldData();
                LookForAPredator();
                IdleOrRun();
                BreedAndShow();
                //OnAnimalMoved(data);
            }
        }

        private void LookForAPredator()
        {
            runFrom = LookAroundFor<Predator>();
            run = (runFrom != null) ? true : false;
        }

        private void BreedAndShow()
        {
            if (!IsAlive())
                return;
            var sameType = LookAroundFor<GrassEater>(true);
            breeding = (sameType != null) ? ++breeding : 0;
            Breed();
            Show();
        }

        private void Breed()
        {
            if (breeding == 3)
            {
                List<ICellBase> area = new List<ICellBase>();
                area = AreaVision<GrassEater>(area);
                foreach (var item in area)
                {
                    if (item is Ground)
                    {
                        _savanna.CreateAndAddAnimalToTheFieldAt(AnimalTypes.Antelope, item.xPos, item.yPos);
                        var animal = _savanna.Area.Field[item.xPos, item.yPos] as GrassEater;
                        OnAnimalBorned(animal.data);
                        break;
                    }
                }
                breeding = 0;
            }
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
            if (winner != null)
            {
                SwapTakeDamageAndShow(winner.xPos, winner.yPos);
            }
        }
    }
}
