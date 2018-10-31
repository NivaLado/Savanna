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

        private void StartGame()
        {
            _renderer.CursorVisible(false);
            _renderer.StartTransition();
        }

        private void ShowGame()
        {
            _renderer.EndTransition(_savanna);
        }

        private void InitializeGameField() //Here Should Be Factory(Animal)
        {
            _savannaManager.GenerateEmptyField();
            //Add Obstacles Before Creating Animals
            _savannaManager.CreateAndAddObstacleToTheFieldRandomly();
            //TestObstacles();
            //Add Animals
            //_savannaManager.CreateAndAddAnimalToTheField(0, 0, 10, 15, true);
            TestLions();
            TestGrassEaters();
            //Initialize All Neigtbors
            _savannaManager.AddNeighbors();
            _savanna = _savannaManager.savanna;
        }

        public void GameLoop()
        {
            ResetAction();
            Render();
            AnimalTurn<GrassEater>();
            AnimalTurn<Predator>();
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

        private void TestLions()
        {
            _savannaManager.CreateAndAddAnimalToTheField(1, 4, 10, 15, true);
            _savannaManager.CreateAndAddAnimalToTheField(27, 42, 10, 15, true);
            _savannaManager.CreateAndAddAnimalToTheField(13, 32, 10, 15, true);
            _savannaManager.CreateAndAddAnimalToTheField(36, 26, 10, 15, true);
            _savannaManager.CreateAndAddAnimalToTheField(11, 35, 10, 15, true);
            _savannaManager.CreateAndAddAnimalToTheField(6, 27, 10, 15, true);
            _savannaManager.CreateAndAddAnimalToTheField(24, 20, 10, 15, true);
            _savannaManager.CreateAndAddAnimalToTheField(34, 11, 10, 15, true);
            _savannaManager.CreateAndAddAnimalToTheField(5, 10, 10, 15, true);
            _savannaManager.CreateAndAddAnimalToTheField(20, 11, 10, 15, true);
        }

        private void TestGrassEaters()
        {
            _savannaManager.CreateAndAddAnimalToTheField(30, 35, 12, 5, false);
            _savannaManager.CreateAndAddAnimalToTheField(40, 9, 12, 5, false);
            _savannaManager.CreateAndAddAnimalToTheField(17, 27, 12, 5, false);
            _savannaManager.CreateAndAddAnimalToTheField(42, 8, 12, 5, false);
            _savannaManager.CreateAndAddAnimalToTheField(17, 30, 12, 5, false);
            _savannaManager.CreateAndAddAnimalToTheField(8, 15, 12, 5, false);
            _savannaManager.CreateAndAddAnimalToTheField(7, 9, 12, 5, false);
            _savannaManager.CreateAndAddAnimalToTheField(17, 26, 12, 5, false);
            _savannaManager.CreateAndAddAnimalToTheField(29, 11, 12, 5, false);
            _savannaManager.CreateAndAddAnimalToTheField(24, 7, 12, 5, false);
        }
    }
}
