using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class InputHandler
    {
        public static int GetUserChoice()
        {
            string userChoiceString;
            int userChoiceInt;
            userChoiceString = Console.ReadLine();
            int.TryParse(userChoiceString, out userChoiceInt);

            return userChoiceInt;
        }

        public static string GetLeicensePlate()
        {
            Console.WriteLine("Please Enter Vihacle leicense plate:");
            string leicensePlate = Console.ReadLine();

            return leicensePlate;
        }

        /*public void bool CheckIfInputIsCorrect(string i_input)
        {
            if (string.IsNullOrWhiteSpace(i_input))
        }*/
    }
}
