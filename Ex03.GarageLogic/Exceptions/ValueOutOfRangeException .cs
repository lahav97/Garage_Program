using System;

namespace GarageLogic.Exceptions
{
    public class ValueOutOfRangeException : Exception
    {
        public float m_MaxValue { get; set; }
        public float m_MinValue { get; set; }

        public ValueOutOfRangeException(string i_ParamName, float i_MaxValue, float i_MinValue)
            : base($"{i_ParamName} is out of range ! Value must be between {i_MaxValue} to {i_MinValue}")
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }
    }
}
