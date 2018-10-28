﻿namespace Savanna.Interfaces
{
    public interface IAnimalData
    {
        int ID { get; set; }
        int Speed { get; set; }
        ICellBase Position { get; set; }
        bool IsPredator { get; set; }
    }
}