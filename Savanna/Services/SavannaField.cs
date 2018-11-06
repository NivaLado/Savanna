﻿using Savanna.Constants;
using Savanna.Interfaces;

namespace Savanna.Services
{
    public class SavannaField : ISavannaField
    {
        public int Width => Globals.Width;

        public int Height => Globals.Height;

        public ICellBase[,] Field { get; set; }
    }
}
