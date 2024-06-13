using System;
using System.Collections.Generic;
using Vehicles;

namespace Garage
{
    public enum eVehicleStatus
    {
        InRepair,
        WasRepair,
        WasPaidFor
    }

    internal class Garage
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
            VehicleInformations vehicleToReEnterToGarage = findVehicleInSystem(i_LicensePlateID);
            vehicleToReEnterToGarage.m_VehicleStatus = eVehicleStatus.InRepair;
        }


        public VehicleInformations findVehicleInSystem(string i_LicensePlateID) //TODO
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

        public void EnterNewVehicleToGarage()
        {

        }

        public List<string> getVehiclesLicensePlateList(eVehicleStatus i_VehicleStatusWanted)
        {
            List<string> licensePlates = new List<string>();

            foreach (string licensePlate in m_Vehicle.Keys)
            {
                if (m_Vehicle[licensePlate].m_VehicleStatus == i_VehicleStatusWanted)
                {
                    licensePlates.Add(licensePlate);
                }
            }

            return licensePlates;
        }

        public void ChangeVehicleStatus(string i_LicensePlateID, eVehicleStatus i_VehicleStatusToChange)
        {
            VehicleInformations vehicleToReEnterToGarage = findVehicleInSystem(i_LicensePlateID);
            vehicleToReEnterToGarage.m_VehicleStatus = i_VehicleStatusToChange;
        }

        public void InflateWheelsToMaximum(string i_LicensePlateID)
        {
            VehicleInformations vehicleInformation = findVehicleInSystem(i_LicensePlateID);
            foreach (Wheel wheel in vehicleInformation.m_VehicleDetails.m_Wheels)
            {
                wheel.InflateToMaximum();
            }
        }

        public void RefuelVehicle(string i_LicensePlateID, eGasTypes i_GasType, float i_AmountToRefuel)
        {
            VehicleInformations vehicleInformation = findVehicleInSystem(i_LicensePlateID);
            EnergyStorage currentEnergyStorage = vehicleInformation.m_VehicleDetails.m_VehicleTank;
            currentEnergyStorage.Refuel(i_AmountToRefuel, i_GasType);
        }

        public void ChargeVehicle(string i_LicensePlateID, int i_MinutesToCharge)
        {
            VehicleInformations vehicleInformation = findVehicleInSystem(i_LicensePlateID);
            EnergyStorage currentEnergyStorage = vehicleInformation.m_VehicleDetails.m_VehicleTank;
            currentEnergyStorage.Refuel(i_MinutesToCharge);
        }

        public Vehicle GetVehicleDetails(string i_LicensePlateID)
        {
            foreach(string licensePlate in m_Vehicle.Keys)
            {
                if(m_Vehicle[licensePlate] == i_LicensePlateID)
                {

                }
            }
            
        }
    }
}
