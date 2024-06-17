using GarageLogic.Exceptions;
using GarageLogic.Vehicles.Types.Motorcycle;
using GarageLogic.Vehicles.Types.Truck;
using GarageLogic.Vehicles.VehicleFactory;
using GarageLogic.Vehicles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static GarageLogic.Vehicles.Types.FuelVehicle;
using VehicleGarage;

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
            if (m_Garage.IsVehicleInSystem(leicensePlate))
            {
                m_Garage.ReEnterVehicleToGarage(leicensePlate);
            }
            else
            {
                EnterNewVehicleToSystem(leicensePlate);
            }
        }

        private void EnterNewVehicleToSystem(string i_leicensePlate)
        {
            Console.WriteLine("You are entering a new Vehicle to the m_Garage, please provide the following information:");
            Console.WriteLine("Please enter Vehicle owner name:");
            string ownerName = InputHandler.GetAStringFromUser("car owner name");

            Console.WriteLine("Please enter Vehicle owner Phone number:");
            string ownerPhoneNumber = InputHandler.GetAStringFromUser("phone number");

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
                io_vehicleType = (eVehicleType)InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, Marshal.SizeOf(typeof(eVehicleType)));
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
            string modelName = InputHandler.GetAStringFromUser("model name");

            Console.WriteLine("Please enter energy left in vehicle:");
            float energyLeftInVehicle = InputHandler.GetFloatFromUser();

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
        }

        private List<Wheel> EnterWheelsInformationFromUser(eNumberOfWheels i_NumberOfWheels, eMaxWheelAirPressure i_MaxWheelAirPreasure)
        {
            Console.WriteLine("Please choose 1. to enter all wheels information at once or 2. to enter each wheel separetly:");
            bool enterWheelsAtOnce = InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, 2) == 1;
            List<Wheel> wheelsList = new List<Wheel>();

            if (enterWheelsAtOnce)
            {
                Wheel prototypeWheel = GetInformationForAWheel(i_MaxWheelAirPreasure);

                for (int i = 0; i < (int)i_NumberOfWheels; i++)
                {
                    Wheel newWheel = new Wheel((float)i_MaxWheelAirPreasure)
                    {
                        ManufactureName = prototypeWheel.ManufactureName,
                        CurrentAirPressure = prototypeWheel.CurrentAirPressure
                    };
                    wheelsList.Add(newWheel);
                }
            }
            else
            {
                for (int i = 0; i < (int)i_NumberOfWheels; i++)
                {
                    Wheel newWheel = GetInformationForAWheel(i_MaxWheelAirPreasure);
                    wheelsList.Add(newWheel);
                }
            }

            return wheelsList;
        }

        private Wheel GetInformationForAWheel(eMaxWheelAirPressure i_MaxWheelAirPreasure)
        {
            Wheel newWheel = new Wheel((float)i_MaxWheelAirPreasure);
            Console.WriteLine("Please enter Wheel manufacture name:");
            newWheel.ManufactureName = InputHandler.GetAStringFromUser("manufacture name");
            while (true)
            {
                try
                {
                    Console.WriteLine($"Please enter current Wheel air preasure, air preasure must be lower than{i_MaxWheelAirPreasure}:");
                    newWheel.CurrentAirPressure = InputHandler.GetFloatFromUser();
                    break;
                }
                catch (GarageLogic.Exceptions.ValueOutOfRangeException exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            }

            return newWheel;
        }

        private void GetInformationForCar(Vehicle io_vehicle)
        {
            io_vehicle.Wheels = EnterWheelsInformationFromUser(eNumberOfWheels.Car, eMaxWheelAirPressure.Car);
            Console.WriteLine("What is the cars color?");
            //((CarInfor)io_vehicle.VehicleInfo).CarColor = GetCarColors();
            Console.WriteLine("how many doors does the car have (2-5)");
            //((CarInfor)io_vehicle.VehicleInfo).NumberOfDoors = InputHandler.GetInputNumberFromUser(2, 5);
        }

        private void GetInformationForTruck(Vehicle io_vehicle)
        {
            io_vehicle.Wheels = EnterWheelsInformationFromUser(eNumberOfWheels.Truck, eMaxWheelAirPressure.Truck);
            Console.WriteLine("Does the truck transport hazardous materials?");
            ((TruckInfo)io_vehicle.VehicleInfo).TransportsHazardousMaterials = InputHandler.GetYesOrNoAnswer();
            Console.WriteLine("What is the truck's cargo volume?");
            ((TruckInfo)io_vehicle.VehicleInfo).CargoVolume = InputHandler.GetFloatFromUser();
        }

        private void GetInformationForMotorcycle(Vehicle io_vehicle)
        {
            io_vehicle.Wheels = EnterWheelsInformationFromUser(eNumberOfWheels.MotorCycle, eMaxWheelAirPressure.Motorcycle);
            Console.WriteLine("Please enter motercycle leicense:");
            //((MotorcycleInfo)io_vehicle.VehicleInfo).MotorcycleLicense = GetMotorcycleLicenseType();

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
                //leicencePlateList = //get all leicnse plates
            }
            else
            {
                //leicencePlateList = get specific leicnse plates
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
                Console.WriteLine($"Please choose which vehicle to change its Status:");
                string leicencePlateOfVihacleToChnge = InputHandler.GetLicensePlate();
                Console.WriteLine(@"
Please choose what Status to change Vehicle into:
1. To repair
2. Was repaird
3. Was paid for");
                int userStatusChoice = InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, 3);

                try
                {
                    m_Garage.ChangeVehicleStatus(leicencePlateOfVihacleToChnge, (eVehicleStatus)userStatusChoice);
                    Console.WriteLine($"Vehicle {leicencePlateOfVihacleToChnge} Status was changed succecfully");
                    break;
                }
                catch (ArgumentException exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            }
        }

        private void inflateVehicleWheels()
        {
            while (true)
            {
                Console.WriteLine($"Please choose which vehicle to inflate its wheels:");
                string leicencePlateOfVihacleToInflate = InputHandler.GetLicensePlate();
                try
                {
                    m_Garage.InflateWheelsToMaximum(leicencePlateOfVihacleToInflate);
                    break;
                }
                catch (ArgumentException exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            }

            Console.WriteLine("Wheels where inflated succecfully");
        }

        private void feuelGasVehicle()
        {
            while (true)
            {
                Console.WriteLine($"Please choose which vehicle to refuel:");
                string leicencePlateOfVihacleToRefuel = InputHandler.GetLicensePlate();
                eFuelTypes GasTypeToFill = getGasTypeFromUser();

                Console.WriteLine($"Please choose amount of gas to fill:");
                try
                {
                    m_Garage.RefuelVehicle(leicencePlateOfVihacleToRefuel, GasTypeToFill, InputHandler.GetFloatFromUser());
                    break;
                }
                catch (ArgumentException exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            }

            Console.WriteLine("Vehicle was reFeuld successfully:");
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
                Console.WriteLine($"Please choose which vehicle to charge:");
                string leicencePlateOfVihacleToCharge = InputHandler.GetLicensePlate();
                Console.WriteLine($"Please choose how long to charge the car in minutes:");
                try
                {
                    m_Garage.ChargeVehicle(leicencePlateOfVihacleToCharge, InputHandler.GetInputNumberFromUser(r_MinumumSizeOfNumericInput, int.MaxValue));
                    break;
                }
                catch (ArgumentException exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            }

            Console.WriteLine($"Vehicle was charged successfully:");
        }

        private void showAllInformationForAVehicle()
        {
            while (true)
            {
                Console.WriteLine($"Please choose which vehicele to show:");
                try
                {
                    Console.WriteLine(m_Garage.GetVehicleInformation(InputHandler.GetLicensePlate()));
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