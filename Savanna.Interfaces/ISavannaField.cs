namespace Savanna.Interfaces
{
    public interface ISavannaField
    {
        int Width { get; }
        int Height { get; }
        ICellBase[,] Field { get; set; }
    }
}
