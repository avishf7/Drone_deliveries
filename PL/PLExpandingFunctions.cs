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
        /// <returns>PL Drone object</returns>
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

        public static PO.Station CopyFromBOStation(this PO.Station POStation, BO.Station BOStation)
        {
            POStation.Id = BOStation.Id;
            POStation.Name = BOStation.Name;
            POStation.LocationOfStation = BOStation.LocationOfStation;
            POStation.FreeChargeSlots = BOStation.FreeChargeSlots;
            POStation.ChargingDrones = BOStation.ChargingDrones;


            return POStation;
        }

    }
}
