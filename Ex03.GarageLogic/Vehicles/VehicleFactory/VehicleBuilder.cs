using GarageLogic.Vehicles.Types.Car;
using GarageLogic.Vehicles.Types.Motorcycle;
using GarageLogic.Vehicles.Types.Truck;
using System;

namespace GarageLogic.Vehicles.VehicleFactory
{
    public class VehicleBuilder
    {
        public Vehicle BuildVehicle(string i_VehicleType, out eVehicleType o_CarType)
        {
            Vehicle resultVehicle;
            o_CarType = ValidateVehicleTypeAndGetIt(i_VehicleType);

            switch (o_CarType)
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

        private eVehicleType ValidateVehicleTypeAndGetIt(string i_VehicleType)
        {
            if (Enum.TryParse(i_VehicleType, out eVehicleType resultVehicleType))
            {
                return resultVehicleType;
            }
            else
            {
                throw new ArgumentException("Invalid car type !");
            }
        }
    }
}
