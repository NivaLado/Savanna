using Autofac;
using Savanna.Abstract;
using Savanna.Containers;
using Savanna.Fauna;

namespace Savanna.Services
{
    public class AnimalFactory
    {
        static IContainer container = IoCBuilder.Build();

        static public AnimalBase CreateAnimal(int input)
        {
            if (input == 1)
            {
                return container.Resolve<Predator>();
            }
            else if (input == 2)
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
