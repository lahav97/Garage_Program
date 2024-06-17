using System.Text;
using GarageLogic.VehiclesInfo;

namespace GarageLogic.Vehicles.Types.Truck
{
    public class TruckInfo : VehicleInformation
    {
        public bool m_TransportsHazardousMaterials;
        public float m_CargoVolume;

        public bool TransportsHazardousMaterials { get; set; }

        public float CargoVolume 
        {  get { return m_CargoVolume; }
           set
           {
                if(value > 0)
                {
                    m_CargoVolume = value;
                }
           }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(base.ToString())
                .AppendLine($"Does truck transports hazardous materials? {(TransportsHazardousMaterials ? "Yes" : "No")} ")
                .AppendLine($"Truck's cargo volume: {CargoVolume} cubic meters");

            return stringBuilder.ToString();
        }
    }
}
