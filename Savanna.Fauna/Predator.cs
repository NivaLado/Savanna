using System.Collections.Generic;
using Savanna.Abstract;
using Savanna.Interfaces;

namespace Savanna.Fauna
{
    public class Predator : AnimalBase, IAnimal
    {
        public Predator(
            int x, int y,
            int speed, int runSpeed,
            INotificator notificator,
            ISavannaField field,
            IPathfinder pathfinder,
            IRenderer renderer)
            : base(x, y, speed, runSpeed, notificator, field, pathfinder, renderer)
        {
            data.IsPredator = true;
            data.Vision = 10;
            data.Speed = speed;
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
                MoveFromTo(target._x, target._y);
            }
            else
            {
                Idle();
            }
        }

        private void MoveFromTo(int x, int y)
        {
            pathToTarget = _pathfinder.MoveFromTo(_savanna.Field[_x, _y], _savanna.Field[x, y]);
            Chase();
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
                    victim.TakeDamage();
                    pathToTarget.Clear();
                    break;
                }

                Swap(pathToTarget[0]._x, pathToTarget[0]._y);
                pathToTarget.Remove(pathToTarget[0]);
            }

            chase = false;
        }
    }
}
