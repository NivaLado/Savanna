using System;
using Savanna.Abstract;
using Savanna.Constants;
using Savanna.Fauna;
using Savanna.Interfaces;
using Savanna.Models;
using Savanna.Rendering;

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

        public ISavannaField savanna { get; set; }

        public void GenerateEmptyField()
        {
            savanna = new SavannaField();
            savanna.Field = new CellBase[savanna.Width, savanna.Height];
            for (int x = 0; x < savanna.Field.GetLength(0); x++)
            {
                for (int y = 0; y < savanna.Field.GetLength(1); y++)
                {
                    savanna.Field[x, y] = new Ground(x, y, savanna);
                }
            }
        }

        public void AddNeighbors()
        {
            for (int x = 0; x < savanna.Field.GetLength(0); x++)
            {
                for (int y = 0; y < savanna.Field.GetLength(1); y++)
                {
                    savanna.Field[x, y].AddNeighbors(savanna);
                }
            }
        }

        public void ClearSavannaAStarData()
        {
            for (int x = 0; x < savanna.Field.GetLength(0); x++)
            {
                for (int y = 0; y < savanna.Field.GetLength(1); y++)
                {
                    savanna.Field[x, y].neighbors.Clear();
                    savanna.Field[x, y].cameFrom = null;
                    savanna.Field[x, y].distance = 0;
                    savanna.Field[x, y].heuristic = 0;
                    savanna.Field[x, y].sum = 0;
                }
            }
        }

        public void CreateAndAddAnimalToTheField(int x, int y, int speed, int runSpeed, bool isPredator)
        {
            CellBase animal;
            if (isPredator)
            {
                animal = new Predator(
                    x, y,
                    speed, runSpeed,
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
                    speed, runSpeed,
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
            var obstacle = new Obstacle(x, y, savanna);
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
                        savanna.Field[x, y] = new Obstacle(x, y, savanna);
                    }
                }
            }
        }
    }
}
