using System;
using static GarageLogic.Vehicles.Types.Car.CarInfo;
using static GarageLogic.Vehicles.Types.Motorcycle.MotorcycleInfo;

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

        internal static bool isExitStatment(string i_UserInput)
        {
            return i_UserInput == r_ExitInput;
        }

        internal static bool GetYesOrNoAnswer()
        {
            while (true)
            {
                Console.WriteLine("Please answer y/n:");
                string answer = Console.ReadLine();
                if (answer == "y" || answer == "n")
                {
                    return answer == "y";
                }

                Console.WriteLine("You entered the wrong input.");
            }
        }

        internal static string GetAStringFromUser(string i_wantedFormat)
        {
            string inputName;
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
                inputPhoneNumber = GetAStringFromUser("phone number");
            } while (!uint.TryParse(inputPhoneNumber, out catchNumber));

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
            } while (!int.TryParse(inputNumberString, out inputNumberInt) || inputNumberInt < i_minmumNumber || inputNumberInt > i_maximumNumber);

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

            } while (!float.TryParse(Console.ReadLine(), out inputNumberfloat) || inputNumberfloat < 0);

            return inputNumberfloat;
        }

        internal static eMotorcycleLicenseType GetMotorcycleLicenseType()
        {
            Console.WriteLine("Please enter one of the following options:");
            foreach (eMotorcycleLicenseType motorcycleLicenseType in Enum.GetValues(typeof(eMotorcycleLicenseType)))
            {
                Console.WriteLine($"{(int)motorcycleLicenseType}. {motorcycleLicenseType}");
            }

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int inputNumber))
                {
                    if (Enum.IsDefined(typeof(eMotorcycleLicenseType), inputNumber))
                    {
                        return (eMotorcycleLicenseType)inputNumber;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice, please try again");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a numeric value.");
                }
            }
        }

        internal static eCarColors GetCarColors()
        {
            Console.WriteLine("Please enter one of the following options:");
            foreach (eCarColors motorcycleLicenseType in Enum.GetValues(typeof(eCarColors)))
            {
                Console.WriteLine($"{(int)motorcycleLicenseType}. {motorcycleLicenseType}");
            }
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int inputNumber))
                {
                    if (Enum.IsDefined(typeof(eCarColors), inputNumber))
                    {
                        return (eCarColors)inputNumber;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice, please try again");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a numeric value.");
                }
            }
        }
    }
}