using Savanna.Abstract;
using Savanna.Interfaces;
using System;

namespace Savanna.Fauna
{
    public class Predator : AnimalBase, IAnimal
    {

        public Predator(int x, int y, INotificator notificator, ISavannaField field, IPathfinder pathfinder) 
            : base(x, y, notificator, field, pathfinder)
        {
            data.IsPredator = true;
        }

        public override void Behave()
        {
            Move();
        }

    }
}
