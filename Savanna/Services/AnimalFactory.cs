using Autofac;
using Savanna.Constants;
using Savanna.Containers;
using Savanna.Entities;

namespace Savanna.Services
{
    public class AnimalFactory
    {
        static IContainer container = IoCBuilder.Build();

        static public AnimalBase CreateAnimal(int input)
        {
            if (input == AnimalTypes.Lion)
            {
                return container.Resolve<Predator>();
            }
            else if (input == AnimalTypes.Antelope)
            {
                return container.Resolve<GrassEater>();
            }
            else
            {
                return null;
            }
        }
    }
}
