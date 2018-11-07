using Savanna.Interfaces;

namespace Savanna.Entities
{
    public class Obstacle : Ground
    {
        public Obstacle(ISavannaFieldManager savanna) : base(savanna)
        {
            IsObstacle = true;
            data.DisplayLetter = "x";
            data.color = System.ConsoleColor.White;
        }
    }
}
