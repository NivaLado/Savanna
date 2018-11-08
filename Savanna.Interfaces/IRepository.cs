using System.Collections.Generic;

namespace Savanna.Interfaces
{
    public interface IRepository<T> where T : IAnimalBase
    {
        IEnumerable<T> List { get; }
        void Add(T entity);
        void Delete(T entity);
        AnimalBase GetAnimal<Type>(string type);
    }
}