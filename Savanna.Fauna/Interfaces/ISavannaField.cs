namespace Savanna.Entities.Interfaces
{
    public interface ISavannaField
    {
        int Width { get; set; }
        int Height { get; set; }
        ICellBase[,] Field { get; set; }
    }
}
