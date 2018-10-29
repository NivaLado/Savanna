using Savanna.Models;

namespace Savanna.Services
{
    public interface ISavannaFieldManager
    {
        SavannaField savanna { get; set; }
        void GenerateEmptyField();
        void AddNeighbors();
        void ClearNeightbors();
        void CreateAndAddAnimalToTheField(int x, int y, bool isPredator);
        void CreateAndAddObstacleToTheField(int x, int y);
        void CreateAndAddObstacleToTheFieldRandomly();
    }
}