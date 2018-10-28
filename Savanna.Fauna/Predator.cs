using Savanna.Abstract;
using Savanna.Interfaces;
using System;

namespace Savanna.Fauna
{
    public class Predator : AnimalBase, IAnimal
    {

        public Predator(INotificator notificator, ISavannaField field, IRenderer renderer) 
            : base(notificator, field, renderer)
        {
            data.IsPredator = true;
        }

        public override void Behave()
        {
            Move();
        }

    }
}
