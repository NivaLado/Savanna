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

        public override void Behave()
        {
            if(CanAction)
            {
                CanAction = false;
                LookAround<GrassEater>();
                Thread.Sleep(500);
                Idle();

                MoveFromTo();
                _renderer.DrawGame(_savanna, 1, 1);
                _pathfinder.ClearOldData();
                OnAnimalMoved(data);
            }
        }

        private void MoveFromTo()
        {
            int width = _savanna.Field.GetLength(0);
            int height = _savanna.Field.GetLength(1);
            bool oneTarget = true;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if(_savanna.Field[x, y] is GrassEater && oneTarget)
                    {
                        _pathfinder.MoveFromTo(_savanna.Field[_x, _y], _savanna.Field[x, y]);
                        oneTarget = false;
                    }
                }
            }
        }
    }
}
