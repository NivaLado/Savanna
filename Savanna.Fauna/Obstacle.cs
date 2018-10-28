namespace Savanna.Fauna
{
    public class Obstacle : Ground
    {
        public Obstacle(int x, int y) : base(x, y)
        {
            IsObstacle = true;
        }
    }
}
