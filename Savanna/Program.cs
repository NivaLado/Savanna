using System;
using Savanna.Services;

namespace Savanna
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var dialog = new DialogWithUser(
                new ConsoleRenderer(),
                new Validator()
            );

            dialog.WriteMessage("Test plain int input");
            string str = dialog.ValidInt();

            dialog.WriteMessage("Test Min is 5");
            string strMin = dialog.ValidMinInt(5);

            dialog.WriteMessage("Test Max is 7");
            string strMax = dialog.ValidMaxInt(7);

            dialog.WriteMessage("Test Range Input");
            string strRange = dialog.ValidIntInRange(3, 6);

            dialog.WriteMessage("End");

            Console.ReadLine();
        }
    }
}