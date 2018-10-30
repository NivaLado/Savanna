namespace Savanna.Interfaces
{
    public interface IPathfinder
    {
        void MoveFromTo(ICellBase start, ICellBase end);
        void ClearOldData();
    }
}
