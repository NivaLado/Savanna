using System.Collections.Generic;

namespace Savanna.Interfaces
{
    public interface IPathfinder
    {
        void ClearOldData();
        double Heuristic(ICellBase neighbor, ICellBase end);
        double GetDistance(ICellBase current, ICellBase end);
        List<ICellBase> MoveFromTo(ICellBase start, ICellBase end);
    }
}
