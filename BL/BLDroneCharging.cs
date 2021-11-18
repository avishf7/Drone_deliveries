using IDAL;
using IBL;
using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBl
    {
        public void SendDroneForCharge(int DroneId)
        {
            try
            {
                int drr = DroneLists.FindIndex(x => x.Id == DroneId);
                var dr = DroneLists.Find(x => x.Id == DroneId);
                if (dr == null || dr.DroneStatus != DroneStatuses.AVAILABLE)
                {
                    //  throw NoNumberFoundException;
                }
                List<IDAL.DO.Station> stationsT = dal.GetStations(x => x.FreeChargeSlots > 0).ToList();

                Location location = FindClosestStationLocation(dr.LocationOfDrone);
                IDAL.DO.Station station = stationsT.Find(x => x.Lattitude == location.Lattitude && x.Longitude == location.Longitude);
                double KM = Distance(location, dr.LocationOfDrone);
                if (KM <dr.BatteryStatus*DroneAvailable)
                {
                    //throw
                }
                dr.BatteryStatus = KM + 1;
                dr.LocationOfDrone = location;
                dr.DroneStatus = DroneStatuses.MAINTENANCE;
                dal.UsingChargingStation(station.Id);

                DroneLists[drr].DroneStatus = dr.DroneStatus;
                DroneLists[drr].LocationOfDrone = dr.LocationOfDrone;
                


            }
            catch (Exception)
            {

                throw;
            }


        }

        public void RealeseDroneFromCharge(int DroneId, TimeSpan time)
        {
            throw new NotImplementedException();
        }
    }
}
