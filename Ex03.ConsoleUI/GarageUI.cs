using GarageLogic.Exceptions;
using GarageLogic.Vehicles.Types.Motorcycle;
using GarageLogic.Vehicles.Types.Truck;
using GarageLogic.Vehicles.VehicleFactory;
using GarageLogic.Vehicles;
using System;
using System.Collections.Generic;
using static GarageLogic.Vehicles.Types.FuelVehicle;
using VehicleGarage;
using System.Linq;
using GarageLogic.Vehicles.Types.Car;
using static GarageLogic.Vehicles.Types.Car.CarInfo;
using GarageLogic.Vehicles.Types;


namespace Ex03.ConsoleUI
{
    internal class GarageUI
    {
        Garage m_Garage = new Garage();
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
                inflateVehicleWheelsToMax();
            }
            else if (i_userChoice == (int)eProgramChoices.FeuelGasVehicle)
            {
                refeuelVehicle();
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
2. Show all vehicale leicense plates in Garage, according to categories.
3. Change vehicle Situation in garage.
4. Inflate vehicle wheels to maximum.
5. refeuel vehicle.
6. Charge Electric vehicle.
7. Show all information of a vehicle.

please write choice number: ");

            return InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, r_ProgramOptionsSize);
        }

        private void EnterVihacleToGarage()
        {
            string leicensePlate = InputHandler.GetLicensePlate();

            if (!m_Garage.IsVehicleInSystem(leicensePlate))
            {
                EnterNewVehicleToSystem(leicensePlate);
            }

            m_Garage.ChangeVehicleStatus(leicensePlate, eVehicleStatus.InRepair);
        }

        private void EnterNewVehicleToSystem(string i_leicensePlate)
        {
            Console.WriteLine("You are entering a new Vehicle to the m_Garage, please provide the following information:");
            Console.WriteLine("Please enter Vehicle owner name:");
            string ownerName = InputHandler.GetAStringFromUser("car owner name");

            Console.WriteLine("Please enter Vehicle owner Phone number:");
            string ownerPhoneNumber = InputHandler.GetPhoneNumberFromUser();

            while (true)
            {
                try
                {
                    eVehicleType vehicleType = new eVehicleType();
                    Vehicle newVehicle = CreateNewVehicle(out vehicleType);
                    newVehicle.VehicleInfo.LicensePlateID = i_leicensePlate;
                    EnterDataToVehicle(newVehicle, vehicleType);

                    m_Garage.EnterNewVehicleToGarage(newVehicle, ownerName, ownerPhoneNumber);
                    break;
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private Vehicle CreateNewVehicle(out eVehicleType io_vehicleType)
        {
            while (true)
            {
                Console.WriteLine(@"
Please enter Vehicle Type:

1. Fuel Car
2. Electric Car,
3. Fueal Motorcycle,
4. Electric Motorcycle,
5. Fuel Truck");
                io_vehicleType = (eVehicleType)InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, (int)Enum.GetValues(typeof(eVehicleType)).Cast<eVehicleType>().Max());
                try
                {
                    return VehicleBuilder.BuildVehicle(io_vehicleType);
                }
                catch (ArgumentException exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            }
        }

        private void EnterDataToVehicle(Vehicle io_vehicle, eVehicleType i_vehicleType)
        {
            Console.WriteLine("Please enter model name:");
            io_vehicle.VehicleInfo.ModelName = InputHandler.GetAStringFromUser("model name");
            Console.WriteLine("Please enter energy Percentage left in vehicle:");
            float energyPercentageInput;

            while ((energyPercentageInput = InputHandler.GetFloatFromUser()) > 100)
            {
                Console.WriteLine("Energy Percentage to high please enter again");
            }

            io_vehicle.VehicleInfo.EnergyPercentageLeft = energyPercentageInput;

            EnterWheelsInformationFromUser(io_vehicle);
            if (i_vehicleType == eVehicleType.ElectricMotorcycle || i_vehicleType == eVehicleType.FuealMotorcycle)
            {
                GetInformationForMotorcycle(io_vehicle);
            }
            else if (i_vehicleType == eVehicleType.FuelTruck)
            {
                GetInformationForTruck(io_vehicle);
            }
            else
            {
                GetInformationForCar(io_vehicle);
            }

            if (i_vehicleType == eVehicleType.ElectricMotorcycle || i_vehicleType == eVehicleType.ElectricCar)
            {
                ((ElectricVehicle)io_vehicle).RemainingBatteryTime = (energyPercentageInput * ((ElectricVehicle)io_vehicle).MaxBatteryCapacity) / 100;
            }
            else
            {
                ((FuelVehicle)io_vehicle).RemainingFuel = (energyPercentageInput * ((FuelVehicle)io_vehicle).MaxFuelTank) / 100;
            }
        }

        private void EnterWheelsInformationFromUser(Vehicle io_vehicle)
        {
            Console.WriteLine("Please choose 1. to enter all wheels information at once or 2. to enter each wheel separetly:");
            bool enterWheelsAtOnce = InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, 2) == 1;
            bool firstTimeInLoop = true;
            float AirPressureToFill = 0;
            string ManufactureName = "";

            foreach (Wheel wheel in io_vehicle.Wheels)
            {
                while (true)
                {
                    try
                    {
                        if (firstTimeInLoop || !enterWheelsAtOnce)
                        {
                            firstTimeInLoop = false;
                            GetInformationForAWheel(out AirPressureToFill, out ManufactureName, wheel.MaxAirPressure);
                        }

                        wheel.CurrentAirPressure = AirPressureToFill;
                        wheel.ManufactureName = ManufactureName;
                        break;
                    }
                    catch (ValueOutOfRangeException exeption)
                    {
                        Console.WriteLine(exeption.Message);
                    }
                }
            }
        }

        private void GetInformationForAWheel(out float o_AirPressureToFill, out string i_ManufactureName, float i_MaxWheelAirPreasure)
        {
            Console.WriteLine("Please enter Wheel manufacture name:");
            i_ManufactureName = InputHandler.GetAStringFromUser("manufacture name");
            Console.WriteLine($"Please enter current Wheel air preasure, air preasure must be up to {i_MaxWheelAirPreasure}:");
            o_AirPressureToFill = InputHandler.GetFloatFromUser();
        }

        private void GetInformationForCar(Vehicle io_vehicle)
        {
            int minNumberOfDoors = (int)Enum.GetValues(typeof(eNumberOfDoors)).Cast<eNumberOfDoors>().Min();
            int maxNumberOfDoors = (int)Enum.GetValues(typeof(eNumberOfDoors)).Cast<eNumberOfDoors>().Max();

            Console.WriteLine("What is the cars color?");
            ((CarInfo)io_vehicle.VehicleInfo).CarColor = InputHandler.GetCarColors();
            Console.WriteLine($"how many doors does the car have ({minNumberOfDoors}-{maxNumberOfDoors})");
            ((CarInfo)io_vehicle.VehicleInfo).NumberOfDoors = (eNumberOfDoors)InputHandler.GetInputNumberFromUser(minNumberOfDoors, maxNumberOfDoors);
        }

        private void GetInformationForTruck(Vehicle io_vehicle)
        {
            Console.WriteLine("Does the truck transport hazardous materials?");
            ((TruckInfo)io_vehicle.VehicleInfo).TransportsHazardousMaterials = InputHandler.GetYesOrNoAnswer();
            Console.WriteLine("What is the truck's cargo volume?");
            ((TruckInfo)io_vehicle.VehicleInfo).CargoVolume = InputHandler.GetFloatFromUser();
        }

        private void GetInformationForMotorcycle(Vehicle io_vehicle)
        {
            Console.WriteLine("Please enter motercycle leicense:");
            ((MotorcycleInfo)io_vehicle.VehicleInfo).MotorcycleLicenseType = InputHandler.GetMotorcycleLicenseType();
            Console.WriteLine("Please enter motercycle Engine Volume:");
            ((MotorcycleInfo)io_vehicle.VehicleInfo).EngineVolume = InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, int.MaxValue);
        }

        private void showVehicels()
        {
            Console.WriteLine(@"
Please choose which vehicles to show:
1. All vehicles being repaird.
2. All repaired vehicles.
3. All vehicles that where paid for.
4. All vehicles in m_Garage.");

            int usersChiceToShow = InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, 4);
            List<string> leicencePlateList = new List<string>();

            if (usersChiceToShow == 4)
            {
                leicencePlateList = m_Garage.GetAllLicensePlatesInGarage();
            }
            else
            {
                leicencePlateList = m_Garage.GetVehiclesLicensePlateListByStatus((eVehicleStatus)usersChiceToShow);
            }

            if (leicencePlateList.Count == 0)
            {
                Console.WriteLine("There are no Vehicles to show in this category");
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
            while (true)
            {
                Console.WriteLine($@"
Please choose which vehicle to change its Status
if you want to go back to menu press {InputHandler.r_ExitInput}");
                string leicencePlateOfVihacleToChnge = InputHandler.GetLicensePlate();

                try
                {
                    if (!InputHandler.isExitStatment(leicencePlateOfVihacleToChnge))
                    {
                        Console.WriteLine(@"
Please choose what Status to change Vehicle into:
1. To repair
2. Was repaird
3. Was paid for");
                        int userStatusChoice = InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, 3);

                        m_Garage.ChangeVehicleStatus(leicencePlateOfVihacleToChnge, (eVehicleStatus)userStatusChoice);
                        Console.WriteLine($"Vehicle {leicencePlateOfVihacleToChnge} Status was changed succecfully");
                    }
                    break;
                }
                catch (ArgumentException exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            }
        }

        private void inflateVehicleWheelsToMax()
        {
            while (true)
            {
                Console.WriteLine($@"
Please choose which vehicle to inflate its wheels
if you want to go back to menu press {InputHandler.r_ExitInput}");
                string leicencePlateOfVihacleToInflate = InputHandler.GetLicensePlate();
                try
                {
                    if (!InputHandler.isExitStatment(leicencePlateOfVihacleToInflate))
                    {
                        m_Garage.InflateWheelsToMaximum(leicencePlateOfVihacleToInflate);
                        Console.WriteLine("Wheels where inflated succecfully to Maximum");
                    }

                    break;
                }
                catch (ArgumentOutOfRangeException exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
                catch (ArgumentException exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            }
        }

        private void refeuelVehicle()
        {
            while (true)
            {
                Console.WriteLine($@"
Please choose which vehicle to refuel
if you want to go back to menu press {InputHandler.r_ExitInput}");
                string leicencePlateOfVihacleToRefuel = InputHandler.GetLicensePlate();

                try
                {
                    if (!InputHandler.isExitStatment(leicencePlateOfVihacleToRefuel))
                    {
                        eFuelTypes GasTypeToFill = getGasTypeFromUser();
                        Console.WriteLine(@"Please choose amount of gas to fill in Litters " +
                            $"(there are {m_Garage.GetEnergyLeftToBeFilled(leicencePlateOfVihacleToRefuel)} litters left to fill):");
                        m_Garage.RefuelVehicle(leicencePlateOfVihacleToRefuel, GasTypeToFill, InputHandler.GetFloatFromUser());
                        Console.WriteLine("Vehicle was refeuld successfully:");
                    }

                    break;
                }
                catch (ArgumentException exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            }
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
            while (true)
            {
                Console.WriteLine($@"
Please choose which vehicle to charge
if you want to go back to menu press {InputHandler.r_ExitInput}");
                string leicencePlateOfVihacleToCharge = InputHandler.GetLicensePlate();

                try
                {
                    if (!InputHandler.isExitStatment(leicencePlateOfVihacleToCharge))
                    {
                        Console.WriteLine($"Please choose how long to charge the car in minutes " +
                            $"((there are {m_Garage.GetEnergyLeftToBeFilled(leicencePlateOfVihacleToCharge) * 60} minutes left to charge):");
                        m_Garage.ChargeVehicle(leicencePlateOfVihacleToCharge, (InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, int.MaxValue)) / 60);
                        Console.WriteLine($"Vehicle was charged successfully:");
                    }

                    break;
                }
                catch (ArgumentException exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            }
        }

        private void showAllInformationForAVehicle()
        {
            while (true)
            {
                Console.WriteLine($@"
Please choose which vehicle to show
if you want to go back to menu press {InputHandler.r_ExitInput}");
                try
                {
                    string LeicensePlateToShow = InputHandler.GetLicensePlate();

                    if (!InputHandler.isExitStatment(LeicensePlateToShow))
                    {
                        Console.WriteLine(m_Garage.GetVehicleInformation(LeicensePlateToShow));
                    }

                    break;
                }
                catch (ArgumentException exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            }
        }
    }
}