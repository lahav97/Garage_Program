using GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic.VehiclesInfo
{
    public abstract class VehicleInformation
    {
        string m_ModelName;
        protected string m_LicensePlateID;
        protected float m_EnergyPercentageLeft;
        private readonly float r_MaxEnergyPercentage = 100;
        private readonly float r_MinEnergyPercentage = 0;

        private float maxEnergyPercentage { get { return r_MaxEnergyPercentage; } }

        private float minEnergyPercentage { get { return r_MinEnergyPercentage; } }

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
                if (value >= minEnergyPercentage && value <= maxEnergyPercentage)
                {
                    m_EnergyPercentageLeft = value;
                }
                else
                {
                    throw new ValueOutOfRangeException("Energy Percentage", minEnergyPercentage, maxEnergyPercentage);
                }
            }
        }

        public abstract List<string> PromptsOfInformationNeeded();

        public abstract void VehicleInformationLeftToFill(List<string> i_ListOfInformationToFill);

        public List<string> AllInformationNeededForVehiclePrompts()
        {
            List<string> outputPromptsList = new List<string>
            {
                "model name",
                "energy percentage left in vehicle"
            };

            foreach(string prompt in PromptsOfInformationNeeded())
            {
                outputPromptsList.Add(prompt);
            }    

            return outputPromptsList;
        }

        public void FillVehicleInformation(List<string> i_ListOfInformationToFill, string i_LeicensePlateID)
        {
            LicensePlateID = i_LeicensePlateID;
            ModelName = i_ListOfInformationToFill[0];

            if (!float.TryParse(i_ListOfInformationToFill[1], out float inputEnergyPercentageLeft))
            {
                throw new FormatException("Energy percentage must be a number !");
            }

            EnergyPercentageLeft = inputEnergyPercentageLeft;
            

            i_ListOfInformationToFill.RemoveRange(0, 2);
            VehicleInformationLeftToFill(i_ListOfInformationToFill);
        } 

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Vehicle license plate ID: {LicensePlateID}")
              .AppendLine($"Vehicle model name: {ModelName}")
              .AppendLine($"Vehicle energy percentage left: {EnergyPercentageLeft:F2}%");

            return stringBuilder.ToString();
        }
    }
}
