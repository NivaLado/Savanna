using System;

namespace Savanna.Fauna.Models
{
    [Serializable]
    public class Animal
    {
        public int ID { get; set; }
        public int Speed { get; set; }
        public int[,] Position { get; set; }
        public bool IsPredator { get; set; }
    }
}
