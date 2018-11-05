using Autofac;
using Savanna.Entities;
using Savanna.Interfaces;
using Savanna.Rendering;
using Savanna.Services;

namespace Savanna.Containers
{
    public class IoCBuilder
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();
            var savannaField = SavannaField.GetInstance();

            builder.RegisterType<Predator>();
            builder.RegisterType<GrassEater>();
            builder.RegisterType<GameManager>();
            builder.RegisterType<InputManager>();

            builder.RegisterType<Validator>().As<IValidator>().SingleInstance();
            builder.RegisterType<ConsoleRenderer>().As<IRenderer>().SingleInstance();
            builder.RegisterType<InputManager>().As<IInputManager>().SingleInstance();
            builder.RegisterType<AStarPathfinding>().As<IPathfinder>().SingleInstance();
            builder.RegisterType<GameNotifications>().As<INotificator>().SingleInstance();

            builder.RegisterType<DialogWithUser>().As<IDialog>();
            builder.RegisterType<SavannaFieldManager>().As<ISavannaFieldManager>();

            builder.RegisterInstance<ISavannaField>(savannaField);

            return builder.Build();
        }
    }
}