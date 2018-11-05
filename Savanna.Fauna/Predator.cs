using System.Collections.Generic;
using Savanna.Interfaces;

namespace Savanna.Entities
{
    public class Predator : AnimalBase
    {
        public Predator(
            ISavannaFieldManager savanna,
            INotificator notificator,
            IPathfinder pathfinder,
            IRenderer renderer)
            : base(savanna, notificator, pathfinder, renderer)
        {
            data.IsPredator = true;
            data.Vision = 10;
            data.Type = "Lion";
        }

        private bool chase;
        private ICellBase target;
        private List<ICellBase> pathToTarget;

        public override void Behave()
        {
            if (CanAction)
            {
                CanAction = false;
                _pathfinder.ClearOldData();
                LookForAVictim();
                IdleOrChase();
                //OnAnimalMoved(data);
            }
        }

        private void LookForAVictim()
        {
            target = LookAroundFor<GrassEater>();
            chase = (target != null) ? true : false;
        }

        private void IdleOrChase()
        {
            if (chase)
            {
                MoveFromTo(target.xPos, target.yPos);
            }
            else
            {
                Idle();
            }
        }

        private void MoveFromTo(int x, int y)
        {
            pathToTarget = _pathfinder.MoveFromTo(_savanna.area.Field[xPos, yPos], _savanna.area.Field[x, y]);
            if (pathToTarget != null)
            {
                Chase();
            }
        }

        private void Chase()
        {
            pathToTarget.Reverse();
            pathToTarget.RemoveAt(0);

            for (int i = 0; i < data.RunSpeed; i++)
            {
                if (pathToTarget[0] is GrassEater)
                {
                    var victim = pathToTarget[0] as AnimalBase;
                    EatAndShow(victim);
                    pathToTarget.Clear();
                    break;
                }

                SwapTakeDamageAndShow(pathToTarget[0].xPos, pathToTarget[0].yPos);
                pathToTarget.Remove(pathToTarget[0]);
            }
            chase = false;
        }

        private void EatAndShow(AnimalBase victim)
        {
            Eat(victim);
            Show();
        }

        private void Eat(AnimalBase victim)
        {
            victim.TakeDamage(100f);
            data.Health += 30;
        }
    }
}
