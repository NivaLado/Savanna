using Autofac;
using Savanna.Entities;
using Savanna.Entities.Interfaces;
using Savanna.Rendering;
using Savanna.Services;

namespace Savanna.Containers
{
    public class IoCBuilder
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Predator>();
            builder.RegisterType<GrassEater>();
            builder.RegisterType<GameManager>();
            builder.RegisterType<InputManager>();

            builder.RegisterType<ConsoleRenderer>().As<IRenderer>().SingleInstance();
            builder.RegisterType<SavannaField>().As<ISavannaField>().SingleInstance();
            builder.RegisterType<AStarPathfinding>().As<IPathfinder>().SingleInstance();
            builder.RegisterType<GameNotifications>().As<INotificator>().SingleInstance();

            builder.RegisterType<AnimalsRepository>().As<IRepository<AnimalBase>>();
            builder.RegisterType<Validator>().As<IValidator>();
            builder.RegisterType<DialogWithUser>().As<IDialog>();
            builder.RegisterType<InputManager>().As<IInputManager>();
            builder.RegisterType<AnimalFactory>().As<IAnimalFactory>();
            builder.RegisterType<AssemblyReader>().As<IAssemblyReader>();
            builder.RegisterType<SavannaFieldManager>().As<ISavannaFieldManager>();

            return builder.Build();
        }
    }
}