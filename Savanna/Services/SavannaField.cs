using Savanna.Constants;
using Savanna.Interfaces;

namespace Savanna.Services
{
    public class SavannaField : ISavannaField
    {
        public int Width
        {
            get
            {
                return Globals.Width;
            }
            set
            {
                Width = value;
            }
        }

        public int Height
        {
            get
            {
                return Globals.Height;
            }
            set
            {
                Height = value;
            }
        }

        public ICellBase[,] Field { get; set; }
    }
}
