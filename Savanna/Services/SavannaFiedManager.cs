using Savanna.Abstract;
using Savanna.Fauna;
using Savanna.Models;
using Savanna.Rendering;

namespace Savanna.Services
{
    public class SavannaFieldManager
    {
        public SavannaField savanna = new SavannaField();

        public void GenerateEmptyField()
        {
            savanna.Field = new CellBase[savanna.Width, savanna.Height];
            for (int x = 0; x < savanna.Field.GetLength(0); x++)
            {
                for (int y = 0; y < savanna.Field.GetLength(1); y++)
                {
                    savanna.Field[x,y] = new Ground();

                    savanna.Field[x, y].x = x;
                    savanna.Field[x, y].y = y;
                }
            }
        }

        public void AddNeightbors()
        {
            for (int x = 0; x < savanna.Field.GetLength(0); x++)
            {
                for (int y = 0; y < savanna.Field.GetLength(1); y++)
                {
                    savanna.Field[x, y].AddNeighbords(savanna);
                }
            }
        }

        public void AddLionToField()
        {
            savanna.Field[21, 21] = new Predator(new GameNotifications(), savanna, ConsoleRenderer.GetInstance());
        }
    }
}
