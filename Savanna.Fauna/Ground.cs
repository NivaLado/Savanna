﻿using System;
using Savanna.Interfaces;

namespace Savanna.Entities
{
    public class Ground : CellBase
    {
        public Ground(ISavannaFieldManager savanna) : base(savanna)
        {
            AddNeighbors(savanna.area);
        }

        public override void Behave()
        {
            throw new NotImplementedException();
        }
    }
}
