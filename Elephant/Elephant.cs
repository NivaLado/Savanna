using System;
using Savanna.Entities.Interfaces;

namespace Savanna.Interfaces
{
    public class Elephant : IEntityData
    {
        public int ID { get; set; }
        public string Type { get; set; } = "Elephant";
        public string DisplayLetter { get; set; } = "E";
        public ConsoleColor color { get; set; } = ConsoleColor.Magenta;
        public int Speed { get; set; } = 2;
        public int RunSpeed { get; set; } = 3;
        public int Vision { get; set; } = 5;
        public float Health { get; set; } = 50;
    }
}
