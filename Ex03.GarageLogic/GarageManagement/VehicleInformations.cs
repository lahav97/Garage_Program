using GarageLogic.Vehicles.VehicleFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic.GarageManagement
{
    internal class VehicleInformations
    {
        public string m_OwnerName;
        public string m_OwnerPhoneNumber;

        public eVehicleStatus VehicleStatus { get; set; }

        public string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }

        public string OwnerPhoneNumber
        {
            get { return m_OwnerPhoneNumber; }
            set
            {
                m_OwnerPhoneNumber = value;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Owner name: {OwnerName}")
                         .AppendLine($"Owner's phone number: {OwnerPhoneNumber}")
                         .AppendLine($"Vehicle status: {GetFormattedStatus()}");

            return stringBuilder.ToString();
        }

        private string GetFormattedStatus()
        {
            switch (VehicleStatus)
            {
                case eVehicleStatus.InRepair:
                    return "In repair";
                case eVehicleStatus.WasRepaired:
                    return "Fixed";
                case eVehicleStatus.WasPaidFor:
                    return "Paid";
                default:
                    throw new InvalidOperationException($"Unknown vehicle status: {VehicleStatus}");
            }
        }
    }
}
