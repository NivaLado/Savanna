using System;
using Savanna.Constants;
using Savanna.Entities;
using Savanna.Interfaces;

namespace Savanna.Services
{
    public class SavannaFieldManager : ISavannaFieldManager
    {
        public ISavannaField Area { get; set; }

        public SavannaFieldManager(ISavannaField area)
        {
            Area = area;
        }

        public void GenerateEmptyField()
        {
            Area.Field = new CellBase[Area.Width, Area.Height];
            for (int x = 0; x < Area.Field.GetLength(0); x++)
            {
                for (int y = 0; y < Area.Field.GetLength(1); y++)
                {
                    var ground = new Ground(this);
                    ground.SetPosition(x, y);
                    Area.Field[x, y] = ground;
                }
            }
        }

        public void AddNeighbors()
        {
            for (int x = 0; x < Area.Field.GetLength(0); x++)
            {
                for (int y = 0; y < Area.Field.GetLength(1); y++)
                {
                    Area.Field[x, y].AddNeighbors(Area);
                }
            }
        }

        public void ClearSavannaAStarData()
        {
            for (int x = 0; x < Area.Field.GetLength(0); x++)
            {
                for (int y = 0; y < Area.Field.GetLength(1); y++)
                {
                    Area.Field[x, y].neighbors.Clear();
                    Area.Field[x, y].cameFrom = null;
                    Area.Field[x, y].distance = 0;
                    Area.Field[x, y].heuristic = 0;
                    Area.Field[x, y].sum = 0;
                }
            }
        }

        public void CreateAndAddAnimalToTheFieldAtRandom(int animal)
        {
            var newAnimal = AnimalFactory.CreateAnimal(animal);
            CheckThatGridIsEmtyReturnPos(out int x, out int y);
            newAnimal.SetPosition(x, y);
            Area.Field[newAnimal.xPos, newAnimal.yPos] = newAnimal;
        }

        public void CreateAndAddAnimalToTheFieldAt(int animal, int x, int y)
        {
            var newAnimal = AnimalFactory.CreateAnimal(animal);
            newAnimal.SetPosition(x, y);
            Area.Field[newAnimal.xPos, newAnimal.yPos] = newAnimal;
        }

        public void CreateAndAddObstacleToTheField(int x, int y)
        {
            var obstacle = new Obstacle(this);
            obstacle.SetPosition(x, y);
            Area.Field[x, y] = obstacle;
        }

        public void CreateAndAddObstacleToTheFieldRandomly()
        {
            Random generator = new Random(Guid.NewGuid().GetHashCode());

            for (int x = 0; x < Area.Field.GetLength(0); x++)
            {
                for (int y = 0; y < Area.Field.GetLength(1); y++)
                {
                    if (generator.Next(101) < Globals.ObstacleAppearChance)
                    {
                        Area.Field[x, y] = new Obstacle(this);
                        Area.Field[x, y].SetPosition(x, y);
                    }
                }
            }
        }

        private void CheckThatGridIsEmtyReturnPos(out int x, out int y)
        {
            Random random = new Random();
            int endCounter = 0;
            do
            {
                x = random.Next(0, Globals.Width);
                y = random.Next(0, Globals.Height);
                endCounter++;
            } while (Area.Field[x, y] is AnimalBase
                    && endCounter < Globals.EndCounter);
        }
    }
}
