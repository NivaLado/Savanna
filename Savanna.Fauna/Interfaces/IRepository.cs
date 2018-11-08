namespace Savanna.Entities.Interfaces
{
    public interface IRepository<T> where T : AnimalBase
    {
        AnimalBase GetAnimal<Type>(string type);
    }
}