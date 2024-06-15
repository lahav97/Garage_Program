using System;
using System.Text;
using GarageLogic.Exceptions;
using static GarageLogic.Vehicles.Types.Motorcycle.MotorcycleInfo;

namespace GarageLogic.Vehicles.Types.Car
{
    public class CarInfor
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
        public void setCarColor(eCarColors i_CarColor)
        {
            ValidateAndSetCarColor(i_CarColor);
        }

        public void setNumberOfDoors(eNumberOfDoors i_NumberOfDoors)
        {
            ValidateAndSetNumberOfDoors(i_NumberOfDoors);
        }

        private void ValidateAndSetNumberOfDoors(eNumberOfDoors i_NumberOfDoors)
        {
            if (!Enum.IsDefined(typeof(eNumberOfDoors), i_NumberOfDoors))
            {
                throw new ArgumentException("Invalid number of doors !");
            }
            NumberOfDoors = i_NumberOfDoors;
        }

        private void ValidateAndSetCarColor(eCarColors i_CarColorToCheck)
        {
            if (!Enum.IsDefined(typeof(eCarColors), i_CarColorToCheck))
            {
                throw new ArgumentException("Invalid car color !");
            }
            CarColor = i_CarColorToCheck;
        }
    }
}
