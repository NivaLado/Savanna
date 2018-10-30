using System;
using System.Collections.Generic;
using System.Threading;
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
            data.Vision = 7;
        }

        private bool chase;
        private ICellBase target;
        private List<ICellBase> pathToTarget;

        public override void Behave()
        {
            if(CanAction)
            {
                CanAction = false;

                LookForAVictim();
                Thread.Sleep(300);
                IdleOrChase();
                Thread.Sleep(300);

                _renderer.DrawGame(_savanna, 1, 1);
                _pathfinder.ClearOldData();
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
            if(chase)
            {
                MoveFromTo(target._x,target._y);
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
            //if(newPath)
            pathToTarget.Reverse();
            pathToTarget.RemoveAt(0);

            for (int i = 0; i < pathToTarget.Count; i++)
            {
                Swap(pathToTarget[0]._x, pathToTarget[0]._y);
                pathToTarget.Remove(pathToTarget[0]);

                _renderer.DrawGame(_savanna, 1, 1);
                Thread.Sleep(100);
            }
        }
    }
}
