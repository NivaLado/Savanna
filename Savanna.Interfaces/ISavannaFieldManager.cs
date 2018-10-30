namespace Savanna.Interfaces
{
    public interface ISavannaFieldManager
    {
        ISavannaField savanna { get; set; }
        void GenerateEmptyField();
        void AddNeighbors();
        void ClearSavannaAStarData();
        void CreateAndAddAnimalToTheField(int x, int y, int speed, int runSpeed, bool isPredator);
        void CreateAndAddObstacleToTheField(int x, int y);
        void CreateAndAddObstacleToTheFieldRandomly();
    }
}