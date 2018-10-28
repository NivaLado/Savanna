using Savanna.Interfaces;
using System;

namespace Savanna.Models
{
    [Serializable]
    public class AnimalData : IAnimalData
    {
        public int ID { get; set; }
        public int Speed { get; set; }
        public ICellBase Position { get; set; }
        public bool IsPredator { get; set; }
    }
}
