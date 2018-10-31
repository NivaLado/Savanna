using Savanna.Interfaces;

namespace Savanna.Fauna
{
    public class Obstacle : Ground
    {
        public Obstacle(int x, int y, ISavannaField savanna) : base(x, y, savanna)
        {
            IsObstacle = true;
        }
    }
}
