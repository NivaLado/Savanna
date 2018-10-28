using Savanna.Fauna;
using Savanna.Flora.Models;
using Savanna.Fauna.Interfaces;
using Savanna.EventHandlers;

namespace Savanna.Flora
{
    public class SavannaFieldManager
    {
        public SavannaField savanna = new SavannaField();

        public void GenerateEmptyField()
        {
            savanna.Field = new IAnimal[savanna.Width, savanna.Height];
            for (int i = 0; i < savanna.Field.GetLength(0); i++)
            {
                for (int j = 0; j < savanna.Field.GetLength(1); j++)
                {
                    savanna.Field[i,j] = null;
                }
            }
        }

        public void AddLionToField()
        {
            savanna.Field[0, 0] = new Lion(new GameNotifications());
        }
    }
}
