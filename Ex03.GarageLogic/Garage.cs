using System;
using System.Collections.Generic;
using Vehicles;

namespace VehicleGarage
{
    public enum eVehicleStatus
    {
        InRepair,
        WasRepair,
        WasPaidFor
    }

    public class Garage
    {
        private Dictionary<string, VehicleInformations> m_Vehicle = new Dictionary<string, VehicleInformations>();
        
        internal class VehicleInformations
        {
            public string m_OwnerName;
            public string m_OwnerPhoneNumber;
            public eVehicleStatus m_VehicleStatus;
            public Vehicle m_VehicleDetails;
        }

        public void ReEnterVehicleToGarage(string i_LicensePlateID)
        {
            VehicleInformations vehicleToReEnterToGarage = getVehicleFromSystem(i_LicensePlateID);
            vehicleToReEnterToGarage.m_VehicleStatus = eVehicleStatus.InRepair;
        }

        public void EnterNewVehicleToGarage()
        {

        }

        public List<string> getVehiclesLicensePlateListByStatus(eVehicleStatus i_VehicleStatusWanted)
        {
            List<string> licensePlatesList = new List<string>();

            foreach (string licensePlate in m_Vehicle.Keys)
            {
                if (m_Vehicle[licensePlate].m_VehicleStatus == i_VehicleStatusWanted)
                {
                    licensePlatesList.Add(licensePlate);
                }
            }

            return licensePlatesList;
        }

        public List<string> getListOfAllVehiclesInTheGarage()
        {
            List<string> VhiecleList = new List<string>();

            foreach (string licensePlate in m_Vehicle.Keys)
            {
                VhiecleList.Add(licensePlate);
            }

            return VhiecleList;
        }

        public void ChangeVehicleStatus(string i_LicensePlateID, eVehicleStatus i_VehicleStatusToChange)
        {
            VehicleInformations vehicleToReEnterToGarage = getVehicleFromSystem(i_LicensePlateID);
            vehicleToReEnterToGarage.m_VehicleStatus = i_VehicleStatusToChange;
        }

        public void InflateWheelsToMaximum(string i_LicensePlateID)
        {
            VehicleInformations vehicleInformation = getVehicleFromSystem(i_LicensePlateID);
            foreach (Wheel wheel in vehicleInformation.m_VehicleDetails.m_Wheels)
            {
                wheel.InflateToMaximum();
            }
        }

        public void RefuelVehicle(string i_LicensePlateID, eGasTypes i_GasType, float i_AmountToRefuel)
        {
            VehicleInformations vehicleInformation = getVehicleFromSystem(i_LicensePlateID);
            EnergyStorage currentEnergyStorage = vehicleInformation.m_VehicleDetails.m_VehicleTank;
            currentEnergyStorage.Refuel(i_AmountToRefuel, i_GasType);
        }

        public void ChargeVehicle(string i_LicensePlateID, int i_MinutesToCharge)
        {
            Vehicle currentVehicle = getVehicleFromSystem(i_LicensePlateID).m_VehicleDetails;
            EnergyStorage currentEnergyStorage = currentVehicle.m_VehicleTank;
            currentEnergyStorage.Refuel(i_MinutesToCharge);
        }

        internal VehicleInformations getVehicleFromSystem(string i_LicensePlateID)
        {
            VehicleInformations vehicleToReturn;
            if (m_Vehicle.TryGetValue(i_LicensePlateID, out vehicleToReturn))
            {
                return vehicleToReturn;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public bool isVehicleInSystem(string i_LicensePlateID)
        {
            return m_Vehicle.ContainsKey(i_LicensePlateID);
        }
    }
}
