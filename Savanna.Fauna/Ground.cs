using System;
using Savanna.Entities.Interfaces;

namespace Savanna.Entities
{
    public class Ground : CellBase
    {
        public Ground(ISavannaFieldManager savanna) : base(savanna)
        {
            AddNeighbors(savanna.Area);
        }

        public override void Behave()
        {
            throw new NotImplementedException();
        }
    }
}
