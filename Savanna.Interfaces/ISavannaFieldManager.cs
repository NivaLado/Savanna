namespace Savanna.Interfaces
{
    public interface ISavannaFieldManager
    {
        ISavannaField savanna { get; set; }
        void AddNeighbors();
        void GenerateEmptyField();
        void ClearSavannaAStarData();
        void CreateAndAddObstacleToTheFieldRandomly();
        void CreateAndAddObstacleToTheField(int x, int y);
        void CreateAndAddAnimalToTheFieldAtRandom(int input);
        void CreateAndAddAnimalToTheFieldAt(int input, int x, int y);
    }
}