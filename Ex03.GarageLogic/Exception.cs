using System;

namespace ValueOutOfRangeException
{
    internal class ValueOutOfRangeException : Exception
    {
        float m_MaxValue;
        float m_MinValue;

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue)
            : base($"Invalid value ! Value must be between {i_MaxValue} to {i_MinValue}")
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }
    }
}
