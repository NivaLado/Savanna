using System;
using Savanna.Entities.Interfaces;

namespace Savanna.Interfaces
{
    public class Lion : IEntityData
    {
        public int ID { get; set; }
        public string Type { get; set; } = "Lion";
        public string DisplayLetter { get; set; } = "L";
        public ConsoleColor color { get; set; } = ConsoleColor.Red;
        public int Speed { get; set; } = 5;
        public int RunSpeed { get; set; } = 10;
        public int Vision { get; set; } = 10;
        public float Health { get; set; } = 25;
    }
}
