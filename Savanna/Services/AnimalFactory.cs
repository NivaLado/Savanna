using Savanna.Abstract;
using Savanna.Fauna;
using Savanna.Rendering;

namespace Savanna.Services
{
    public class AnimalFactory
    {

        static public AnimalBase CreateAnimal(int input)
        {
            if (input == 1)
            {
                return new Predator(
                    0, 0,
                    new GameNotifications(),
                    SavannaFieldManager.GetInstance().savanna,
                    AStarPathfinding.GetInstance(),
                    ConsoleRenderer.GetInstance()
                    );
            }
            else if (input == 2)
            {
                return new GrassEater(
                    0, 0,
                    new GameNotifications(),
                    SavannaFieldManager.GetInstance().savanna,
                    AStarPathfinding.GetInstance(),
                    ConsoleRenderer.GetInstance()
                    );
            }
            else
            {
                return null;
            }
        }
    }
}
