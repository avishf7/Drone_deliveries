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
        public void AddDrone(Drone drone, int staionId)
        {
            try
            {
                List<IDAL.DO.Station> DroneSt = dal.GetStations().ToList();
                IDAL.DO.Station st = DroneSt.Find(x => x.Id == staionId);
                dal.AddDrone(new IDAL.DO.Drone
                {
                    Id = drone.Id,
                    MaxWeight = (IDAL.DO.Weight)drone.MaxWeight,
                    Model = drone.Model
                });

                drone.LocationOfDrone.Longitude = st.Longitude;
                drone.LocationOfDrone.Lattitude = st.Lattitude;
                dal.UsingChargingStation(st.Id);
                DroneLists.Add(new()
                {
                    Id = drone.Id,
                    MaxWeight = drone.MaxWeight,
                    Model = drone.Model,
                    BatteryStatus = rd.NextDouble() * rd.Next(20) + 20,
                    DroneStatus = DroneStatuses.MAINTENANCE,
                    LocationOfDrone = new() { Lattitude = st.Lattitude, Longitude = st.Longitude },
                    PackageNumber = -1
                });

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateDrone(int droneId, string model)
        {
            try
            {
                List<IDAL.DO.Drone> DroneT = dal.GetDrones().ToList();

                IDAL.DO.Drone dr = DroneT.Find(x => x.Id == droneId);
                dr.Model = model;
                dal.UpdateDrone(dr);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Drone GetDrone(int droneId)
        {

            var dr = DroneLists.Find(x => x.Id == droneId);
            if (dr == null)
            {
                throw new NoNumberFoundExeptions();
            }

            return new()
            {
                Id = dr.Id,
                Model = dr.Model,
                MaxWeight = dr.MaxWeight,
                BatteryStatus =dr.BatteryStatus,
                DroneStatus=dr.DroneStatus,
                DeliveryInProgress = dr.DroneStatus==DroneStatuses.SENDERING ? new() {}
                };
        }

        public IEnumerable<DroneToList> GetDrones(Predicate<Drone> predicate = null)
        {
            throw new NotImplementedException();
        }

        public void DeleteDrone(int id)
        {
            throw new NotImplementedException();
        }
    }
}
