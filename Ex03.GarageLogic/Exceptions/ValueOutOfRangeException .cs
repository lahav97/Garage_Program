using System;

namespace GarageLogic.Exceptions
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public float MaxValue 
        { 
            get { return m_MaxValue; }
            set { m_MaxValue = value; }
        }

        public float MinValue 
        {
            get { return m_MinValue; } 
            set { m_MinValue = value; } 
        }

        public ValueOutOfRangeException(string i_ParamName, float i_MaxValue, float i_MinValue)
            : base($"{i_ParamName} is out of range ! Value must be between {i_MinValue} to {i_MaxValue}")
        {
            MaxValue = i_MaxValue;
            MinValue = i_MinValue;
        }
    }
}
