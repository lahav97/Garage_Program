using Garage;
using System;
using System.Collections.Generic;


namespace Vehicles
{

    public enum eGasTypes
    {
        Soler,
        Octan95,
        Octan96,
        Octan98,
        None
    }

    public enum eCarColors
    {
        Yellow,
        White,
        Red,
        Black
    }

    public enum eLicenseType
    {
        A,
        A1,
        AA,
        B1
    }

    internal abstract class Vehicle
    {
        protected string m_ModelName { get; set; }
        protected string m_LicensePlateID { get; set; }
        protected float m_EnergyPercentageLeft { get; }
        protected Wheel[] m_Wheels { get; }
        protected EnergyStorage m_VehicleTank { get; }

        protected Vehicle(string i_ModelName, string i_LicensePlateID, float i_EnergyPercentageLeft, Wheel[] i_Wheels, EnergyStorage i_VehicleTank)
        {
            m_ModelName = i_ModelName;
            m_LicensePlateID = i_LicensePlateID;
            m_EnergyPercentageLeft = i_EnergyPercentageLeft;
            m_Wheels = i_Wheels;
            m_VehicleTank = i_VehicleTank;
        }
    }

    /*internal abstract class VehicleBuilder
    {
        protected Vehicle m_Vehicle;

        public void SetModelName(string i_ModelName)
        {
            m_Vehicle.m_ModelName = i_ModelName;
        }

        public void SetLicensePlateID(string i_LicensePlateID)
        {
            m_Vehicle.m_LicensePlateID = i_LicensePlateID;
        }

        public void SetEnergyPercentageLeft(float i_EnergyPercentageLeft)
        {
            m_Vehicle.m_EnergyPercentageLeft = i_EnergyPercentageLeft;
        }

        public void SetWheels(Wheel[] i_Wheels)
        {
            m_Vehicle.m_Wheels = i_Wheels;
        }

        public void SetVehicleTank(EnergyStorage i_VehicleTank)
        {
            m_Vehicle.m_VehicleTank = i_VehicleTank;
        }

        public abstract Vehicle Build();
    }*/



    internal abstract class EnergyStorage
    {
        protected float m_MaxEnergyCapacity { get; set; }
        protected float m_MinEnergyCapacity { get; set; } = 0;
        protected float m_CurrentEnergyStorage { get; set; }

        public EnergyStorage(float i_MaxEnergyCapacity, float i_CurrentEnergyStorage)
        {
            m_MaxEnergyCapacity = i_MaxEnergyCapacity;
            m_CurrentEnergyStorage = i_CurrentEnergyStorage;
        }
        protected abstract void Refuel(float i_EnergyPercentageToAdd, eGasTypes i_GasType);
        protected abstract void Refuel(float i_EnergyPercentageToAdd);

        public bool IsOutOfRange(float i_EnergyPercentageToAdd)
        {
            return i_EnergyPercentageToAdd + m_CurrentEnergyStorage <= m_MaxEnergyCapacity;
        }
    }

    internal class Battery : EnergyStorage
    {
        public Battery(float i_MaxEnergyCapacity, float i_CurrentEnergyStorage)
           : base(i_MaxEnergyCapacity, i_CurrentEnergyStorage) { }

        protected override void Refuel(float i_EnergyPercentageToAdd, eGasTypes i_GasType)
        {
            throw new NotImplementedException();
        }

        protected override void Refuel(float i_EnergyPercentageToAdd)
        {
            if (IsOutOfRange(i_EnergyPercentageToAdd))
            {
                throw new ValueOutOfRangeException.ValueOutOfRangeException(m_MaxEnergyCapacity, m_MinEnergyCapacity);
            }
            else
            {
                m_CurrentEnergyStorage += i_EnergyPercentageToAdd;
            }
        }


    }


    internal class FuelTank : EnergyStorage
    {
        eGasTypes m_GasType;
        public FuelTank(eGasTypes i_GasType, float i_MaxEnergyCapacity, float i_CurrentEnergyStorage)
            : base(i_MaxEnergyCapacity, i_CurrentEnergyStorage)
        {
            m_GasType = i_GasType;
        }

        protected override void Refuel(float i_EnergyPercentageToAdd, eGasTypes i_GasType)
        {
            throw new NotImplementedException();
        }

        protected override void Refuel(float i_EnergyPercentageToAdd)
        {
            if (IsOutOfRange(i_EnergyPercentageToAdd))
            {
                throw new ValueOutOfRangeException.ValueOutOfRangeException(m_MaxEnergyCapacity, m_MinEnergyCapacity);
            }
            else
            {
                m_CurrentEnergyStorage += i_EnergyPercentageToAdd;
            }
        }
    }

    internal class Wheel
    {
        public float m_MaxAirPressure { get; }
        public string m_ManufactureName { get; set; }
        public float m_MinAirPressure { get; } = 0;
        public float m_AirPressure
        {
            get { return m_AirPressure; }
            set
            {
                if (value >= 0 && value < m_MaxAirPressure)
                {
                    m_AirPressure = value;
                }
                else
                {
                    throw new ValueOutOfRangeException.ValueOutOfRangeException(m_MaxAirPressure, m_MinAirPressure);
                }
            }
        }


        public Wheel(float i_AirPressure, float i_MaxAirPressure, string i_ManufactureName)
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_AirPressure = i_AirPressure;
            m_ManufactureName = i_ManufactureName;
            m_MinAirPressure = 0;
        }

        private void InflateWheel(float i_AirToAdd)
        {
            if (IsMoreThanMaxAirPressure(i_AirToAdd))
            {
                throw new ValueOutOfRangeException.ValueOutOfRangeException(m_MaxAirPressure, m_MinAirPressure);
            }
            else
            {
                m_AirPressure += i_AirToAdd;
            }
        }

        private bool IsMoreThanMaxAirPressure(float i_AirToAdd)
        {
            return i_AirToAdd + m_AirPressure <= m_MaxAirPressure;
        }
    }


    internal class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;
        int m_MarAirPressure = 33;
        int m_MaxWheels = 2;

        public Motorcycle(string i_ModelName, string i_LicensePlateID, float i_EnergyPercentageLeft, Wheel[] i_Wheels, eLicenseType i_LicenseType, int i_EngineVolume, EnergyStorage i_VehicleTank)
            : base(i_ModelName, i_LicensePlateID, i_EnergyPercentageLeft, i_Wheels, i_VehicleTank)
        {
            m_LicenseType = i_LicenseType;
            m_EngineVolume = i_EngineVolume;
        }

        protected bool IsAirPressureValid(Wheel[] i_Wheels)
        {
            bool airPressureValid = true;
            foreach (Wheel wheel in i_Wheels)
            {
                if (wheel.m_AirPressure > m_MarAirPressure)
                {
                    airPressureValid = false;
                    break;
                }
            }
            return airPressureValid;
        }

        protected bool IsAmountOfWheelsValid(Wheel[] i_Wheels)
        {
            return i_Wheels.Length <= m_MaxWheels;
        }
    }

    /*internal class MotorcycleBuilder : VehicleBuilder
    {
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public void SetLicenseType(eLicenseType i_LicenseType)
        {
            m_LicenseType = i_LicenseType;
        }

        public void SetEngineVolume(int i_EngineVolume)
        {
            m_EngineVolume = i_EngineVolume;
        }

        public override Vehicle Build()
        {
            return new Motorcycle(m_ModelName, m_LicensePlateID, m_EnergyPercentageLeft, m_Wheels, m_LicenseType, m_EngineVolume, m_VehicleTank);
        }
    }*/


    internal class Car : Vehicle
    {
        private readonly eCarColors m_CarColor;
        private readonly int m_NumberOfDoors;
        private readonly float m_MaxAirPressure = 31;
        private readonly int m_MaxWheels = 5;

        public Car(string i_ModelName, string i_LicensePlateID, float i_EnergyPercentageLeft, Wheel[] i_Wheels, eCarColors i_eCarColor, int i_NumberOfDoors, bool i_IsElectric, eGasTypes i_eGasType, float i_MaxEnergyCapacity, float i_CurrentEnergyStorage)
            : base(i_ModelName, i_LicensePlateID, i_EnergyPercentageLeft, i_Wheels, i_IsElectric, i_eGasType, i_MaxEnergyCapacity, i_CurrentEnergyStorage)
        {
            m_CarColor = i_eCarColor;
            m_NumberOfDoors = i_NumberOfDoors;
        }
        protected bool IsAirPressureValid(Wheel[] i_Wheels)
        {
            bool airPressureValid = true;
            foreach (Wheel wheel in i_Wheels)
            {
                if (wheel.m_MaxAirPressure > m_MaxAirPressure)
                {
                    airPressureValid = false;
                    break;
                }
            }
            return airPressureValid;
        }

        protected bool IsAmountOfWheelsValid(Wheel[] i_Wheels)
        {
            return i_Wheels.Length <= m_MaxWheels;
        }
    }

    internal class Truck : Vehicle
    {
        private readonly bool m_TransportsHazardousMaterials;
        private readonly float m_CargoVolume;
        private readonly float m_MaxAirPressure = 28;
        private readonly int m_MaxWheels = 12;

        public Truck(string i_ModelName, string i_LicensePlateID, float i_EnergyPercentageLeft, Wheel[] i_Wheels,
            bool i_TransportsHazardousMaterials, float i_CargoVolume, bool i_IsElectric, eGasTypes i_eGasType, float i_MaxEnergyCapacity, float i_CurrentEnergyStorage)
         : base(i_ModelName, i_LicensePlateID, i_EnergyPercentageLeft, i_Wheels, i_IsElectric, i_eGasType, i_MaxEnergyCapacity, i_CurrentEnergyStorage)
        {
            m_TransportsHazardousMaterials = i_TransportsHazardousMaterials;
            m_CargoVolume = i_CargoVolume;
        }

        protected bool IsAirPressureValid(Wheel[] i_Wheels)
        {
            bool airPressureValid = true;
            foreach (Wheel wheel in i_Wheels)
            {
                if (wheel.m_MaxAirPressure > m_MaxAirPressure)
                {
                    airPressureValid = false;
                    break;
                }
            }
            return airPressureValid;
        }

        protected bool IsAmountOfWheelsValid(Wheel[] i_Wheels)
        {
            return i_Wheels.Length <= m_MaxWheels;
        }
    }
}
