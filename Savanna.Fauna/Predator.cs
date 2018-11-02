using System.Collections.Generic;
using Savanna.Abstract;
using Savanna.Interfaces;

namespace Savanna.Fauna
{
    public class Predator : AnimalBase
    {
        public Predator(
            ISavannaField savanna,
            INotificator notificator,
            IPathfinder pathfinder,
            IRenderer renderer)
            : base(savanna, notificator, pathfinder, renderer)
        {
            data.IsPredator = true;
            data.Vision = 10;
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
                OnAnimalMoved(data);
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
            pathToTarget = _pathfinder.MoveFromTo(_savanna.Field[xPos, yPos], _savanna.Field[x, y]);
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
                    var victim = pathToTarget[0] as GrassEater;
                    victim.TakeDamageAndShow();
                    pathToTarget.Clear();
                    break;
                }

                SwapAndShow(pathToTarget[0].xPos, pathToTarget[0].yPos);
                pathToTarget.Remove(pathToTarget[0]);
            }

            chase = false;
        }
    }
}
