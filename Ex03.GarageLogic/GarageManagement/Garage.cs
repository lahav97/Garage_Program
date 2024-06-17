using System.Collections.Generic;
using GarageLogic.Vehicles;
using GarageLogic.Vehicles.Types;
using GarageLogic.Vehicles.VehicleFactory;
using System;
using System.Linq;
using System.Text;

namespace VehicleGarage
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleInformations> r_VehicleInformation = new Dictionary<string, VehicleInformations>();
        private readonly Dictionary<string, Vehicle> r_Vehicles = new Dictionary<string, Vehicle>();

        public class VehicleInformations
        {
            public string m_OwnerName;
            public string m_OwnerPhoneNumber;
            public eVehicleStatus eVehicleStatus;

            public eVehicleStatus VehicleStatus {  get;  set; }
           
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

        public void EnterNewVehicleToGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            VehicleInformations vehicleInformations = new VehicleInformations();
            vehicleInformations.OwnerPhoneNumber = i_OwnerPhoneNumber;
            vehicleInformations.OwnerName = i_OwnerName;
            vehicleInformations.VehicleStatus = eVehicleStatus.InRepair;

            r_Vehicles.Add(i_Vehicle.VehicleInfo.LicensePlateID, i_Vehicle);
            r_VehicleInformation.Add(i_Vehicle.VehicleInfo.LicensePlateID, vehicleInformations);
        }

        public List<string> GetVehiclesLicensePlateListByStatus(eVehicleStatus? i_VehicleStatus = null)
        {
            ValidateVehicleStatus(i_VehicleStatus);

            List<string> licensePlatesList;

            if (i_VehicleStatus == null) // To return all of the license plates.
            {
                licensePlatesList = r_VehicleInformation.Keys.ToList();
            }
            else
            {
                licensePlatesList = r_VehicleInformation
                    .Where(vehicleInfo => vehicleInfo.Value.VehicleStatus == i_VehicleStatus.Value)
                    .Select(vehicleInfo => vehicleInfo.Key)
                    .ToList();
            }

            return licensePlatesList;
        }

        private void ValidateVehicleStatus(eVehicleStatus? vehicleStatus)
        {
            if (vehicleStatus.HasValue && !Enum.IsDefined(typeof(eVehicleStatus), vehicleStatus.Value))
            {
                throw new ArgumentException($"Invalid vehicle status: {vehicleStatus}");
            }
        }

        public List<string> GetAllLicensePlatesInGarage() 
        {
            List<string> VehicleList = new List<string>();

            foreach (string licensePlate in r_VehicleInformation.Keys)
            {
                VehicleList.Add(licensePlate);
            }

            return VehicleList;
        }

        public void ChangeVehicleStatus(string i_LicensePlateID, eVehicleStatus i_VehicleStatusToChange)
        {
            if (r_VehicleInformation.TryGetValue(i_LicensePlateID, out VehicleInformations vehicleInfo))
            {
                vehicleInfo.VehicleStatus = i_VehicleStatusToChange;
            }
            else
            {
                throw new ArgumentException($"Vehicle with license plate ID '{i_LicensePlateID}' not found.");
            }
        }

        public void InflateWheelsToMaximum(string i_LicensePlateID)
        {
            Vehicle currentVehicle = getVehicleFromSystem(i_LicensePlateID);
            currentVehicle.InflateAllWheelsToMaximum();
        }

        public void RefuelVehicle(string i_LicensePlateID, FuelVehicle.eFuelTypes i_FuelType, float i_AmountToRefuel)
        {
            Vehicle currentVehicle = getVehicleFromSystem(i_LicensePlateID);

            if (currentVehicle is FuelVehicle currentFuelVehicle)
            {
                currentFuelVehicle.Refuel(i_AmountToRefuel, i_FuelType);
            }
            else
            {
                throw new ArgumentException("Expected a fuel Vehicle but received an electric vehicle.");
            }
        }

        public void ChargeVehicle(string i_LicensePlateID, int i_MinutesToCharge)
        {
            Vehicle currentVehicle = getVehicleFromSystem(i_LicensePlateID);

            if (currentVehicle is ElectricVehicle currentElectricVehicle)
            {
                currentElectricVehicle.ChargeBattery(i_MinutesToCharge);
            }
            else
            {
                throw new ArgumentException("Expected an electric vehicle but received fuel vehicle.");
            }
        }

        public string GetVehicleInformation(string licensePlateID)
        {
            if (!IsVehicleInSystem(licensePlateID))
            {
                throw new ArgumentException($"Vehicle with license plate ID '{licensePlateID}' does not exist in the system.");
            }

            Vehicle vehicle = r_Vehicles[licensePlateID];
            VehicleInformations vehicleInfo = r_VehicleInformation[licensePlateID];
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Owner Information:");
            stringBuilder.AppendLine(vehicleInfo.ToString());
            stringBuilder.AppendLine("Vehicle Information:");
            stringBuilder.AppendLine(vehicle.ToString());
            stringBuilder.AppendLine();

            return stringBuilder.ToString();
        }

        private Vehicle getVehicleFromSystem(string i_LicensePlateID)
        {
            if (!IsVehicleInSystem(i_LicensePlateID))
            {
                throw new ArgumentException($"Vehicle with license plate ID '{i_LicensePlateID}' does not exist in the system.");
            }

            Vehicle resultVehicle = null;

            if(!r_Vehicles.TryGetValue(i_LicensePlateID, out resultVehicle))
            {
                throw new ArgumentException($"Vehicle with license plate ID '{i_LicensePlateID}' not found.");
            }

            return resultVehicle;
        }

        public bool IsVehicleInSystem(string i_LicensePlateID)
        {
            return r_VehicleInformation.ContainsKey(i_LicensePlateID);
        }

        public float GetEnergyLeftToBeFilled(string i_LicensePlateID)
        {
            Vehicle currentVehicle = getVehicleFromSystem(i_LicensePlateID);
            float remainingCapacity;

            if (currentVehicle is FuelVehicle currentFuelVehicle)
            {
                remainingCapacity = currentFuelVehicle.GetRemainingTankCapacityToRefuel();
            }
            else
            {
                remainingCapacity = ((ElectricVehicle)currentVehicle).GetRemainingBatteryCapacityToCharge();
            }

            return remainingCapacity;
        }
    }
}
