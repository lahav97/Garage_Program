using System;
using System.Text;
using GarageLogic.Exceptions;
using GarageLogic.VehiclesInfo;
using static GarageLogic.Vehicles.Types.Motorcycle.MotorcycleInfo;

namespace GarageLogic.Vehicles.Types.Car
{
    public class CarInfor : VehicleInformation
    {
        public enum eCarColors
        {
            Yellow = 1,
            White,
            Red,
            Black
        }
        public enum eNumberOfDoors : uint
        {
            TwoDoor = 2,
            ThreeDoor = 3,
            FourDoor = 4,
            FiveDoor = 5
        }

        public eCarColors CarColor { get; private set; }
        public eNumberOfDoors NumberOfDoors { get; private set; }
        public void SetCarColor(eCarColors i_CarColor)
        {
            validateAndSetCarColor(i_CarColor);
        }

        public void SetNumberOfDoors(eNumberOfDoors i_NumberOfDoors)
        {
            validateAndSetNumberOfDoors(i_NumberOfDoors);
        }

        private void validateAndSetNumberOfDoors(eNumberOfDoors i_NumberOfDoors)
        {
            if (!Enum.IsDefined(typeof(eNumberOfDoors), i_NumberOfDoors))
            {
                throw new ArgumentException("Invalid number of doors !");
            }
            NumberOfDoors = i_NumberOfDoors;
        }

        private void validateAndSetCarColor(eCarColors i_CarColorToCheck)
        {
            if (!Enum.IsDefined(typeof(eCarColors), i_CarColorToCheck))
            {
                throw new ArgumentException("Invalid car color !");
            }
            CarColor = i_CarColorToCheck;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(base.ToString())
                .AppendLine($"Car's color: {CarColor}")
                .AppendLine($"Number of doors in car: {NumberOfDoors}");

            return stringBuilder.ToString();
        }
    }
}
