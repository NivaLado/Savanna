using System;
using Savanna.Constants;
using Savanna.Entities;
using Savanna.Entities.Interfaces;

namespace Savanna.Services
{
    public class GameManager
    {
        public event Action<int> NewDay;
        private int days = 0;

        private IDialog _dialog;
        private IRenderer _renderer;
        private INotificator _notificator;
        private ISavannaFieldManager _savannaManager;

        public GameManager(
            IDialog dialog,
            IRenderer renderer,
            INotificator notificator,
            ISavannaFieldManager savannaManager)
        {
            _dialog = dialog;
            _renderer = renderer;
            _notificator = notificator;
            _savannaManager = savannaManager;
        }

        public void MainMenu()
        {
            int selected = _dialog.GameMenu();
            switch (selected)
            {
                case MenuOption.NewGame:
                    StartCountingDays();
                    InitializeGameField();
                    StartGame();
                    ShowGame();
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
            OnNewDay();
            ResetAction();
            AnimalTurn<GrassEater>();
            AnimalTurn<Predator>();
            days++;
        }

        private void StartGame()
        {
            _renderer.CursorVisible(false);
            _renderer.Transition();
        }

        private void ShowGame()
        {
            _renderer.Transition(_savannaManager.Area);
        }

        private void StartCountingDays()
        {
            NewDay += _notificator.OnNewDay;
            OnNewDay();
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
        }

        private void Render()
        {
            _renderer.DrawGame(_savannaManager.Area, Globals.XOffset, Globals.YOffset);
        }

        private void AnimalTurn<Animal>()
        {
            int width = _savannaManager.Area.Width;
            int height = _savannaManager.Area.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (_savannaManager.Area.Field[x, y] is Animal)
                    {
                        _savannaManager.Area.Field[x, y].Behave();
                    }
                }
            }
        }

        private void ResetAction()
        {
            int width = _savannaManager.Area.Width;
            int height = _savannaManager.Area.Height;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _savannaManager.Area.Field[x, y].CanAction = true;
                }
            }
        }

        private void OnNewDay()
        {
            NewDay?.Invoke(days);
        }

        /*Testing Methods*/
        private void TestPredatorsRandomly()
        {
            for (int i = 0; i < 10; i++)
            {
                _savannaManager.AddAnimalToTheFieldAtRandom<Predator>("Lion");
            }
        }

        private void TestGrassEatersRandomly()
        {
            for (int i = 0; i < 10; i++)
            {
                _savannaManager.AddAnimalToTheFieldAtRandom<GrassEater>("Antelope");
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
