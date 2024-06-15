﻿using GarageLogic.Exceptions;

namespace GarageLogic.VehiclesInfo
{
    public abstract class VehicleInformation
    {
        string m_ModelName;
        protected string m_LicensePlateID;
        protected float m_EnergyPercentageLeft;
        public float m_MaxEnergyPercentage = 100;
        
        private float MaxEnergyPercentage {  get; }

        private float MinEnergyPercentage { get; }

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public string LicensePlateID
        {
            get { return m_LicensePlateID; }
            set { m_LicensePlateID = value; }
        }

        public float EnergyPercentageLeft
        {
            get { return m_EnergyPercentageLeft; }
            set
            {
                if(value >= MinEnergyPercentage && value <= MaxEnergyPercentage)
                {
                    m_EnergyPercentageLeft = value;
                }
                else
                {
                    throw new ValueOutOfRangeException("Energy Percentage", MinEnergyPercentage, MaxEnergyPercentage);
                }
            }
        }
    }
}
