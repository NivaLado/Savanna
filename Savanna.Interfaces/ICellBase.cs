using System.Collections.Generic;

namespace Savanna.Interfaces
{
    public interface ICellBase
    {
        int _x { get; set; }
        int _y { get; set; }

        double f { get; set; }
        double g { get; set; }
        double h { get; set; }

        List<ICellBase> neighbors { get; set; }
        ICellBase cameFrom { get; set; }
        void AddNeighbords(ISavannaField savanna);


        void Behave();
    }
}
