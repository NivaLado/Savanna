using Savanna.Interfaces;
using System.Collections.Generic;

namespace Savanna.Abstract
{
    public abstract class CellBase : ICellBase
    {
        public int _x { get; set; }
        public int _y { get; set; }

        public double f { get; set; }
        public double g { get; set; }
        public double h { get; set; }
        public bool IsObstacle { get; set; }
        public List<ICellBase> neighbors { get; set; }
        public ICellBase cameFrom { get; set; }

        public CellBase(int x, int y)
        {
            _x = x;
            _y = y;
        }


        public void AddNeighbords(ISavannaField savanna)
        {
            if(neighbors== null)
                neighbors = new List<ICellBase>();

            if( _x < savanna.Width - 1)
            {
                neighbors.Add(savanna.Field[_x + 1, _y]);
            }
            if (_x > 0)
            {
                neighbors.Add(savanna.Field[_x - 1, _y]);
            }
            if(_y < savanna.Height - 1)
            {
                neighbors.Add(savanna.Field[_x, _y + 1]);
            }
            if( _y > 0)
            {
                neighbors.Add(savanna.Field[_x, _y - 1]);
            }
        }

        public abstract void Behave();
    }
}
