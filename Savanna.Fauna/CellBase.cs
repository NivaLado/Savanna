using System.Collections.Generic;
using Savanna.Interfaces;

namespace Savanna.Entities
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

        protected ISavannaFieldManager _savanna;

        public CellBase(ISavannaFieldManager savanna)
        {
            _savanna = savanna;
            AddNeighbors(_savanna.Area);
        }

        public void SetPosition(int x, int y)
        {
            xPos = x;
            yPos = y;
        }

        public void AddNeighbors(ISavannaField area)
        {
            neighbors = new List<ICellBase>();

            if (xPos < area.Width - 1)
            {
                neighbors.Add(area.Field[xPos + 1, yPos]);
            }
            if (xPos > 0)
            {
                neighbors.Add(area.Field[xPos - 1, yPos]);
            }
            if (yPos < area.Height - 1)
            {
                neighbors.Add(area.Field[xPos, yPos + 1]);
            }
            if (yPos > 0)
            {
                neighbors.Add(area.Field[xPos, yPos - 1]);
            }
        }

        public abstract void Behave();
    }
}
