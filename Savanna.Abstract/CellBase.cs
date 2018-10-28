using Savanna.Interfaces;
using System.Collections.Generic;

namespace Savanna.Abstract
{
    public abstract class CellBase : ICellBase
    {
        public int x { get; set; }
        public int y { get; set; }

        public double f { get; set; }
        public double g { get; set; }
        public double h { get; set; }
        public List<ICellBase> neighbors { get; set; }
        public ICellBase cameFrom { get; set; }

        //public List<ICellBase> neighbors;// = new List<ICellBase>();

        public void AddNeighbords(ISavannaField savanna)
        {
            if(neighbors== null)
                neighbors = new List<ICellBase>();

            if( x < savanna.Width - 1)
            {
                neighbors.Add(savanna.Field[x + 1, y]);
            }
            if (x > 0)
            {
                neighbors.Add(savanna.Field[x - 1, y]);
            }
            if(y < savanna.Height - 1)
            {
                neighbors.Add(savanna.Field[x, y + 1]);
            }
            if( y > 0)
            {
                neighbors.Add(savanna.Field[x, y - 1]);
            }
        }

        public abstract void Behave();
    }
}
