using System.Collections.Generic;
using GarageLogic.Vehicles;
using GarageLogic.Vehicles.Types;
using GarageLogic.Vehicles.VehicleFactory;
using System;
using System.Linq;
using System.Text;
using GarageLogic.GarageManagement;

namespace VehicleGarage
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleInformations> r_VehicleInformation = new Dictionary<string, VehicleInformations>();
        private readonly Dictionary<string, Vehicle> r_Vehicles = new Dictionary<string, Vehicle>();
        public readonly int r_MaxSizeOfVehicleStatus = Enum.GetValues(typeof(eVehicleStatus)).Cast<int>().Max();

        public List<string> GetListOfVehicleTypesInGarage()
        {
            List<string> ListOfPromptsToSend = new List<string>();

            foreach (eVehicleType vehicleType in Enum.GetValues(typeof(eVehicleType)))
            {
                ListOfPromptsToSend.Add(separateWordsByUpperCase(vehicleType.ToString()));
            }

            return ListOfPromptsToSend;
        }

        private string separateWordsByUpperCase(string i_SentenceToSeperate)
        {
            string vehicleTypeName = string.Empty;

            for (int i = 0; i < i_SentenceToSeperate.Length; i++)
            {
                if (i > 0 && char.IsUpper(i_SentenceToSeperate[i]) && !char.IsUpper(i_SentenceToSeperate[i - 1]))
                {
                    vehicleTypeName += " ";
                }

                vehicleTypeName += i_SentenceToSeperate[i];
            }

            return vehicleTypeName;
        }
        
        public void EnterNewVehicleToGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            VehicleInformations vehicleInformations = new VehicleInformations();

            vehicleInformations.OwnerPhoneNumber = i_OwnerPhoneNumber;
            vehicleInformations.OwnerName = i_OwnerName;
            vehicleInformations.VehicleStatus = eVehicleStatus.InRepair;
            r_Vehicles.Add(i_Vehicle.m_VehicleInfo.LicensePlateID, i_Vehicle);
            r_VehicleInformation.Add(i_Vehicle.m_VehicleInfo.LicensePlateID, vehicleInformations);
        }

        public List<string> GetVehicleStatusPrompt()
        {
            List<string> listOfPromptsToSend = new List<string>();

            foreach (eVehicleStatus vehicleStatus in Enum.GetValues(typeof(eVehicleStatus)))
            {
                listOfPromptsToSend.Add(separateWordsByUpperCase(vehicleStatus.ToString()));
            }

            listOfPromptsToSend.Add("All vehicles");

            return listOfPromptsToSend;
        }

        public List<string> GetVehiclesLicensePlateListByStatus(string i_VehicleStatus)
        {
            eVehicleStatus vehicleStatus;
            List<string> licensePlatesList;

            bool getAllVehile = i_VehicleStatus == $"{r_MaxSizeOfVehicleStatus + 1}";

            if (getAllVehile) // To return all of the license plates.
            {
                licensePlatesList = r_VehicleInformation.Keys.ToList();
            }
            else
            {
                if (!Enum.TryParse(i_VehicleStatus, true, out vehicleStatus) || !Enum.IsDefined(typeof(eVehicleStatus), vehicleStatus))
                {
                    throw new ArgumentException("Invalid vehicle status!");
                }

                licensePlatesList = r_VehicleInformation
                    .Where(vehicleInfo => vehicleInfo.Value.VehicleStatus == vehicleStatus)
                    .Select(vehicleInfo => vehicleInfo.Key)
                    .ToList();
            }

            return licensePlatesList;
        }

        public void ChangeVehicleStatus(string i_LicensePlateID, int i_VehicleStatusToChange)
        {
            if (r_VehicleInformation.TryGetValue(i_LicensePlateID, out VehicleInformations vehicleInfo))
            {
                vehicleInfo.VehicleStatus = (eVehicleStatus)i_VehicleStatusToChange;
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

        public void RefuelVehicle(string i_LicensePlateID, string i_FuelType, float i_AmountToRefuel)
        {
            Vehicle currentVehicle = getVehicleFromSystem(i_LicensePlateID);
            eFuelTypes fuelTypeToEnter;

            if (!Enum.TryParse(i_FuelType, true, out fuelTypeToEnter))
            {
                throw new ArgumentException("Input for fuel type was wrong!");
            }
            if (currentVehicle is FuelVehicle currentFuelVehicle)
            {
                currentFuelVehicle.Refuel(i_AmountToRefuel, fuelTypeToEnter);
            }
            else
            {
                throw new ArgumentException("Expected a fuel vehicle but received an electric vehicle.");
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

        public string GetVehicleInformation(string i_LicensePlateID)
        {
            if (!IsVehicleInSystem(i_LicensePlateID))
            {
                throw new ArgumentException($"Vehicle with license plate ID '{i_LicensePlateID}' does not exist in the system.");
            }

            Vehicle vehicle = r_Vehicles[i_LicensePlateID];
            VehicleInformations vehicleInfo = r_VehicleInformation[i_LicensePlateID];
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Owner's Information:");
            stringBuilder.AppendLine(vehicleInfo.ToString());
            stringBuilder.AppendLine("Vehicle Information:");
            stringBuilder.AppendLine(vehicle.ToString());
            stringBuilder.AppendLine();

            return stringBuilder.ToString();
        }

        private Vehicle getVehicleFromSystem(string i_LicensePlateID)
        {
            Vehicle resultVehicle = null;

            if (!IsVehicleInSystem(i_LicensePlateID))
            {
                throw new ArgumentException($"Vehicle with license plate ID '{i_LicensePlateID}' does not exist in the system.");
            }

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

        public bool IsElectricVehicle(string i_LicensePlateID)
        {
            return getVehicleFromSystem(i_LicensePlateID) is ElectricVehicle;
        }
    }
}
