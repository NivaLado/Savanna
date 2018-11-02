using System;
using Savanna.Constants;
using Savanna.Interfaces;

namespace Savanna.Services
{
    public class SavannaField : ISavannaField
    {
        #region Singleton
        private static readonly Lazy<SavannaField> lazy =
                            new Lazy<SavannaField>(() => new SavannaField());
        public string Name { get; private set; }

        private SavannaField()
        {
            Name = Guid.NewGuid().ToString();
        }

        public static SavannaField GetInstance()
        {
            return lazy.Value;
        }
        #endregion

        public int Width => Globals.Width;

        public int Height => Globals.Height;

        public ICellBase[,] Field { get; set; }
    }
}
