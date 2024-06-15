using GarageLogic.Vehicles.VehicleFactory;
using System;

namespace GarageLogic.Vehicles.Types.Motorcycle
{
    public class MotorcycleInfo
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

        public void setMotorLicenseType(eMotorcycleLicenseType i_LicenseType)
        {
            ValidateAndSetMotorcycleLicenseType(i_LicenseType);
        }

        private void ValidateAndSetMotorcycleLicenseType(eMotorcycleLicenseType i_LicenseType)
        {
            if (!Enum.IsDefined(typeof(eMotorcycleLicenseType), i_LicenseType))
            {
                throw new ArgumentException("Invalid vehicle type!");
            }
            MotorcycleLicense =  i_LicenseType;
        }
    }
}
