namespace Savanna.Entities.Interfaces
{
    public interface IAnimalFactory
    {
        AnimalBase CreateAnimal<T>();
    }
}
