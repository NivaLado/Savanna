using Savanna.Abstract;
using Savanna.Interfaces;
using Savanna.Models;

namespace Savanna.Services
{
    public class GameManager
    {
        private IRenderer _renderer;
        private ISavannaField _savanna;

        public GameManager(IRenderer renderer, SavannaField savanna)
        {
            _renderer = renderer;
            _savanna = savanna;
        }

        public void StartGame()
        {
            _renderer.StartTransition();
        }

        public void ShowGame()
        {
            _renderer.EndTransition(_savanna);
        }

        public void GameLoop()
        {
            int width = _savanna.Field.GetLength(0);
            int height = _savanna.Field.GetLength(1);

            for (int x = 0; x < _savanna.Field.GetLength(0); x++)
            {
                for (int y = 0; y < _savanna.Field.GetLength(1); y++)
                {
                    if(_savanna.Field[x, y] is AnimalBase)
                        _savanna.Field[x, y].Behave();
                }
            }
        }

        public void Render()
        {
            _renderer.DrawGame(_savanna, 1, 1);
        }
    }
}
