using System;
using System.Text;
using GarageLogic.VehiclesInfo;

namespace GarageLogic.Vehicles.Types.Car
{
    public class CarInfo : VehicleInformation
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

        eCarColors m_eCarColor;
        eNumberOfDoors m_NumberOfDoors;

        public eCarColors CarColor 
        { 
            get { return m_eCarColor;}
            set
            {
                if (!Enum.IsDefined(typeof(eCarColors), value))
                {
                    throw new ArgumentException("Invalid car color !");
                }
                m_eCarColor = value;
            }
        }

        public eNumberOfDoors NumberOfDoors 
        { 
            get { return m_NumberOfDoors;}
            set
            {
                if (!Enum.IsDefined(typeof(eNumberOfDoors), value))
                {
                    throw new ArgumentException("Invalid number of doors !");
                }
                m_NumberOfDoors = value;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(base.ToString())
                .AppendLine($"Car's color: {CarColor}")
                .AppendLine($"Number of doors in car: {(int)NumberOfDoors}");

            return stringBuilder.ToString();
        }
    }
}
