using System.Collections.Generic;
using Savanna.Interfaces;

namespace Savanna.Abstract
{
    public abstract class CellBase : ICellBase
    {
        public int _x { get; set; }
        public int _y { get; set; }

        public double sum { get; set; }
        public double distance { get; set; }
        public double heuristic { get; set; }

        public bool IsObstacle { get; set; }
        public bool CanAction { get; set; }
        public List<ICellBase> neighbors { get; set; }
        public ICellBase cameFrom { get; set; }

        public CellBase(int x, int y)
        {
            _x = x;
            _y = y;
        }


        public void AddNeighbors(ISavannaField savanna)
        {
            neighbors = new List<ICellBase>();

            if (_x < savanna.Width - 1)
            {
                neighbors.Add(savanna.Field[_x + 1, _y]);
            }
            if (_x > 0)
            {
                neighbors.Add(savanna.Field[_x - 1, _y]);
            }
            if (_y < savanna.Height - 1)
            {
                neighbors.Add(savanna.Field[_x, _y + 1]);
            }
            if (_y > 0)
            {
                neighbors.Add(savanna.Field[_x, _y - 1]);
            }
        }

        public abstract void Behave();
    }
}
