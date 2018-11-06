using Autofac;
using Savanna.Constants;
using Savanna.Entities;

namespace Savanna.Services
{
    public class AnimalFactory
    {
        private static IContainer _container;

        static public void SetContainer(IContainer container)
        {
            _container = container;
        }

        static public AnimalBase CreateAnimal(int input)
        {
            if (input == AnimalTypes.Lion)
            {
                return _container.Resolve<Predator>();
            }
            else if (input == AnimalTypes.Antelope)
            {
                return _container.Resolve<GrassEater>();
            }
            else
            {
                return null;
            }
        }
    }
}
