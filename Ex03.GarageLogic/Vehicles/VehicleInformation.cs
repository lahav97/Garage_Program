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

        public abstract List<string> PromptsOfInformationNeeded();

        public void FillVehicleInformation(List<string> i_ListOfInformationToFill, string leicensePlateID)
        {
            LicensePlateID = leicensePlateID;
            ModelName = i_ListOfInformationToFill[0];

            if (!float.TryParse(i_ListOfInformationToFill[1], out float inputEnergyPercentageLeft))
            {
                throw new ArgumentException("Input for Energy Percentage must be a number!");
            }

            EnergyPercentageLeft = inputEnergyPercentageLeft;
            

            i_ListOfInformationToFill.RemoveRange(0, 2);
            VehicleInformationLeftToFill(i_ListOfInformationToFill);
        }

        public abstract void VehicleInformationLeftToFill(List<string> values);

        private float MaxEnergyPercentage { get { return r_MaxEnergyPercentage; } }

        private float MinEnergyPercentage { get { return r_MinEnergyPercentage; } }

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

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Vehicle license Plate ID: {LicensePlateID}")
              .AppendLine($"Vehicle model name: {ModelName}")
              .AppendLine($"Vehicle energy percentage left: {EnergyPercentageLeft:F2}%");

            return stringBuilder.ToString();
        }
    }
}
