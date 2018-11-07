using System;

namespace Savanna.Interfaces
{
    public class Antelope : IEntityData
    {
        public int ID { get; set; }
        public string Type { get; set; } = "Antelope";
        public string DisplayLetter { get; set; } = "A";
        public ConsoleColor color { get; set; } = ConsoleColor.Green;
        public int Speed { get; set; } = 7;
        public int RunSpeed { get; set; } = 10;
        public int Vision { get; set; } = 7;
        public float Health { get; set; } = 20;
    }
}
