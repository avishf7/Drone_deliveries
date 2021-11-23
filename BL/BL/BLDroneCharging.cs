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
                int iDr = DroneLists.FindIndex(x => x.Id == DroneId);
                var dr = DroneLists.Find(x => x.Id == DroneId);

                if (dr == null || dr.DroneStatus != DroneStatuses.AVAILABLE)
                {
                    //  throw NoNumberFoundException;
                }

                List<IDAL.DO.Station> stationsT = dal.GetStations(x => x.FreeChargeSlots > 0).ToList();

                Location stLocation = FindClosestStationLocation(dr.LocationOfDrone);                
                double KM = Distance(stLocation, dr.LocationOfDrone);
                

                if (KM <dr.BatteryStatus*DroneAvailable)
                {
                    //throw
                }

                dr.BatteryStatus = KM + 1;
                dr.LocationOfDrone = stLocation;
                dr.DroneStatus = DroneStatuses.MAINTENANCE;

                DroneLists.Insert(iDr, dr);

                IDAL.DO.Station station = stationsT.Find(x => x.Lattitude == stLocation.Lattitude && x.Longitude == stLocation.Longitude);
                dal.UsingChargingStation(station.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void RealeseDroneFromCharge(int DroneId, TimeSpan time)
        {
            try
            {
                int drr = DroneLists.FindIndex(x => x.Id == DroneId);
                var dr = DroneLists.Find(x => x.Id == DroneId);

                double timeOfCharg = time.TotalHours;

                List<double> tmp = dal.ChargingRequest();

                List<IDAL.DO.Station> stationsT = dal.GetStations(x => x.Lattitude==dr.LocationOfDrone.Lattitude&& x.Longitude==dr.LocationOfDrone.Longitude).ToList();
                IDAL.DO.Station station = stationsT.Find(x => x.Lattitude == dr.LocationOfDrone.Lattitude && x.Longitude == dr.LocationOfDrone.Longitude);


                if (dr==null || dr.DroneStatus!=DroneStatuses.MAINTENANCE)
                {
                 //   throw;
                }
                dr.BatteryStatus = timeOfCharg * tmp[4];
                dr.DroneStatus = DroneStatuses.AVAILABLE;

                DroneLists[drr].BatteryStatus = dr.BatteryStatus;
                DroneLists[drr].DroneStatus = dr.DroneStatus;

                dal.RealeseChargingStation(station.Id);
            }
            catch (Exception)
            {
                throw;
            }     
        }
    }
}
