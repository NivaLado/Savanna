namespace Savanna.Interfaces
{
    public interface IAnimalData
    {
        int ID { get; set; }
        int Speed { get; set; }
        int RunSpeed { get; set; }
        int Vision { get; set; }
        bool IsRunning { get; set; }
        bool IsPredator { get; set; }
    }
}
