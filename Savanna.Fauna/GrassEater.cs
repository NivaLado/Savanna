using Savanna.Abstract;
using Savanna.Interfaces;
using System;

namespace Savanna.Fauna
{
    public class GrassEater : AnimalBase, IAnimal
    {
        public GrassEater(
            int x, 
            int y, 
            INotificator notificator,
            ISavannaField field, 
            IPathfinder pathfinder)
        : base(x, y, notificator, field, pathfinder)
        {
            data.IsPredator = false;
        }

        public override void Behave()
        {
            Move();
        }
    }
}
