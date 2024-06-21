using System.Collections.Generic;
using System.Text;

namespace GarageLogic.Vehicles
{
    public class Wheel
    {
        private string m_ManufactureName;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public Wheel(float i_MaxAirPressure)
        {
            MaxAirPressure = i_MaxAirPressure;
        }

        private float MaxAirPressure 
        { 
            get {  return m_MaxAirPressure; } 
            set { m_MaxAirPressure = value; }
        }

        public string ManufactureName
        {
            get { return m_ManufactureName; }
            set { m_ManufactureName = value; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set
            {
                if (value >= 0f && value <= MaxAirPressure)
                {
                    m_CurrentAirPressure = value;
                }
                else
                {
                    throw new GarageLogic.Exceptions.ValueOutOfRangeException(value.ToString(), MaxAirPressure, 0f);
                }
            }
        }

        internal static List<string> ListOfInformationNeededForWheels()
        {
            return new List<string>
            {
                "manufacture name",
                "current air pressure"
            };
        }

        public void InflateWheels(float i_AirPressureToAdd)
        {
            if (isMoreThanMaxAirPressure(i_AirPressureToAdd))
            {
                throw new GarageLogic.Exceptions.ValueOutOfRangeException(i_AirPressureToAdd.ToString(), MaxAirPressure, 0f);
            }
            else
            {
                CurrentAirPressure += i_AirPressureToAdd;
            }
        }

        private bool isMoreThanMaxAirPressure(float i_AirToAdd)
        {
            return i_AirToAdd + m_CurrentAirPressure <= MaxAirPressure;
        }

        public void InflateToMaximum()
        {
            m_CurrentAirPressure = MaxAirPressure;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Wheel manufacture Name: {ManufactureName}")
              .AppendLine($"Wheel current air pressure: {CurrentAirPressure} PSI")
              .AppendLine($"Wheel maximum air pressure: {MaxAirPressure} PSI");

            return stringBuilder.ToString();
        }
    }
}