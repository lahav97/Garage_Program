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
        private readonly Dictionary<string, VehicleInformations> r_Vehicle = new Dictionary<string, VehicleInformations>();
        private readonly List<Vehicle> r_Vehicles = new List<Vehicle>();

        public class VehicleInformations
        {
            public string m_OwnerName;
            public string m_OwnerPhoneNumber;
            public eVehicleStatus m_VehicleStatus;

            public string OwnerName
            {
                get { return m_OwnerName; } 
                private set { m_OwnerName = value; }
            }

            public string OwnerPhoneNumber
            {
                get { return m_OwnerPhoneNumber; }
                private set
                {
                    m_OwnerPhoneNumber = value;
                }
            }
        }

        public void ReEnterVehicleToGarage(string i_LicensePlateID)
        {
            VehicleInformations vehicleToReEnterToGarage = getVehicleFromSystem(i_LicensePlateID);
            vehicleToReEnterToGarage.m_VehicleStatus = eVehicleStatus.InRepair;
        }

        public void EnterNewVehicleToGarage()
        {

        }

        public List<string> GetVehiclesLicensePlateListByStatus(string i_VehicleStatus) //GOOD
        {
            List<string> licensePlatesList;

            if (!i_VehicleStatus.Equals("All"))
            {
                licensePlatesList = r_Vehicle.Keys.ToList();
            }
            else
            {
                eVehicleStatus vehicleStatus = ValidateAndParseVehicleStatus(i_VehicleStatus);

                licensePlatesList = r_Vehicle
                    .Where(licensePlates => licensePlates.Value.m_VehicleStatus == vehicleStatus)
                    .Select(licensePlates => licensePlates.Key)
                    .ToList();
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

            foreach (string licensePlate in r_Vehicle.Keys)
            {
                VhiecleList.Add(licensePlate);
            }

            return VhiecleList;
        }

        public void ChangeVehicleStatus(string i_LicensePlateID, eVehicleStatus i_VehicleStatusToChange) //CHECK IF GOOD
        {
            VehicleInformations vehicleToReEnterToGarage = getVehicleFromSystem(i_LicensePlateID);
            vehicleToReEnterToGarage.m_VehicleStatus = i_VehicleStatusToChange;
        }

        public void InflateWheelsToMaximum(string i_LicensePlateID)
        {
            VehicleInformations vehicleInformation = getVehicleFromSystem(i_LicensePlateID);
            foreach (Wheel wheel in vehicleInformation.r_VehicleDetails.m_Wheels)
            {
                wheel.InflateToMaximum();
            }
        }

        public void RefuelVehicle(string i_LicensePlateID, eFuelTypes i_FuelType, float i_AmountToRefuel)
        {
            VehicleInformations vehicleInformation = getVehicleFromSystem(i_LicensePlateID);
            EnergyStorage currentEnergyStorage = vehicleInformation.r_VehicleDetails.r_VehicleTank;
            currentEnergyStorage.Refuel(i_AmountToRefuel, i_FuelType);
        }

        public void ChargeVehicle(string i_LicensePlateID, int i_MinutesToCharge)
        {
            Vehicle currentVehicle = getVehicleFromSystem(i_LicensePlateID).r_VehicleDetails;
            EnergyStorage currentEnergyStorage = currentVehicle.r_VehicleTank;
            currentEnergyStorage.Refuel(i_MinutesToCharge);
        }

        private Vehicle getVehicleFromSystem(string i_LicensePlateID) //GOOD
        {
            Vehicle resulVehicle = null;
            if(isVehicleInSystem(i_LicensePlateID))
            {
                foreach (Vehicle vehicle in r_Vehicles)
                {
                    if (vehicle.VehicleInfo.LicensePlateID.Equals(i_LicensePlateID))
                    {
                        resulVehicle = vehicle;
                        break;
                    }
                }
            }
            else
            {
                throw new ArgumentException($"Vehicle with license plate ID '{i_LicensePlateID}' does not exist in the system.");
            }

            return resulVehicle;
        }

        public bool isVehicleInSystem(string i_LicensePlateID) //GOOD
        {
            return r_Vehicle.ContainsKey(i_LicensePlateID);
        }
    }
}
