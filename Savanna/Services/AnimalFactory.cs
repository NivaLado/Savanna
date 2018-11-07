using Autofac;
using Savanna.Constants;
using Savanna.Entities;

namespace Savanna.Services
{
    public class AnimalFactory
    {
        private static AssemblyReader reader;
        private static IContainer _container;
        //CLEAN HERE - Make repository , get rid of static//
        static public void SetContainer(IContainer container)
        {
            _container = container;
            reader = new AssemblyReader();
        }

        static public AnimalBase CreateAnimal(int input)
        {
            if (input == AnimalTypes.Lion)
            {
                var animal = _container.Resolve<Predator>();
                animal.data = reader.PullAnimal("Lion");
                return animal;
            }
            else if (input == AnimalTypes.Antelope)
            {
                var animal = _container.Resolve<GrassEater>();
                animal.data = reader.PullAnimal("Antelope");
                return animal;
            }
            else
            {
                return null;
            }
        }
    }
}
