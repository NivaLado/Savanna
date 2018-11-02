using System;
using Savanna.Abstract;
using Savanna.Interfaces;

namespace Savanna.Fauna
{
    public class Ground : CellBase
    {
        public Ground(ISavannaField savanna) : base(savanna)
        {
            AddNeighbors(savanna);
        }

        public override void Behave()
        {
            throw new NotImplementedException();
        }
    }
}
