using System;
using Savanna.Interfaces;

namespace Savanna.Models
{
    [Serializable]
    public class EntityData : IEntityData
    {
        public int ID { get; set; }
        public int Speed { get; set; }
        public int RunSpeed { get; set; }
        public int Vision { get; set; }
        public float Health { get; set; }
        public string Type { get; set; }
        public string DisplayLetter { get; set; }
        public ConsoleColor color { get; set; }
    }
}
