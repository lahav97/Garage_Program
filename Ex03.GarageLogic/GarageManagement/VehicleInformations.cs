using GarageLogic.Vehicles.Types.Car;
using GarageLogic.Vehicles.VehicleFactory;
using System;
using System.Text;

namespace GarageLogic.GarageManagement
{
    internal class VehicleInformations
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;
        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set
            {
                if (!Enum.IsDefined(typeof(eVehicleStatus), value))
                {
                    throw new ArgumentException("Invalid vehicle status !");
                }

                m_VehicleStatus = value;
            }
        }

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

        private string getFormattedStatus()
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

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Owner's name: {OwnerName}")
                         .AppendLine($"Owner's phone number: {OwnerPhoneNumber}")
                         .AppendLine($"Vehicle status: {getFormattedStatus()}");

            return stringBuilder.ToString();
        }
    }
}
