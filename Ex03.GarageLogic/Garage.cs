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
        internal class VehicleDetails
        {
            string m_OwnerName;
            string m_OwnerPhoneNumber;
            eVehicleStatus m_VehicleStatus;
            Vehicle m_Vehicle;
        }

        public void ReEnterVehicleToGarage(string i_LicensePlateID)
        {

        }
        public bool IsVehicleInSystem() //TODO
        {
            bool res = false;
            return res;
        }
        public void EnterNewVehicleToGarage()
        {

        }
        public List<Vehicle> getVehiclesLicensePlateList(eVehicleStatus i_VehicleStatusWanted) //TODO
        {
            List<Vehicle> res = new List<Vehicle>();
            return res;
        }
        public void ChangeVehicleStatus(string i_LicensePlateID, eVehicleStatus i_VehicleStatusToChange)
        {

        }
        public void InflateWheelsToMaximum(string i_LicensePlateID)
        {

        }
        public void RefuelVehicle(string i_LicensePlateID, eGasTypes i_GasType, float i_AmountToRefuel)
        {

        }
        public void ChargeVehicle(string i_LicensePlateID, int i_MinutesToCharge)
        {

        }
        public void GetVehicleDetails(string i_LicensePlateID)
        {

        }
    }
}
