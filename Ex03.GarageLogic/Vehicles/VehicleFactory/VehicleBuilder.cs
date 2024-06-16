using GarageLogic.Vehicles.Types.Car;
using GarageLogic.Vehicles.Types.Motorcycle;
using GarageLogic.Vehicles.Types.Truck;
using System;

namespace GarageLogic.Vehicles.VehicleFactory
{
    public class VehicleBuilder
    {
        public static Vehicle BuildVehicle(eVehicleType i_VehicleType)
        {
            Vehicle resultVehicle;
            validateVehicleTypeAndGetIt(i_VehicleType);

            switch (i_VehicleType)
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

        private static void validateVehicleTypeAndGetIt(eVehicleType i_VehicleType)
        {
            if (!Enum.IsDefined(typeof(eVehicleType), i_VehicleType))
            {
                throw new ArgumentException("Invalid vehicle type!");
            }
        }
    }
}
