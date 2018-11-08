using System.Collections.Generic;
using Savanna.Entities.Interfaces;

namespace Savanna.Entities
{
    public class Predator : AnimalBase, IAnimal
    {
        public Predator(
            ISavannaFieldManager savanna,
            INotificator notificator,
            IPathfinder pathfinder,
            IRenderer renderer)
            : base(savanna, notificator, pathfinder, renderer)
        { }

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
                OnAnimalStartRunning(data);
                MoveFromTo(target.xPos, target.yPos);
            }
            else
            {
                OnAnimalStartMoving(data);
                Idle();
            }
        }

        private void MoveFromTo(int x, int y)
        {
            pathToTarget = _pathfinder.MoveFromTo(_savanna.Area.Field[xPos, yPos], _savanna.Area.Field[x, y]);
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
