using Savanna.Abstract;
using Savanna.Interfaces;

namespace Savanna.Fauna
{
    public class Predator : AnimalBase, IAnimal
    {
        public Predator(int x, int y, 
            INotificator notificator, 
            ISavannaField field, 
            IPathfinder pathfinder) 
            : base(x, y, notificator, field, pathfinder)
        {
            data.IsPredator = true;
            data.Speed = 1;
        }

        public override void Behave()
        {
            MoveFromTo();
            Move();
        }

        private void MoveFromTo()
        {
            int width = _savanna.Field.GetLength(0);
            int height = _savanna.Field.GetLength(1);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if(_savanna.Field[x, y] is GrassEater)
                        _pathfinder.MoveFromTo(_savanna.Field[_x, _y], _savanna.Field[x, y]);
                }
            }
        }
    }
}
