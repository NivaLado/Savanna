using Autofac;
using Savanna.Interfaces;
using Savanna.Rendering;
using Savanna.Services;

namespace Savanna.Containers
{
    public class IoCBuilder
    {
        public static IContainer BuildDialogWithUser()
        {
            var builder = new ContainerBuilder();
            var rendererInstance = ConsoleRenderer.GetInstance();

            builder.RegisterType<DialogWithUser>();
            builder.RegisterType<Validator>().As<IValidator>();
            builder.RegisterInstance<IRenderer>(rendererInstance);

            return builder.Build();
        }
    }
}
