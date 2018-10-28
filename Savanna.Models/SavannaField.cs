using Savanna.Constants;
using Savanna.Interfaces;
using System;

namespace Savanna.Models
{
    [Serializable]
    public class SavannaField : ISavannaField
    {
        public int Width
        {
            get
            {
                 return Globals.Width;
            }
        }
        public int Height
        {
            get
            {
                return Globals.Height;
            }
        }
        public ICellBase[,] Field { get; set; }
    }
}
