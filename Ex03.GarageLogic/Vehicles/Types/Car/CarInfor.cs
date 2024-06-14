using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageLogic.Exceptions;


namespace GarageLogic.Vehicles.Types.Car
{
    public class CarInfor
    {
        public enum eCarColors
        {
            Yellow,
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
        public void setCarColor(string i_CarColor)
        {
            CarColor = ValidateCarColorAndGetIt(i_CarColor);
        }

        public void setNumberOfDoors(int i_NumberOfDoors)
        {
            NumberOfDoors = ValidateNumberOfDoorsAndGetIt(i_NumberOfDoors);
        }

        private eNumberOfDoors ValidateNumberOfDoorsAndGetIt(int i_NumberOfDoors)
        {
            if (Enum.IsDefined(typeof(eNumberOfDoors), i_NumberOfDoors))
            {
                return (eNumberOfDoors)i_NumberOfDoors;
            }
            else
            {
                throw new ArgumentException("Invalid number of doors !");
            }
        }

        private eCarColors ValidateCarColorAndGetIt(string i_CarColorToCheck)
        {
            if (Enum.TryParse(i_CarColorToCheck, out eCarColors resultColor))
            {
                return resultColor;
            }
            else
            {
                throw new ArgumentException("Invalid car color !");
            }
        }
    }
}
