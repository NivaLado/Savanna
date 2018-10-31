using System.Collections.Generic;

namespace Savanna.Interfaces
{
    public interface ICellBase
    {
        int _x { get; set; }
        int _y { get; set; }

        double sum { get; set; }
        double distance { get; set; }
        double heuristic { get; set; }

        bool IsObstacle { get; set; }
        bool CanAction { get; set; }

        List<ICellBase> neighbors { get; set; }
        ICellBase cameFrom { get; set; }

        void AddNeighbors(ISavannaField savanna);

        void Behave();
    }
}
