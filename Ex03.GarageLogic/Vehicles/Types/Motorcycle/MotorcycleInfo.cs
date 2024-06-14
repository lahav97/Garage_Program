using System;

namespace GarageLogic.Vehicles.Types.Motorcycle
{
    public class MotorcycleInfo
    {
        public enum eMotorcycleLicenseType
        {
            A,
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

        public void setMotorLicenseType(string i_LicenseType)
        {
            MotorcycleLicense = ValidateMotorcycleLicenseTypeAndGetIt(i_LicenseType);
        }

        private eMotorcycleLicenseType ValidateMotorcycleLicenseTypeAndGetIt(string i_LicenseType)
        {
            if (Enum.TryParse(i_LicenseType, out eMotorcycleLicenseType LicenseType))
            {
                return LicenseType;
            }
            else
            {
                throw new ArgumentException("Invalid motorcycle license choice !");
            }
        }
    }
}
