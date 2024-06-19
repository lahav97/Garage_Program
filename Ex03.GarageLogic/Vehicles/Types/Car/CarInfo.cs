using System;
using System.Collections.Generic;
using System.Text;
using GarageLogic.VehiclesInfo;
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

        public override List<string> PromptsOfInformationNeeded()
        {
            return new List<string>
            {
                "car color (Yellow, White, Red, Black)",
                "number of doors (2, 3, 4, 5)"
            };
        }

        public override void VehicleInformationLeftToFill(List<string> i_ListOfInformationToFill)
        {
            if (!Enum.TryParse(i_ListOfInformationToFill[0], true, out m_eCarColor))
            {
                throw new ArgumentException("Invalid car color!");
            }

            if (!Enum.TryParse(i_ListOfInformationToFill[1], true, out m_NumberOfDoors))
            {
                throw new ArgumentException("Invalid number of doors!");
            }
        }
    }
}
