using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class InputHandler
    {
        internal static string GetLeicensePlate()
        {
            Console.WriteLine("Please Enter Vihacle leicense plate:");
            string leicensePlate = Console.ReadLine();

            return leicensePlate;
        }

        internal static string GetAStringFromUser(string i_wantedFormat)
        {
            string inputName = Console.ReadLine();
            bool firstTimeInLoop = true;

            do
            {
                if (!firstTimeInLoop)
                {
                    Console.WriteLine($"The {i_wantedFormat} You enterd is incorect, please enter again:");
                }

                inputName = Console.ReadLine();
                firstTimeInLoop = false;
            } while (string.IsNullOrEmpty(inputName));

            return inputName;
        }

        internal static int GetInputNumberFromUser(int i_minmumNumber, int i_maximumNumber)
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

        internal static float GetFloatFromUser()
        {
            float inputNumberfloat;
            bool firstTimeInLoop = true;

            do
            {
                if (!firstTimeInLoop)
                {
                    Console.WriteLine($"The input You enterd is incurrect");
                }
                firstTimeInLoop = false;

            } while (!float.TryParse(Console.ReadLine(), out inputNumberfloat) && inputNumberfloat > 0);

            return inputNumberfloat;
        }
    }
}
