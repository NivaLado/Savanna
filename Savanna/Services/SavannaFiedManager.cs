using Savanna.Abstract;
using Savanna.Fauna;
using Savanna.Models;
using Savanna.Rendering;
using System;

namespace Savanna.Services
{
    public class SavannaFieldManager
    {

        #region Singleton
        private static readonly Lazy<SavannaFieldManager> lazy =
                            new Lazy<SavannaFieldManager>(() => new SavannaFieldManager());
        public string Name { get; private set; }

        private SavannaFieldManager()
        {
            Name = Guid.NewGuid().ToString();
        }

        public static SavannaFieldManager GetInstance()
        {
            return lazy.Value;
        }
        #endregion

        public static SavannaField savanna = new SavannaField();

        public void GenerateEmptyField()
        {
            savanna.Field = new CellBase[savanna.Width, savanna.Height];
            for (int x = 0; x < savanna.Field.GetLength(0); x++)
            {
                for (int y = 0; y < savanna.Field.GetLength(1); y++)
                {
                    savanna.Field[x, y] = new Ground(x, y);
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

        public void CreateAndAddAnimalToTheField(int x, int y)
        {
            var animal = new Predator(
                x,y,
                new GameNotifications(),
                savanna,
                AStarPathfinding.GetInstance());

            savanna.Field[x, y] = animal;

        }
    }
}
