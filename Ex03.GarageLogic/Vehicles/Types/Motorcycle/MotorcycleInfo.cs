using GarageLogic.VehiclesInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic.Vehicles.Types.Motorcycle
{
    public class MotorcycleInfo : VehicleInformation
    {
        public enum eMotorcycleLicenseType
        {
            A = 1,
            A1,
            AA,
            B1
        }

        int m_EngineVolume;
        eMotorcycleLicenseType m_eMotorcycleLicenseType;

        public int EngineVolume 
        {
            get { return m_EngineVolume; }
            set
            {
                if (value > 0)
                {
                    m_EngineVolume = value;
                }
                else
                {
                    throw new ArgumentException("Engine Volume must be higher than 0!");
                }
            }
        }

        public eMotorcycleLicenseType MotorcycleLicenseType
        {
            get { return m_eMotorcycleLicenseType; }
            set
            {
                m_eMotorcycleLicenseType = value;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(base.ToString())
                .AppendLine($"Motorcycle's license type: {MotorcycleLicenseType}")
                .AppendLine($"Motorcycle's engine volume: {EngineVolume} cc");

            return stringBuilder.ToString();
        }

        public override List<string> PromptsOfInformationNeeded()
        {
            return new List<string>
            {
                "motorcycle's engine volume in cc",
                "motorcycle's license type (A, A1, AA, B1)"
            };

        }

        public override void VehicleInformationLeftToFill(List<string> i_ListOfInformationToFill)
        {
            if (!int.TryParse(i_ListOfInformationToFill[0], out m_EngineVolume) || m_EngineVolume < 0)
            {
                throw new ArgumentException("Input for engine volume was wrong!");
            }

            if (!Enum.TryParse(i_ListOfInformationToFill[1], true, out m_eMotorcycleLicenseType))
            {
                throw new ArgumentException("Invalid license type!");
            }
        }
    }
}
