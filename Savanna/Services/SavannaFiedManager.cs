using System;
using Savanna.Abstract;
using Savanna.Constants;
using Savanna.Fauna;
using Savanna.Interfaces;

namespace Savanna.Services
{
    public class SavannaFieldManager : ISavannaFieldManager
    {
        public ISavannaField _savanna { get; set; }

        public SavannaFieldManager(ISavannaField savanna)
        {
            _savanna = savanna;
        }

        public void AddNeighbors()
        {
            for (int x = 0; x < _savanna.Field.GetLength(0); x++)
            {
                for (int y = 0; y < _savanna.Field.GetLength(1); y++)
                {
                    _savanna.Field[x, y].AddNeighbors(_savanna);
                }
            }
        }

        public void GenerateEmptyField()
        {
            _savanna.Field = new CellBase[_savanna.Width, _savanna.Height];
            for (int x = 0; x < _savanna.Field.GetLength(0); x++)
            {
                for (int y = 0; y < _savanna.Field.GetLength(1); y++)
                {
                    var ground = new Ground(_savanna);
                    ground.SetPosition(x, y);
                    _savanna.Field[x, y] = ground;
                }
            }
        }

        public void ClearSavannaAStarData()
        {
            for (int x = 0; x < _savanna.Field.GetLength(0); x++)
            {
                for (int y = 0; y < _savanna.Field.GetLength(1); y++)
                {
                    _savanna.Field[x, y].neighbors.Clear();
                    _savanna.Field[x, y].cameFrom = null;
                    _savanna.Field[x, y].distance = 0;
                    _savanna.Field[x, y].heuristic = 0;
                    _savanna.Field[x, y].sum = 0;
                }
            }
        }

        public void CreateAndAddAnimalToTheFieldAtRandom(int animal)
        {
            var newAnimal = AnimalFactory.CreateAnimal(animal);
            CheckThatGridIsEmtyReturnPos(out int x, out int y);
            newAnimal.SetPosition(x, y);
            _savanna.Field[newAnimal.xPos, newAnimal.yPos] = newAnimal;
        }

        public void CreateAndAddAnimalToTheFieldAt(int animal, int x, int y)
        {
            var newAnimal = AnimalFactory.CreateAnimal(animal);
            newAnimal.SetPosition(x, y);
            _savanna.Field[newAnimal.xPos, newAnimal.yPos] = newAnimal;
        }

        public void CreateAndAddObstacleToTheField(int x, int y)
        {
            var obstacle = new Obstacle(_savanna);
            obstacle.SetPosition(x, y);
            _savanna.Field[x, y] = obstacle;
        }

        public void CreateAndAddObstacleToTheFieldRandomly()
        {
            Random generator = new Random(Guid.NewGuid().GetHashCode());

            for (int x = 0; x < _savanna.Field.GetLength(0); x++)
            {
                for (int y = 0; y < _savanna.Field.GetLength(1); y++)
                {
                    if (generator.Next(101) < Globals.ObstacleAppearChance)
                    {
                        _savanna.Field[x, y] = new Obstacle(_savanna);
                        _savanna.Field[x, y].SetPosition(x, y);
                    }
                }
            }
        }

        private void CheckThatGridIsEmtyReturnPos(out int x, out int y)
        {
            Random random = new Random();
            do
            {
                x = random.Next(0, Globals.Width);
                y = random.Next(0, Globals.Height);
            } while (_savanna.Field[x, y] is AnimalBase);
        }
    }
}
