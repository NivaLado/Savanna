using System;
using Savanna.Constants;
using Savanna.Fauna;
using Savanna.Interfaces;

namespace Savanna.Services
{
    public class GameManager
    {
        private IRenderer _renderer;
        private ISavannaFieldManager _savannaManager;
        private IDialog _dialog;
        private ISavannaField _savanna;

        public GameManager(IRenderer renderer, ISavannaFieldManager savanna, IDialog dialog)
        {
            _savannaManager = savanna;
            _renderer = renderer;
            _dialog = dialog;
        }

        public void MainMenu()
        {
            int selected = _dialog.GameMenu();
            switch (selected)
            {
                case MenuOption.NewGame:
                    InitializeGameField();
                    //StartGame();
                    //ShowGame();
                    break;
                case MenuOption.Exit:
                    Environment.Exit(0);
                    break;
                case MenuOption.EscExit:
                    Environment.Exit(0);
                    break;
            }
        }

        public void GameLoop()
        {
            ResetAction();
            AnimalTurn<GrassEater>();
            AnimalTurn<Predator>();
        }

        private void StartGame()
        {
            _renderer.CursorVisible(false);
            _renderer.StartTransition();
        }

        private void ShowGame()
        {
            _renderer.EndTransition(_savanna);
        }

        private void InitializeGameField()
        {
            _savannaManager.GenerateEmptyField();
            //Add Obstacles Before Creating Animals
            _savannaManager.CreateAndAddObstacleToTheFieldRandomly();
            //Add Animals
            TestPredatorsRandomly();
            TestGrassEatersRandomly();
            //Initialize All Neigtbors
            _savannaManager.AddNeighbors();
            _savanna = _savannaManager.savanna;
        }

        private void Render()
        {
            _renderer.DrawGame(_savanna, Globals.XOffset, Globals.YOffset);
        }

        private void AnimalTurn<Animal>()
        {
            int width = _savanna.Field.GetLength(0);
            int height = _savanna.Field.GetLength(1);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (_savanna.Field[x, y] is Animal)
                    {
                        _savanna.Field[x, y].Behave();
                    }
                }
            }
        }

        private void ResetAction()
        {
            int width = _savanna.Field.GetLength(0);
            int height = _savanna.Field.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _savanna.Field[x, y].CanAction = true;
                }
            }
        }

        /*Testing Methods*/
        private void TestPredators()
        {
            _savannaManager.CreateAndAddAnimalToTheFieldAt(1, 5, 40);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(1, 10, 35);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(1, 15, 30);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(1, 20, 25);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(1, 25, 20);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(1, 30, 15);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(1, 35, 10);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(1, 40, 5);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(1, 6, 6);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(1, 18, 18);
        }

        private void TestPredatorsRandomly()
        {
            for (int i = 0; i < 10; i++)
            {
                _savannaManager.CreateAndAddAnimalToTheFieldAtRandom(1);
            }
        }

        private void TestGrassEaters()
        {
            _savannaManager.CreateAndAddAnimalToTheFieldAt(2, 38, 4);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(2, 32, 8);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(2, 26, 12);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(2, 16, 16);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(2, 12, 22);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(2, 8, 26);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(2, 6, 32);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(2, 4, 38);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(2, 2, 0);
            _savannaManager.CreateAndAddAnimalToTheFieldAt(2, 0, 0);
        }

        private void TestGrassEatersRandomly()
        {
            for (int i = 0; i < 10; i++)
            {
                _savannaManager.CreateAndAddAnimalToTheFieldAtRandom(2);
            }
        }

        private void TestObstacles()
        {
            _savannaManager.CreateAndAddObstacleToTheField(20, 21);

            _savannaManager.CreateAndAddObstacleToTheField(21, 20);
            _savannaManager.CreateAndAddObstacleToTheField(21, 22);

            _savannaManager.CreateAndAddObstacleToTheField(22, 20);
            _savannaManager.CreateAndAddObstacleToTheField(22, 22);

            _savannaManager.CreateAndAddObstacleToTheField(23, 20);
            _savannaManager.CreateAndAddObstacleToTheField(23, 22);

            _savannaManager.CreateAndAddObstacleToTheField(24, 20);
            _savannaManager.CreateAndAddObstacleToTheField(24, 22);

            _savannaManager.CreateAndAddObstacleToTheField(25, 20);
            _savannaManager.CreateAndAddObstacleToTheField(25, 22);
        }
    }
}
