using System;
using Savanna.Fauna.Interfaces;

namespace Savanna.Fauna 
{
    public class Lion : AnimalBase, IAnimal
    {
        public void Move()
        {
            Console.WriteLine("HELLO IM MOVING");
        }
    }
}
