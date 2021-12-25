using DalApi;
using BlApi;
using BlApi.BO;
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

            var dr = droneLists.Find(x => x.Id == DroneId);


            if (dr == null)
            {
                throw new BlApi.NoNumberFoundException("Drone ID not found");
            }

            if (dr.DroneStatus != DroneStatuses.Available)
            {
                throw new DroneNotAvailableException();
            }

            Location stLocation;
            try { stLocation = FindClosestStationLocation(dr.LocationOfDrone, x => x.FreeChargeSlots > 0); }
            catch (BlApi.NoNumberFoundException ex)
            {
                throw new BlApi.NoNumberFoundException("There is no station with available charging stations", ex);
            }

            double KM = Distance(stLocation, dr.LocationOfDrone);


            if (KM <= dr.BatteryStatus * DroneAvailable)
            {
                dr.BatteryStatus = dr.BatteryStatus - KM / DroneAvailable;
                dr.LocationOfDrone = stLocation;
                dr.DroneStatus = DroneStatuses.Maintenance;

                List<DO.Station> stationsT = dal.GetStations(x => x.FreeChargeSlots > 0).ToList();
                DO.Station station = stationsT.Find(x => x.Lattitude == stLocation.Lattitude && x.Longitude == stLocation.Longitude);

                dal.UsingChargingStation(station.Id);
                dal.AddDroneCharge(new() { DroneId = DroneId, StationId = station.Id, ChargeStart = DateTime.Now });
            }
            else
            {
                throw new NotEnoughBattery("Not enough to get to the nearest available station");
            }


        }


        public void RealeseDroneFromCharge(int DroneId, TimeSpan time)
        {
            var dr = droneLists.Find(x => x.Id == DroneId);

            if (dr == null)
            {
                throw new BlApi.NoNumberFoundException("Drone ID not found");
            }

            if (dr.DroneStatus != DroneStatuses.Maintenance)
            {
                throw new DroneNotMaintenanceException();
            }


            dr.BatteryStatus = dr.BatteryStatus + time.TotalHours * ChargingRate;
            if (dr.BatteryStatus > 100)
            {
                dr.BatteryStatus = 100;
            }
            dr.DroneStatus = DroneStatuses.Available;

            List<DO.Station> stationsT = dal.GetStations(x => x.Lattitude == dr.LocationOfDrone.Lattitude && x.Longitude == dr.LocationOfDrone.Longitude).ToList();
            DO.Station station = stationsT.Find(x => x.Lattitude == dr.LocationOfDrone.Lattitude && x.Longitude == dr.LocationOfDrone.Longitude);

            dal.RealeseChargingStation(station.Id);
            dal.DeleteDroneCharge(DroneId);

        }   

        public void RealeseDroneFromCharge(int DroneId)
        {
            var timeOfCharge = DateTime.Now - dal.GetDroneCharge(DroneId).ChargeStart;

            RealeseDroneFromCharge(DroneId, (TimeSpan)timeOfCharge);
        }
    }
}
