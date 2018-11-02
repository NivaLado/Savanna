using Savanna.Interfaces;

namespace Savanna.Fauna
{
    public class Obstacle : Ground
    {
        public Obstacle(ISavannaField savanna) : base(savanna)
        {
            IsObstacle = true;
        }
    }
}
