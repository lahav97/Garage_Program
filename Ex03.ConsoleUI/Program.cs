using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleGarage;
using Vehicles;

namespace Ex03.ConsoleUI
{
    internal class Program
    {
        Garage garage;
        readonly int r_MinumumSizeOfNumericInput = 1;
        readonly int r_ProgramOptionsSize = Enum.GetValues(typeof(eProgramChoices)).Length;        
        private enum eProgramChoices
        {
            EnterVihacleToGarage = 1,
            ShowVehicels,
            ChangeVehicleSituation,
            InflateVehicleWheels,
            FeuelGasVehicle,
            ChargeElectricVehicle,
            ShowAllInformationForAVehicle
        }

        public void StartGarageProgram()
        {
            
            int userChoice;

            while (true)
            {
                userChoice = printMenuAndGetUserChoice();
                playChoice(userChoice);
            }
        }

        private void playChoice(int i_userChoice)
        {
            if (i_userChoice == (int)eProgramChoices.EnterVihacleToGarage)
            {
                EnterVihacleToGarage();
            }
            else if (i_userChoice == (int)eProgramChoices.ShowVehicels)
            {
                showVehicels();
            }
            else if (i_userChoice == (int)eProgramChoices.ChangeVehicleSituation)
            {
                changeVehicleSituation();
            }
            else if (i_userChoice == (int)eProgramChoices.InflateVehicleWheels)
            {
                inflateVehicleWheels();
            }
            else if (i_userChoice == (int)eProgramChoices.FeuelGasVehicle)
            {
                feuelGasVehicle();
            }
            else if (i_userChoice == (int)eProgramChoices.ChargeElectricVehicle)
            {
                chargeElectricVehicle();
            }
            else if (i_userChoice == (int)eProgramChoices.ShowAllInformationForAVehicle)
            {
                showAllInformationForAVehicle();
            }
        }

        private int printMenuAndGetUserChoice()
        {
            Console.WriteLine(@"
Please choose what you want to do:

1. Enter vehicle into Garage.
2. Show all vehicales in Garage.
3. Change vehicle Situation.
4. Inflate vehicle wheels.
5. Feuel gas vehicle.
6. Charge Electric vehicle.
7. Show all information for a vehicle.

please write choice number: ");

            return InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, r_ProgramOptionsSize);
        }

        private void EnterVihacleToGarage()
        {
            string leicensePlate = InputHandler.GetLeicensePlate();
            if (garage.IsVehicleInSystem(leicensePlate))
            {
                garage.ReEnterVehicleToGarage(leicensePlate);
            }
            else
            {
                //TO DO
            }
        }

        private void EnterNewCarToSystem(int i_leicensePlate)
        {
            Console.WriteLine("You are entering a new Vihecle to the garage, please provide the following information:");
            Console.WriteLine("Please enter Car owner name:");
            string ownerName = InputHandler.GetAStringFromUser("car owner name");

            Console.WriteLine("Please enter Car owner Phone number:");
            string ownerPhoneNumber = InputHandler.GetAStringFromUser("phone number");

            Console.WriteLine();
        }

        private void showVehicels()
        {
            Console.WriteLine(@"
Please choose which vehicels to show:
1. All vihacels being repaird.
2. All repaired vihacels.
3. All vihacels that where paid for.
4. All vihacelss in garage.");
            
            int usersChiceToShow = InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, 4);
            List<string> leicencePlateList = new List<string>();

            if(usersChiceToShow == 4) 
            {
                //leicencePlateList = //get all leicnse plates
            }
            else
            {
                //leicencePlateList = get specific leicnse plates
            }

            if(leicencePlateList.Count == 0)
            {
                Console.WriteLine("There are no Vihacles to show in this category");
            }
            else
            {
                foreach (string leicencePlate in leicencePlateList)
                {
                    Console.WriteLine(leicencePlate);
                }
            }
        }

        private void changeVehicleSituation()
        {
            Console.WriteLine($"Please choose which vehicele to change its Status:");
            string leicencePlateOfVihacleToChnge = InputHandler.GetLeicensePlate();
            Console.WriteLine(@"
Please choose what Status to change Vihacle into:
1. To repair
2. Was repaird
3. Was paid for");
            int userStatusChoice = InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, 3);
            garage.ChangeVehicleStatus(leicencePlateOfVihacleToChnge, getVehicleStatusEnumChoice(userStatusChoice));
            //catch
            { 
                // TO DO ! ! !
            }
            Console.WriteLine($"Vihacle {leicencePlateOfVihacleToChnge} Status was changed succecfully");
        }

        private void inflateVehicleWheels()
        {
            Console.WriteLine($"Please choose which vehicele to inflate its wheels:");
            string leicencePlateOfVihacleToInflate = InputHandler.GetLeicensePlate();

            garage.InflateWheelsToMaximum(leicencePlateOfVihacleToInflate);
            //catch
            {
                // TO DO ! ! !
            }
            Console.WriteLine("Wheels where inflated succecfully");
        }

        private void feuelGasVehicle()
        {
            Console.WriteLine($"Please choose which vehicele to refuel:");
            string leicencePlateOfVihacleToRefuel = InputHandler.GetLeicensePlate();
            eGasTypes GasTypeToFill = getGasTypeFromUser();

            Console.WriteLine($"Please choose amount of gas to fill:");
            garage.RefuelVehicle(leicencePlateOfVihacleToRefuel, GasTypeToFill, InputHandler.GetFloatFromUser());
            //catch
            {
                // TO DO ! ! !
            }
        }

        private eGasTypes getGasTypeFromUser()
        {
            Console.WriteLine(@"Please enter Vehicle Gas Type:");
            foreach (eGasTypes gasType in Enum.GetValues(typeof(eGasTypes)))
            {
                Console.WriteLine($"{(int)gasType}. {gasType}");
            }

            return (eGasTypes)(InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, 4));
        }

        private void chargeElectricVehicle()
        {
            Console.WriteLine($"Please choose which vehicele to charge:");
            string leicencePlateOfVihacleToCharge = InputHandler.GetLeicensePlate();
            Console.WriteLine($"Please choose how long to charge the car in minutes:");

            garage.ChargeVehicle(leicencePlateOfVihacleToCharge, InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, int.MaxValue));
            //catch
            {
                //TO DO!!!
            }
        }

        private void showAllInformationForAVehicle()
        {

        }
    }
}
