using System;

namespace Ex03.ConsoleUI
{
    internal class InputHandler
    {
        internal readonly static string r_ExitInput = "-1";

        internal static string GetLicensePlate()
        {
            string leicensePlate;
            bool firstTimeInLoop = true;

            do
            {
                if (!firstTimeInLoop)
                {
                    Console.WriteLine($"the input you enterd is wrong, leicense plate has to contain letters and/or numbers");
                }

                firstTimeInLoop = false;
                Console.WriteLine("Please Enter Vihacle leicense plate:");
                leicensePlate = Console.ReadLine();
                leicensePlate = leicensePlate.Trim();
            } while (leicensePlate.Length == 0);

            return leicensePlate;
        }

        internal static bool IsExitStatment(string i_UserInput)
        {
            return i_UserInput == r_ExitInput;
        }

        internal static string GetAStringFromUser()
        {
            string inputName;
            bool firstTimeInLoop = true;

            do
            {
                if (!firstTimeInLoop)
                {
                    Console.WriteLine($"The input You enterd is incorect, please enter again:");
                }

                inputName = Console.ReadLine();
                firstTimeInLoop = false;
            } while (string.IsNullOrEmpty(inputName));

            return inputName;
        }

        internal static string GetPhoneNumberFromUser()
        {
            string inputPhoneNumber;
            uint catchNumber;
            bool firstTimeInLoop = true;

            do
            {
                if (!firstTimeInLoop)
                {
                    Console.WriteLine("your input is not a valid phone number, please enter numeric phone number.");
                }

                firstTimeInLoop = false;
                inputPhoneNumber = GetAStringFromUser();
            }while (!uint.TryParse(inputPhoneNumber, out catchNumber));

            return inputPhoneNumber;
        }

        internal static int GetInputNumberFromUser(int i_minmumNumber, int i_maximumNumber)
        {
            string inputNumberString;
            int inputNumberInt;
            bool firstTimeInLoop = true;

            do
            {
                if (!firstTimeInLoop)
                {
                    Console.WriteLine($"The input You enterd is out of range, please enter a number between {i_minmumNumber} - {i_maximumNumber}");
                }

                inputNumberString = Console.ReadLine();
                firstTimeInLoop = false;
            }while (!int.TryParse(inputNumberString, out inputNumberInt) || inputNumberInt < i_minmumNumber || inputNumberInt > i_maximumNumber);

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
                    Console.WriteLine($"The input You enterd is incorrect");
                }
                firstTimeInLoop = false;

            }while (!float.TryParse(Console.ReadLine(), out inputNumberfloat) || inputNumberfloat < 0);

            return inputNumberfloat;
        }
    }
}