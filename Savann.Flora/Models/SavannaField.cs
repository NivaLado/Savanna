using System;
using Savanna.Constants;
using Savanna.Fauna;
using Savanna.Fauna.Interfaces;

namespace Savanna.Flora.Models
{
    [Serializable]
    public class SavannaField
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
        public IAnimal[,] Field { get; set; }
    }
}
