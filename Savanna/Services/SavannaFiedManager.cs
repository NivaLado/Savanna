using Savanna.Abstract;
using Savanna.Constants;
using Savanna.Fauna;
using Savanna.Models;
using Savanna.Rendering;
using System;

namespace Savanna.Services
{
    public class SavannaFieldManager : ISavannaFieldManager
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

        public SavannaField savanna { get; set; }

        public void GenerateEmptyField()
        {
            savanna = new SavannaField();
            savanna.Field = new CellBase[savanna.Width, savanna.Height];
            for (int x = 0; x < savanna.Field.GetLength(0); x++)
            {
                for (int y = 0; y < savanna.Field.GetLength(1); y++)
                {
                    savanna.Field[x, y] = new Ground(x, y);
                }
            }
        }

        public void AddNeighbors()
        {
            for (int x = 0; x < savanna.Field.GetLength(0); x++)
            {
                for (int y = 0; y < savanna.Field.GetLength(1); y++)
                {
                    savanna.Field[x, y].AddNeighbords(savanna);
                }
            }
        }

        public void ClearNeightbors()
        {
            for (int x = 0; x < savanna.Field.GetLength(0); x++)
            {
                for (int y = 0; y < savanna.Field.GetLength(1); y++)
                {
                    savanna.Field[x, y].neighbors.Clear();
                }
            }
        }

        public void ClearCameFrom()
        {
            for (int x = 0; x < savanna.Field.GetLength(0); x++)
            {
                for (int y = 0; y < savanna.Field.GetLength(1); y++)
                {
                    savanna.Field[x, y].cameFrom = null;
                    savanna.Field[x, y].g = 0;
                    savanna.Field[x, y].h = 0;
                    savanna.Field[x, y].f = 0;
                }
            }
        }

        public void CreateAndAddAnimalToTheField(int x, int y, bool isPredator)
        {
            CellBase animal;
            if (isPredator)
            {
                animal = new Predator(
                    x, y,
                    new GameNotifications(),
                    savanna,
                    AStarPathfinding.GetInstance(),
                    ConsoleRenderer.GetInstance()
                    );
            }
            else
            {
                animal = new GrassEater(
                    x, y,
                    new GameNotifications(),
                    savanna,
                    AStarPathfinding.GetInstance(),
                    ConsoleRenderer.GetInstance()
                    );
            }

            savanna.Field[x, y] = animal;
        }

        public void CreateAndAddObstacleToTheField(int x, int y)
        {
            var obstacle = new Obstacle(x,y);

            savanna.Field[x, y] = obstacle;
        }

        public void CreateAndAddObstacleToTheFieldRandomly()
        {
            Random generator = new Random(Guid.NewGuid().GetHashCode());

            for (int x = 0; x < savanna.Field.GetLength(0); x++)
            {
                for (int y = 0; y < savanna.Field.GetLength(1); y++)
                {
                    if (generator.Next(101) < Globals.ObstacleAppearChance)
                    {
                        savanna.Field[x, y] = new Obstacle(x, y);
                    }
                }
            }
        }
    }
}
