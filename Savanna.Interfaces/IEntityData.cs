using System;

namespace Savanna.Interfaces
{
    public interface IEntityData
    {
        int ID { get; set; }
        string Type { get; set; }
        string DisplayLetter { get; set; }
        ConsoleColor color { get; set; }
        int Speed { get; set; }
        int RunSpeed { get; set; }
        int Vision { get; set; }
        float Health { get; set; }
    }
}
