using System.Collections.Generic;

namespace Savanna.Interfaces
{
    public interface IPathfinder
    {
        List<ICellBase> MoveFromTo(ICellBase start, ICellBase end);
        double Heuristic(ICellBase neighbor, ICellBase end);
        void ClearOldData();
    }
}
