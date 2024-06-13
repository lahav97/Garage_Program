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

        public static string GetLeicensePlate()
        {
            Console.WriteLine("Please Enter Vihacle leicense plate:");
            string leicensePlate = Console.ReadLine();

            return leicensePlate;
        }

        public static int GetInputNumberFromUser(int i_minmumNumber, int i_maximumNumber)
        {
            string inputNumberString;
            int inputNumberInt;
            bool firstTimeInLoop = true;

            do
            {
                if(!firstTimeInLoop)
                {
                    Console.WriteLine($"The input You enterd is out of range, please enter a number between {i_minmumNumber} - {i_maximumNumber}");
                }

                inputNumberString = Console.ReadLine();
                firstTimeInLoop = false;
            } while (!int.TryParse(inputNumberString, out inputNumberInt) && inputNumberInt >= i_minmumNumber && inputNumberInt <= i_maximumNumber);
            
            return inputNumberInt;
        }
    }
}
