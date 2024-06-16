using System.Collections.Generic;
using GarageLogic.Vehicles;
using GarageLogic.Vehicles.Types;
using GarageLogic.Exceptions;
using GarageLogic.Vehicles.VehicleFactory;
using System;
using System.Linq;


namespace VehicleGarage
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleInformations> r_VehicleInformation = new Dictionary<string, VehicleInformations>();
        private readonly List<Vehicle> r_Vehicles = new List<Vehicle>();

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
        }

        public void ReEnterVehicleToGarage(string i_LicensePlateID)
        {
            ChangeVehicleStatus(i_LicensePlateID, eVehicleStatus.InRepair);
        }

        public void EnterNewVehicleToGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber) //GOOD
        {
            VehicleInformations vehicleInformations = new VehicleInformations();
            vehicleInformations.OwnerPhoneNumber = i_OwnerName;
            vehicleInformations.OwnerName = i_OwnerName;
            vehicleInformations.VehicleStatus = eVehicleStatus.InRepair;

            r_Vehicles.Add(i_Vehicle);
            r_VehicleInformation.Add(i_Vehicle.VehicleInfo.LicensePlateID, vehicleInformations);
        }

        public List<string> GetVehiclesLicensePlateListByStatus(eVehicleStatus? i_VehicleStatus = null)
        {
            List<string> licensePlatesList;

            if (i_VehicleStatus.HasValue)
            {
                licensePlatesList = r_VehicleInformation
                    .Where(vehicleInfo => vehicleInfo.Value.eVehicleStatus == i_VehicleStatus.Value)
                    .Select(vehicleInfo => vehicleInfo.Key)
                    .ToList();
            }
            else //To return all license plate in the garage
            {
                licensePlatesList = r_VehicleInformation.Keys.ToList();
            }

            return licensePlatesList;
        }

        public eVehicleStatus ValidateAndParseVehicleStatus(string i_VehicleStatus) //GOOD
        {
            if (Enum.TryParse(i_VehicleStatus, out eVehicleStatus VehicleStatus))
            {
                return VehicleStatus;
            }
            else
            {
                throw new ArgumentException("Invalid vehicle status!");
            }
        }

        public List<string> GetListOfAllVehiclesInTheGarage() //GOOD
        {
            List<string> VhiecleList = new List<string>();

            foreach (string licensePlate in r_VehicleInformation.Keys)
            {
                VhiecleList.Add(licensePlate);
            }

            return VhiecleList;
        }

        public void ChangeVehicleStatus(string i_LicensePlateID, eVehicleStatus i_VehicleStatusToChange) //GOOD
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

        public void InflateWheelsToMaximum(string i_LicensePlateID) //GOOD
        {
            Vehicle currentVehicle = getVehicleFromSystem(i_LicensePlateID);
            currentVehicle.InflateAllWheelsToMaximum();
        }

        public void RefuelVehicle(string i_LicensePlateID, FuelVehicle.eFuelTypes i_FuelType, float i_AmountToRefuel) //GOOD
        {
            Vehicle currentVehicle = getVehicleFromSystem(i_LicensePlateID);

            if (currentVehicle is FuelVehicle fuelVehicle)
            {
                fuelVehicle.Refuel(i_AmountToRefuel, i_FuelType);
            }
            else
            {
                throw new ArgumentException("Expected a FuelVehicle but received an ElectricVehicle.");
            }
        }

        public void ChargeVehicle(string i_LicensePlateID, int i_MinutesToCharge) //GOOD
        {
            Vehicle currentVehicle = getVehicleFromSystem(i_LicensePlateID);

            if (currentVehicle is ElectricVehicle electricVehicle)
            {
                electricVehicle.ChargeBattery(i_MinutesToCharge);
            }
            else
            {
                throw new ArgumentException("Expected an electric vehicle but received fuel vehicle.");
            }
        }

        public string getVehicleInformationstring(string i_LicensePlateID) //TODO
        {

        }

        private Vehicle getVehicleFromSystem(string i_LicensePlateID) //GOOD
        {
            if (!isVehicleInSystem(i_LicensePlateID))
            {
                throw new ArgumentException($"Vehicle with license plate ID '{i_LicensePlateID}' does not exist in the system.");
            }

            Vehicle resultVehicle = null;

            foreach (Vehicle vehicle in r_Vehicles)
            {
                if (vehicle.VehicleInfo.LicensePlateID.Equals(i_LicensePlateID))
                {
                    resultVehicle = vehicle;
                    break;
                }
            }

            return resultVehicle;
        }

        public bool isVehicleInSystem(string i_LicensePlateID) //GOOD
        {
            return r_VehicleInformation.ContainsKey(i_LicensePlateID);
        }
    }
}
