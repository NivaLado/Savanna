namespace Savanna.Entities.Interfaces
{
    public interface ISavannaFieldManager
    {
        ISavannaField Area { get; set; }
        void AddNeighbors();
        void GenerateEmptyField();
        void ClearSavannaAStarData();
        void CreateAndAddObstacleToTheFieldRandomly();
        void CreateAndAddObstacleToTheField(int x, int y);
        void AddAnimalToTheFieldAtRandom<T>(string animal);
        void AddAnimalToTheFieldAt<T>(string animal, int x, int y);
    }
}