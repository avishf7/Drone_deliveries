using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    static class PLExpandingfunctions
    {
        /// <summary>
        /// The function copies properties from a BL Drone object to a PL Drone object
        /// and returns the PL Drone object after copy
        /// </summary>
        /// <param name="PODrone">PL Drone object</param>
        /// <param name="BODrone">BL Drone object</param>
        /// <returns>this PL Drone object</returns>
        public static PO.Drone CopyFromBODrone(this PO.Drone PODrone, BO.Drone BODrone)
        {
            PODrone.Id = BODrone.Id;
            PODrone.Model = BODrone.Model;
            PODrone.BatteryStatus = BODrone.BatteryStatus;
            PODrone.DroneStatus = BODrone.DroneStatus;
            PODrone.MaxWeight = BODrone.MaxWeight;
            PODrone.LocationOfDrone = BODrone.LocationOfDrone;
            PODrone.PackageInProgress = BODrone.PackageInProgress;

            return PODrone;
        }


        /// <summary>
        /// The function copies properties from a BL Station object to a PL Station object
        /// and returns the PL Station object after copy
        /// </summary>
        /// <param name="POStation">PL Station object</param>
        /// <param name="BOStation">BL Station object</param>
        /// <returns>this PL Station object</returns>
        public static PO.Station CopyFromBOStation(this PO.Station POStation, BO.Station BOStation)
        {
            POStation.Id = BOStation.Id;
            POStation.Name = BOStation.Name;
            POStation.LocationOfStation = BOStation.LocationOfStation;
            POStation.FreeChargeSlots = BOStation.FreeChargeSlots;
            POStation.ChargingDrones = BOStation.ChargingDrones;


            return POStation;
        }


        /// <summary>
        /// The function copies properties from a BL Customer object to a PL Customer object
        /// and returns the PL Customer object after copy
        /// </summary>
        /// <param name="POCustomer">PL Customer object</param>
        /// <param name="BOCustomer">BL Customer object</param>
        /// <returns>this PL Station object</returns>
        public static PO.Customer CopyFromBOCustomer(this PO.Customer POCustomer, BO.Customer BOCustomer)
        {
            POCustomer.Id = BOCustomer.Id;
            POCustomer.Name = BOCustomer.Name;
            POCustomer.Phone = BOCustomer.Phone;
            POCustomer.CustomerLocation = BOCustomer.CustomerLocation;
            POCustomer.PackageAtCustomerFromCustomer = BOCustomer.PackageAtCustomerFromCustomer;
            POCustomer.PackageAtCustomerToCustomer = BOCustomer.PackageAtCustomerToCustomer;


            return POCustomer;
        }

        /// <summary>
        /// The function copies properties from a BL Package object to a PL Package object
        /// and returns the PL Package object after copy
        /// </summary>
        /// <param name="POPackage">PL Package object</param>
        /// <param name="BOPackage">BL Package object</param>
        /// <returns>this PL Station object</returns>
        public static PO.Package CopyFromBOPackage(this PO.Package POPackage, BO.Package BOPackage)
        {
            POPackage.Id = BOPackage.Id;
            POPackage.SenderCustomerInPackage = BOPackage.SenderCustomerInPackage;
            POPackage.TargetCustomerInPackage = BOPackage.TargetCustomerInPackage;
            POPackage.Weight = BOPackage.Weight;
            POPackage.Priority = BOPackage.Priority;
            POPackage.DroneInPackage = BOPackage.DroneInPackage;
            POPackage.Requested = BOPackage.Requested;
            POPackage.Scheduled = BOPackage.Scheduled;
            POPackage.PickedUp = BOPackage.PickedUp;
            POPackage.Delivered = BOPackage.Delivered;

            return POPackage;
        }



    }
}
