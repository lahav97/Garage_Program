using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.Vehicles.Types.Car;
using GarageLogic.VehiclesInfo;

namespace GarageLogic.Vehicles.Types.Car
{
    public class CarInfo : VehicleInformation
    {
        private eCarColors m_CarColor;
        private eNumberOfDoors m_NumberOfDoors;

        internal eCarColors CarColor 
        { 
            get { return m_CarColor;}
            set
            {
                if (!Enum.IsDefined(typeof(eCarColors), value))
                {
                    throw new ArgumentException("Invalid car color !");
                }

                m_CarColor = value;
            }
        }

        internal eNumberOfDoors NumberOfDoors 
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
            if (!Enum.TryParse(i_ListOfInformationToFill[0], true, out m_CarColor))
            {
                throw new FormatException("Invalid car color !");
            }

            if (!Enum.TryParse(i_ListOfInformationToFill[1], true, out m_NumberOfDoors))
            {
                throw new FormatException("Invalid number of doors !");
            }
        }
    }
}
