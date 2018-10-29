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
            IPathfinder pathfinder,
            IRenderer renderer)
        : base(x, y, notificator, field, pathfinder, renderer)
        {
            data.IsPredator = false;
        }

        public override void Behave()
        {
            if (CanAction)
            {
                Move();
            }
        }
    }
}
