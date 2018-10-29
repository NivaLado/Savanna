using System;
using System.Threading;
using Savanna.Abstract;
using Savanna.Constants;
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
            //_savannaManager.CreateAndAddAnimalToTheField(21, 21, true);

            _savannaManager.CreateAndAddAnimalToTheField(5, 5, false);
            //Initialize All Neigtbors
            _savannaManager.AddNeighbors();
            _savanna = _savannaManager.savanna;
        }

        public void GameLoop()
        {
            Render();
            int width = _savanna.Field.GetLength(0);
            int height = _savanna.Field.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if(_savanna.Field[x, y] is AnimalBase)
                        _savanna.Field[x, y].Behave();
                }
            }
            //Thread.Sleep(1000);
            Render();
        }

        public void Render()
        {
            _renderer.DrawGame(_savanna, 1, 1);
        }
    }
}
