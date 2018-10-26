using Savanna.Interfaces;
using Savanna.Services;
using Autofac;

namespace Savanna
{
    public class IoCBuilder
    {
        public static IContainer BuildDialogWithUser()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DialogWithUser>();
            builder.RegisterType<ConsoleRenderer>().As<IRenderer>();
            builder.RegisterType<Validator>().As<IValidator>();

            return builder.Build();
        }
    }
}
