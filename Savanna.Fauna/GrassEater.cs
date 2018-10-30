using Savanna.Abstract;
using Savanna.Interfaces;

namespace Savanna.Fauna
{
    public class GrassEater : AnimalBase, IAnimal, IGrassEater
    {
        public GrassEater(
            int x, int y, 
            int speed, int runSpeed,
            INotificator notificator,
            ISavannaField field, 
            IPathfinder pathfinder,
            IRenderer renderer)
        : base(x, y, speed, runSpeed, notificator, field, pathfinder, renderer)
        {
            data.IsPredator = false;
            data.Vision = 3;
        }

        public override void Behave()
        {
            if (CanAction)
            {
                CanAction = false;
                LookAround<Predator>();
                Idle();

                _renderer.DrawGame(_savanna, 1, 1);
                _pathfinder.ClearOldData();
                OnAnimalMoved(data);
            }
        }
    }
}
