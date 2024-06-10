using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class Program
    {


        public void StartGarageProgram()
        {
            Garage garage; 
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
                changeVehicleSitoation();
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

            return InputHandler.GetUserChoice();
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

            }
        }



        private void showVehicels()
        {

        }

        private void changeVehicleSitoation()
        {

        }

        private void inflateVehicleWheels()
        {

        }

        private void feuelGasVehicle()
        {

        }

        private void chargeElectricVehicle()
        {

        }

        private void showAllInformationForAVehicle()
        {

        }
    }
}
