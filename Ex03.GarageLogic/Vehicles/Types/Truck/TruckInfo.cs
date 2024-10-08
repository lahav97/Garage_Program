using System;
using System.Collections.Generic;
using System.Text;
using GarageLogic.VehiclesInfo;

namespace GarageLogic.Vehicles.Types.Truck
{
    public class TruckInfo : VehicleInformation
    {
        private bool m_TransportsHazardousMaterials;
        private float m_CargoVolume;

        public bool TransportsHazardousMaterials 
        {
            get { return m_TransportsHazardousMaterials; }
            set { m_TransportsHazardousMaterials = value; }
        }

        public float CargoVolume 
        {  get { return m_CargoVolume; }
           set
           {
                if(value > 0f)
                {
                    m_CargoVolume = value;
                }
                else
                {
                    throw new ArgumentException("Cargo volume must be higher than 0 !");
                }
            }
        }

        public override List<string> PromptsOfInformationNeeded()
        {
            return new List<string>
            {
                "Does the truck transport hazardous materials? (Yes/No)",
                "truck's cargo volume in cubic meters"
            };
        }

        public override void VehicleInformationLeftToFill(List<string> i_ListOfInformationToFill)
        {
            if (i_ListOfInformationToFill[0] == "Yes")
            {
                TransportsHazardousMaterials = true;
            }
            else if (i_ListOfInformationToFill[0] == "No")
            {
                TransportsHazardousMaterials = false;
            }
            else
            {
                throw new FormatException("Input for hazrdus materials was wrong !");
            }

            if (!float.TryParse(i_ListOfInformationToFill[1],out m_CargoVolume) || m_CargoVolume < 0)
            {
                throw new FormatException("Input for cargo volume was wrong !");
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(base.ToString())
                .AppendLine($"does truck transports hazardous materials? {(TransportsHazardousMaterials ? "Yes" : "No")} ")
                .AppendLine($"Truck's cargo volume: {CargoVolume} cubic meters");

            return stringBuilder.ToString();
        }
    }
}
