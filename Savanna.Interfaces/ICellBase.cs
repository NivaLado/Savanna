using System.Collections.Generic;

namespace Savanna.Interfaces
{
    public interface ICellBase
    {
        int xPos { get; set; }
        int yPos { get; set; }

        double sum { get; set; }
        double distance { get; set; }
        double heuristic { get; set; }

        bool CanAction { get; set; }
        bool IsObstacle { get; set; }

        ICellBase cameFrom { get; set; }
        List<ICellBase> neighbors { get; set; }

        void SetPosition(int x, int y);
        void AddNeighbors(ISavannaField savanna);

        void Behave();
    }
}
