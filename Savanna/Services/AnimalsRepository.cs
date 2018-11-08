using Savanna.Entities;
using Savanna.Entities.Interfaces;

namespace Savanna.Services
{
    public class AnimalsRepository : IRepository<AnimalBase>
    {
        static int counter = 0;
        IAssemblyReader _reader;
        IAnimalFactory _factory;

        public AnimalsRepository(IAnimalFactory factory, IAssemblyReader reader)
        {
            _factory = factory;
            _reader = reader;
        }

        public AnimalBase GetAnimal<T>(string type)
        {
            var animal = _factory.CreateAnimal<T>();
            animal.data = _reader.PullAnimal(type);
            DynamicIdAssign(animal);
            return animal;
        }

        private void DynamicIdAssign(AnimalBase animal)
        {
            animal.data.ID = counter;
            counter++;
        }
    }
}
