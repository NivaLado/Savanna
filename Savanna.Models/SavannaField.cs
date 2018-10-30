using Savanna.Constants;
using Savanna.Interfaces;
using System;

namespace Savanna.Models
{
    [Serializable]
    public class SavannaField : ISavannaField
    {
        public int Width => Globals.Width;

        public int Height => Globals.Height;

        public ICellBase[,] Field { get; set; }
    }
}
