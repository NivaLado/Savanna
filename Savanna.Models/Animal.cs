﻿using System;
using Savanna.Interfaces;

namespace Savanna.Models
{
    [Serializable]
    public class AnimalData : IAnimalData
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public int Speed { get; set; }
        public int RunSpeed { get; set; }
        public int Vision { get; set; }
        public float Health { get; set; }
        public bool IsPredator { get; set; }
    }
}
