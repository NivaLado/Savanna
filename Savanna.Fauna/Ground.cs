using System;
using Savanna.Abstract;
using Savanna.Interfaces;

namespace Savanna.Fauna
{
    public class Ground : CellBase, IGround
    {
        public Ground(int x, int y) : base(x, y)
        {

        }

        public override void Behave()
        {
            throw new NotImplementedException();
        }
    }
}
