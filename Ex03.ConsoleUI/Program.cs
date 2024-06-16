using GarageLogic.Vehicles.VehicleFactory;
using System;
using System.Collections.Generic;
using VehicleGarage;
using static GarageLogic.Vehicles.Types.FuelVehicle;

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
            string leicensePlate = InputHandler.GetLicensePlate();
            if (garage.isVehicleInSystem(leicensePlate))
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

            //getVehiclefromsystem
            //
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
            while(true)
            {
                Console.WriteLine($"Please choose which vehicele to change its Status:");
                string leicencePlateOfVihacleToChnge = InputHandler.GetLicensePlate();
                Console.WriteLine(@"
Please choose what Status to change Vihacle into:
1. To repair
2. Was repaird
3. Was paid for");
                int userStatusChoice = InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, 3);

                try
                {
                    garage.ChangeVehicleStatus(leicencePlateOfVihacleToChnge, (eVehicleStatus)userStatusChoice);
                    Console.WriteLine($"Vihacle {leicencePlateOfVihacleToChnge} Status was changed succecfully");
                    break;
                }
                catch(ArgumentException exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            }

        }

        private void inflateVehicleWheels()
        {   while(true)
            {
                Console.WriteLine($"Please choose which vehicele to inflate its wheels:");
                string leicencePlateOfVihacleToInflate = InputHandler.GetLicensePlate();
                try
                {
                    garage.InflateWheelsToMaximum(leicencePlateOfVihacleToInflate);
                    break;
                }
                catch(ArgumentException exeption)
                    {
                    Console.WriteLine(exeption.Message);
                }
            }
            Console.WriteLine("Wheels where inflated succecfully");

        }

        private void feuelGasVehicle()
        {
            while(true)
            {
                Console.WriteLine($"Please choose which vehicele to refuel:");
                string leicencePlateOfVihacleToRefuel = InputHandler.GetLicensePlate();
                eFuelTypes GasTypeToFill = getGasTypeFromUser();
                
                Console.WriteLine($"Please choose amount of gas to fill:");
                try
                {
                    garage.RefuelVehicle(leicencePlateOfVihacleToRefuel, GasTypeToFill, InputHandler.GetFloatFromUser());
                    break;
                }
                catch (ArgumentException exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            }

            Console.WriteLine("Vehicele was reFeuld successfully:");
        }

        private eFuelTypes getGasTypeFromUser()
        {
            Console.WriteLine(@"Please enter Vehicle Gas Type:");
            foreach (eFuelTypes gasType in Enum.GetValues(typeof(eFuelTypes)))
            {
                Console.WriteLine($"{(int)gasType}. {gasType}");
            }

            return (eFuelTypes)(InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, 4));
        }

        private void chargeElectricVehicle()
        {
            while(true)
            {
                Console.WriteLine($"Please choose which vehicele to charge:");
                string leicencePlateOfVihacleToCharge = InputHandler.GetLicensePlate();
                Console.WriteLine($"Please choose how long to charge the car in minutes:");
                try
                {
                    garage.ChargeVehicle(leicencePlateOfVihacleToCharge, InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, int.MaxValue));
                    break;
                }
                catch(ArgumentException exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            }

            Console.WriteLine($"Vehicele was charged successfully:");
        }

        private void showAllInformationForAVehicle()
        {
            Console.WriteLine($"Please choose which vehicele to show:");
            string leicencePlateOfVihacleToCharge = InputHandler.GetLicensePlate();

            try
            {
                //Console.WriteLine(garage.);
            }
            catch( ArgumentException exeption)
            {
                Console.WriteLine(exeption.Message);
            }
        }
    }
}
