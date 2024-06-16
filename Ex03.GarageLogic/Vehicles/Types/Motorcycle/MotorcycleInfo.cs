﻿using GarageLogic.Vehicles.Types.Truck;
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
        eMotorcycleLicenseType m_eMotorLicenseType;

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

        public eMotorcycleLicenseType MotorcycleLicense
        {
            get { return m_eMotorLicenseType; }
            private set
            {
                if (!Enum.IsDefined(typeof(eMotorcycleLicenseType), value))
                {
                    throw new ArgumentException("Invalid vehicle type !");
                }
                MotorcycleLicense = value;
            }
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
