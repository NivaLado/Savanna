using Savanna.Interfaces;
using System;

namespace Savanna.Models
{
    [Serializable]
    public class AnimalData : IAnimalData
    {
        public int ID { get; set; }
        public int Speed { get; set; }
        public int Vision { get; set; }
        public int RunSpeed { get; set; }
        public bool IsRunning { get; set; }
        public bool IsPredator { get; set; }
    }
}
