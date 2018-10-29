using System;
using System.Threading;
using Savanna.Abstract;
using Savanna.Constants;
using Savanna.Fauna;
using Savanna.Interfaces;
using Savanna.Models;

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
            switch(selected)
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

        public void StartGame()
        {
            _renderer.StartTransition();
        }

        public void ShowGame()
        {
            _renderer.EndTransition(_savanna);
        }

        public void InitializeGameField()
        {
            _savannaManager.GenerateEmptyField();
            //Add Obstacles Before Creating Animals
            _savannaManager.CreateAndAddObstacleToTheFieldRandomly();
            //Add Animals
            _savannaManager.CreateAndAddAnimalToTheField(3, 3, true);

            _savannaManager.CreateAndAddAnimalToTheField(0, 0, false);
            //_savannaManager.CreateAndAddAnimalToTheField(15, 5, false);
            //Initialize All Neigtbors
            _savannaManager.AddNeighbors();
            _savanna = _savannaManager.savanna;
        }

        public void GameLoop()
        {
            ResetAction();
            int width = _savanna.Field.GetLength(0);
            int height = _savanna.Field.GetLength(1);
            Render();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (_savanna.Field[x, y] is GrassEater)
                    {
                        _savanna.Field[x, y].Behave();
                    }
                }
            }
            Render();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (_savanna.Field[x, y] is Predator)
                    {
                        _savanna.Field[x, y].Behave();
                    }
                }
            }
            Thread.Sleep(1500);
            Render();
            Thread.Sleep(1500);
        }

        public void Render()
        {
            _renderer.DrawGame(_savanna, 1, 1);
        }

        public void ResetAction()
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
    }
}
