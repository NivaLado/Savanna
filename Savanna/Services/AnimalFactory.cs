using System;
using Autofac;
using Savanna.Entities;
using Savanna.Entities.Interfaces;

namespace Savanna.Services
{
    public class AnimalFactory : IAnimalFactory
    {
        static IContainer _container;

        public static void SetContainer(IContainer container)
        {
            _container = container;
        }

        public AnimalBase CreateAnimal<T>()
        {
            Type listType = typeof(T);

            if (listType == typeof(Predator))
            {
                return _container.Resolve<Predator>();
            }
            else if (listType == typeof(GrassEater))
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
