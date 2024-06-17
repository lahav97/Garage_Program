using System;
using System.Text;
using GarageLogic.Exceptions;
using GarageLogic.VehiclesInfo;
using static GarageLogic.Vehicles.Types.Car.CarInfo;
using static GarageLogic.Vehicles.Types.Motorcycle.MotorcycleInfo;

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
            private set
            {
                if (!Enum.IsDefined(typeof(eCarColors), value))
                {
                    throw new ArgumentException("Invalid car color !");
                }
                CarColor = value;
            }
        }

        public eNumberOfDoors NumberOfDoors 
        { 
            get { return m_NumberOfDoors;}
            private set
            {
                if (!Enum.IsDefined(typeof(eNumberOfDoors), value))
                {
                    throw new ArgumentException("Invalid number of doors !");
                }
                NumberOfDoors = value;
            }
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
