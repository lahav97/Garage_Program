using GarageLogic.Vehicles.Types.Truck;
using GarageLogic.Vehicles.VehicleFactory;
using GarageLogic.VehiclesInfo;
using System;
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

        public int EngineVolume 
        {
            get { return m_EngineVolume; }
            set
            {
                if (value > 0)
                {
                    m_EngineVolume = value;
                }
            }
        }

        public eMotorcycleLicenseType MotorcycleLicense { get; private set; }

        public void SetMotorLicenseType(eMotorcycleLicenseType i_LicenseType)
        {
            validateAndSetMotorcycleLicenseType(i_LicenseType);
        }

        private void validateAndSetMotorcycleLicenseType(eMotorcycleLicenseType i_LicenseType)
        {
            if (!Enum.IsDefined(typeof(eMotorcycleLicenseType), i_LicenseType))
            {
                throw new ArgumentException("Invalid vehicle type!");
            }
            MotorcycleLicense =  i_LicenseType;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(base.ToString())
                .AppendLine($"Motorcycle's license type: {MotorcycleLicense}")
                .AppendLine($"Motorcycle's engine volume: {EngineVolume} cc");

            return stringBuilder.ToString();
        }
    }
}
