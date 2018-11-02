using System.Collections.Generic;
using Savanna.Interfaces;

namespace Savanna.Abstract
{
    public abstract class CellBase : ICellBase
    {
        public int xPos { get; set; }
        public int yPos { get; set; }

        public double sum { get; set; }
        public double distance { get; set; }
        public double heuristic { get; set; }

        public bool CanAction { get; set; }
        public bool IsObstacle { get; set; }
        public ICellBase cameFrom { get; set; }
        public List<ICellBase> neighbors { get; set; }

        protected ISavannaField _savanna;

        public CellBase(ISavannaField savanna)
        {
            _savanna = savanna;
            AddNeighbors(savanna);
        }

        public void SetPosition(int x, int y)
        {
            xPos = x;
            yPos = y;
        }

        public void AddNeighbors(ISavannaField savanna)
        {
            neighbors = new List<ICellBase>();

            if (xPos < savanna.Width - 1)
            {
                neighbors.Add(savanna.Field[xPos + 1, yPos]);
            }
            if (xPos > 0)
            {
                neighbors.Add(savanna.Field[xPos - 1, yPos]);
            }
            if (yPos < savanna.Height - 1)
            {
                neighbors.Add(savanna.Field[xPos, yPos + 1]);
            }
            if (yPos > 0)
            {
                neighbors.Add(savanna.Field[xPos, yPos - 1]);
            }
        }

        public abstract void Behave();
    }
}
