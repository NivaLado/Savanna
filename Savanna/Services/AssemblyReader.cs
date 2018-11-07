using System;
using System.Reflection;
using Savanna.Interfaces;

namespace Savanna.Services
{
    public class AssemblyReader : IAssemblyReader
    {
        public string root = AppDomain.CurrentDomain.BaseDirectory;

        public IEntityData PullAnimal(string animal)
        {
            Assembly source = Assembly.LoadFile(root + animal + ".dll");
            Type animalType = source.GetType("Savanna.Interfaces." + animal);
            object instance = Activator.CreateInstance(animalType);
            IEntityData data = instance as IEntityData;
            return data;
        }
    }
}
