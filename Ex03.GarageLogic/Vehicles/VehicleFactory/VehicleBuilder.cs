using GarageLogic.Vehicles.Types.Car;
using GarageLogic.Vehicles.Types.Motorcycle;
using GarageLogic.Vehicles.Types.Truck;
using System;

namespace GarageLogic.Vehicles.VehicleFactory
{
    public class VehicleBuilder
    {
        static public Vehicle BuildVehicle(int i_VehicleType)
        {
            Vehicle resultVehicle;

            validateVehicleType(i_VehicleType);
            switch ((eVehicleType)i_VehicleType)
            {
                case eVehicleType.FuelCar:
                    resultVehicle = new FuelCar();
                    break;
                case eVehicleType.ElectricCar:
                    resultVehicle = new ElectricCar();
                    break;
                case eVehicleType.FuealMotorcycle:
                    resultVehicle = new FuelMotorcycle();
                    break;
                case eVehicleType.ElectricMotorcycle:
                    resultVehicle = new ElectricMotorcycle();
                    break;                    
                case eVehicleType.FuelTruck:
                    resultVehicle = new FuelTruck();
                    break;                    
                default:
                    throw new ArgumentException("Invalid vehicle type!");
            }

            return resultVehicle;
        }

        static private void validateVehicleType(int i_VehicleType)
        {
            if (!Enum.IsDefined(typeof(eVehicleType), i_VehicleType))
            {
                throw new ArgumentException("Invalid vehicle type!");
            }
        }
    }
}