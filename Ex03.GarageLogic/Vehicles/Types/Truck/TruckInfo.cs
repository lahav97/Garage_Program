using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Vehicles.Types.Truck
{
    public class TruckInfo
    {
        public bool m_TransportsHazardousMaterials;
        public float m_CargoVolume;

        public bool TransportsHazardousMaterials { get; set; }

        public float CargoVolume {  get; set; }
    }
}
