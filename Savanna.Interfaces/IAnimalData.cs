namespace Savanna.Interfaces
{
    public interface IAnimalData
    {
        int ID { get; set; }
        string Type { get; set; }
        int Speed { get; set; }
        int RunSpeed { get; set; }
        int Vision { get; set; }
        float Health { get; set; }
        bool IsPredator { get; set; }
    }
}
