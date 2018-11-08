namespace Savanna.Interfaces
{
    public interface IAnimalFactory
    {
        IAnimalBase CreateAnimal<T>();
    }
}
