using static GarageLogic.Vehicles.Types.Motorcycle.MotorcycleInfo;
using System;

namespace GarageLogic.Vehicles.Types.Truck
{
    public class TruckInfo
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
    }
}
