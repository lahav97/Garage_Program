using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VehicleGarage;
using Vehicles;

namespace Ex03.ConsoleUI
{
    internal class Program
    {
        Garage garage;
        readonly int r_MinumumSizeOfNumericInput = 1;
        readonly int r_ProgramOptionsSize = 7;
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
            if (i_userChoice == 1)
            {
                EnterVihacleToGarage();
            }
            else if (i_userChoice == 2)
            {
                showVehicels();
            }
            else if (i_userChoice == 3)
            {
                changeVehicleSituation();
            }
            else if (i_userChoice == 4)
            {
                inflateVehicleWheels();
            }
            else if (i_userChoice == 5)
            {
                feuelGasVehicle();
            }
            else if (i_userChoice == 6)
            {
                chargeElectricVehicle();
            }
            else if (i_userChoice == 7)
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
            
            foreach (string leicencePlate in leicencePlateList)
            {
                Console.WriteLine(leicencePlate);
            }
        }

        private eVehicleStatus getVehicleStatusEnumChoice(int i_usersChice)
        {
            eVehicleStatus UserChice = new eVehicleStatus();

            if (i_usersChice == 1)
            {
                UserChice = eVehicleStatus.InRepair;
            }
            else if (i_usersChice == 2)
            {
                UserChice = eVehicleStatus.WasRepair;
            }
            else
            {
                UserChice = eVehicleStatus.InRepair;
            }

            return UserChice;
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
            Console.WriteLine(@"
Please enter Vehicle Gas Type:
1. Soler,
2. Octan95
3. Octan96
4. Octan98");

            return changeInputToEGasType(InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, 4));
        }

        private eGasTypes changeInputToEGasType(int i_usersChice)
        {
            eGasTypes UserChice = new eGasTypes();

            if (i_usersChice == 1)
            {
                UserChice = eGasTypes.Soler;
            }
            else if (i_usersChice == 2)
            {
                UserChice = eGasTypes.Octan95;
            }
            else if(i_usersChice == 3)
            {
                UserChice = eGasTypes.Octan96;
            }
            else
            {
                UserChice = eGasTypes.Octan98;
            }

            return UserChice;
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
