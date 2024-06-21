using GarageLogic.Exceptions;
using GarageLogic.Vehicles.VehicleFactory;
using GarageLogic.Vehicles;
using System;
using System.Collections.Generic;
using VehicleGarage;
using System.Linq;

namespace ConsoleUI
{
    internal class GarageUI
    {
        Garage m_Garage = new Garage();
        readonly int r_MinumumSizeOfNumericInput = 1;
        readonly int r_ProgramOptionsSize = Enum.GetValues(typeof(eProgramChoices)).Length;

        public void StartGarageProgram()
        {
            int userChoice;

            while (true)
            {
                userChoice = printMenuAndGetUserChoice();
                playChoice(userChoice);
            }
        }

        private void playChoice(int i_UserChoice)
        {
            if (i_UserChoice == (int)eProgramChoices.EnterVihacleToGarage)
            {
                enterVihacleToGarage();
            }
            else if (i_UserChoice == (int)eProgramChoices.ShowVehicels)
            {
                showVehicels();
            }
            else if (i_UserChoice == (int)eProgramChoices.ChangeVehicleStatus)
            {
                changeVehicleStatus();
            }
            else if (i_UserChoice == (int)eProgramChoices.InflateVehicleWheels)
            {
                inflateVehicleWheelsToMax();
            }
            else if (i_UserChoice == (int)eProgramChoices.FeuelGasVehicle)
            {
                refeuelVehicle();
            }
            else if (i_UserChoice == (int)eProgramChoices.ChargeElectricVehicle)
            {
                chargeElectricVehicle();
            }
            else if (i_UserChoice == (int)eProgramChoices.ShowAllInformationForAVehicle)
            {
                showAllInformationForAVehicle();
            }
        }

        private int printMenuAndGetUserChoice()
        {
            Console.WriteLine(@"
Please choose what you want to do:

1. Enter vehicle into garage.
2. Show all vehicale leicense plates in garage, according to categories.
3. Change vehicle status in garage.
4. Inflate vehicle wheels to maximum.
5. refeuel vehicle.
6. Charge electric vehicle.
7. Show all information of a vehicle.

Please write choice number: ");

            return InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, r_ProgramOptionsSize);
        }

        private void enterVihacleToGarage()
        {
            string leicensePlate = InputHandler.GetLicensePlate();
            int inRepair = 1;

            if (!m_Garage.IsVehicleInSystem(leicensePlate))
            {
                enterNewVehicleToSystem(leicensePlate);
            }

            m_Garage.ChangeVehicleStatus(leicensePlate, inRepair); 
        }

        private void enterNewVehicleToSystem(string i_LeicensePlate)
        {
            Console.WriteLine("You are entering a new vehicle to the garage, please provide the following information:");
            Console.WriteLine("Please enter vehicle owner name:");
            string ownerName = InputHandler.GetAStringFromUser();

            Console.WriteLine("Please enter vehicle owner phone number:");
            string ownerPhoneNumber = InputHandler.GetPhoneNumberFromUser();
            Vehicle newVehicle = createNewVehicle();

            enterDataToVehicle(newVehicle, i_LeicensePlate);
            m_Garage.EnterNewVehicleToGarage(newVehicle, ownerName, ownerPhoneNumber);
        }

        private void enterDataToVehicle(Vehicle io_Vehicle, string i_LeicenseID)
        {
            while(true)
            {
                try
                {
                    List<string> promtsToShowUser = io_Vehicle.OutputPromptsList();
                    List<string> listOfInformationOnVehicle = new List<string>();

                    for (int i = 0; i < promtsToShowUser.Count; i++)
                    {
                        Console.WriteLine($"Please enter {promtsToShowUser[i]}:");
                        listOfInformationOnVehicle.Add(InputHandler.GetAStringFromUser());
                    }

                    io_Vehicle.GatherInformationForVehicle(listOfInformationOnVehicle, i_LeicenseID);
                    enterWheelsInformationFromUser(io_Vehicle);
                    break;
                }
                catch(ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch(FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private Vehicle createNewVehicle()
        {
            List<string> promtsToShowUser = m_Garage.GetListOfVehicleTypesInGarage();
            int vehicleType;

            while (true)
            {
                Console.WriteLine("Please enter type of vehicle");

                for (int i = 0; i < promtsToShowUser.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {promtsToShowUser[i]}");
                }

                try
                {
                    vehicleType = InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, promtsToShowUser.Count);

                    return VehicleBuilder.BuildVehicle(vehicleType);
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void enterWheelsInformationFromUser(Vehicle io_Vehicle)
        {
            Console.WriteLine("Please choose 1. to enter all wheels information at once or 2. to enter each wheel separetly:");
            bool enterWheelsAtOnce = InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, 2) == 1;
            int numberOfWheels;

            while (true)
            {
                List<string> wheelsPrompts = io_Vehicle.OutputPromptListForWheel(out numberOfWheels); 
                List<string> wheelsInformationToInsert = new List<string>();
                List<string> duplicateString = new List<string>();
                bool firstTimeInLoop = true;

                for (int i = 0; i < numberOfWheels; i++)
                {
                    if (enterWheelsAtOnce)
                    {
                        if(firstTimeInLoop)
                        {
                            foreach (string promt in wheelsPrompts)
                            {
                                Console.WriteLine($"Please enter {promt}:");
                                duplicateString.Add((InputHandler.GetAStringFromUser()));
                            }

                            firstTimeInLoop = false;
                        }

                        wheelsInformationToInsert = wheelsInformationToInsert.Concat(duplicateString).ToList();
                    }
                    else
                    {
                        foreach (string promt in wheelsPrompts)
                        {
                            Console.WriteLine($"Please enter {promt}:");
                            wheelsInformationToInsert.Add((InputHandler.GetAStringFromUser()));
                        }
                    }
                }

                try
                {
                    io_Vehicle.EnterWheelsInformation(wheelsInformationToInsert);
                    break;
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void showVehicels()
        {
            List<string> leicencePlateList;

            while (true)
            {
                Console.WriteLine("Please choose which vehicle status to show:");
                int numerOfPrompt = 1;

                foreach(string prompt in m_Garage.GetVehicleStatusPrompt())
                {
                    Console.WriteLine($"{numerOfPrompt}. {prompt}");
                    numerOfPrompt++;
                }

                string usersChiceToShow = InputHandler.GetAStringFromUser();

                try
                {
                    leicencePlateList = m_Garage.GetVehiclesLicensePlateListByStatus(usersChiceToShow);
                    break;
                }
                catch(FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            printAllVehiclesList(leicencePlateList);
        }

        private void printAllVehiclesList(List<string> i_VehiclesLeicencePlateList)
        {
            if (i_VehiclesLeicencePlateList.Count == 0)
            {
                Console.WriteLine("There are no vehicles to show in this category");
            }
            else
            {
                foreach (string leicencePlate in i_VehiclesLeicencePlateList)
                {
                    Console.WriteLine(leicencePlate);
                }
            }
        }

        private void changeVehicleStatus()
        {
            while (true)
            {
                Console.WriteLine($@"
Please choose which vehicle to change its Status.
If you want to go back to menu press {InputHandler.r_ExitInput}");
                string leicencePlateOfVihacleToChnge = InputHandler.GetLicensePlate();

                try
                {
                    if (!InputHandler.IsExitStatment(leicencePlateOfVihacleToChnge))
                    {
                        Console.WriteLine(@"
Please choose what status to change vehicle into:
1. To repair
2. Was repaird
3. Was paid for");
                        int userStatusChoice = InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, 3);

                        m_Garage.ChangeVehicleStatus(leicencePlateOfVihacleToChnge, userStatusChoice);
                        Console.WriteLine($"Vehicle {leicencePlateOfVihacleToChnge} status was changed succecfully");
                    }

                    break;
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void inflateVehicleWheelsToMax()
        {
            while (true)
            {
                Console.WriteLine($@"
Please choose which vehicle to inflate its wheels.
If you want to go back to menu press {InputHandler.r_ExitInput}");
                string leicencePlateOfVihacleToInflate = InputHandler.GetLicensePlate();

                try
                {
                    if (!InputHandler.IsExitStatment(leicencePlateOfVihacleToInflate))
                    {
                        m_Garage.InflateWheelsToMaximum(leicencePlateOfVihacleToInflate);
                        Console.WriteLine("Wheels where inflated succecfully to maximum");
                    }

                    break;
                }
                catch (ArgumentOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void refeuelVehicle()
        {
            while (true)
            {
                Console.WriteLine($@"
Please choose which vehicle to refuel.
If you want to go back to menu press {InputHandler.r_ExitInput}.");
                string leicencePlateOfVihacleToRefuel = InputHandler.GetLicensePlate();

                try
                {
                    if (!InputHandler.IsExitStatment(leicencePlateOfVihacleToRefuel) && !m_Garage.IsElectricVehicle(leicencePlateOfVihacleToRefuel))
                    {
                        Console.WriteLine(@"Please enter vehicle fuel Type:");
                        string gasTypeToFill = InputHandler.GetAStringFromUser();

                        Console.WriteLine(@"Please choose amount of fuel to fill in litters " +
                            $"(there are {m_Garage.GetEnergyLeftToBeFilled(leicencePlateOfVihacleToRefuel)} litters left to fill):");
                        m_Garage.RefuelVehicle(leicencePlateOfVihacleToRefuel, gasTypeToFill, InputHandler.GetFloatFromUser());
                        Console.WriteLine("Vehicle was refeuld successfully.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("This is not a fuel vehicle !");
                    }
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void chargeElectricVehicle()
        {
            while (true)
            {
                Console.WriteLine($@"
Please choose which vehicle to charge.
If you want to go back to menu press {InputHandler.r_ExitInput}.");
                string leicencePlateOfVihacleToCharge = InputHandler.GetLicensePlate();

                try
                {
                    if (!InputHandler.IsExitStatment(leicencePlateOfVihacleToCharge) && m_Garage.IsElectricVehicle(leicencePlateOfVihacleToCharge))
                    {
                        Console.WriteLine($"Please choose how long to charge the car in minutes " +
                            $"((there are {m_Garage.GetEnergyLeftToBeFilled(leicencePlateOfVihacleToCharge) * 60} minutes left to charge):");
                        m_Garage.ChargeVehicle(leicencePlateOfVihacleToCharge, (InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, int.MaxValue)) / 60);
                        Console.WriteLine($"Vehicle was charged successfully.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("This is not an electric vehicle !");
                    }
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void showAllInformationForAVehicle()
        {
            while (true)
            {
                Console.WriteLine($@"
Please choose which vehicle to show.
If you want to go back to menu press {InputHandler.r_ExitInput}");
                try
                {
                    string leicensePlateToShow = InputHandler.GetLicensePlate();

                    if (!InputHandler.IsExitStatment(leicensePlateToShow))
                    {
                        Console.WriteLine(m_Garage.GetVehicleInformation(leicensePlateToShow));
                    }

                    break;
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }
}
