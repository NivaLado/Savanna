using Savanna.Interfaces;
using System;

namespace Savanna.Services
{
    public class GameNotifications : INotificator
    {
        //IRenderer _renderer;

        //public  GameNotifications(IRenderer renderer)
        //{

        //}

        public void OnAnimalMoved(object source, AnimalEventArgs e)
        {
            //Console.WriteLine("Lion with id " +  e.AnimalData.ID +  " Moved. Great Job.");
        }

        public void OnAnimalDied(object source, AnimalEventArgs e)
        {
            //Console.WriteLine("Lion with id " + e.AnimalData.ID + " Died. So Sad.");
        }

        public void OnAnimalBorned(object source, AnimalEventArgs e)
        {
            //Console.WriteLine("Lion with id " + e.AnimalData.ID + " Borned.");
        }
    }
}
