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
               List <IDAL.DO.Station> DroneSt =dal.GetStations().ToList();
              IDAL.DO.Station st = DroneSt.Find(x => x.Id == staionId);
                dal.AddDrone(new IDAL.DO.Drone
                {
                    Id = drone.Id,
                    MaxWeight = (IDAL.DO.Weight)drone.MaxWeight,
                    Model = drone.Model
                });
                drone.BatteryStatus = rd.NextDouble() * rd.Next((int)20) + 20;
                drone.DroneStatus = DroneStatuses.MAINTENANCE;
                drone.LocationOfDrone.Longitude = st.Longitude;
                drone.LocationOfDrone.Lattitude = st.Lattitude;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateDrone(DroneToList drone, int id)
        {
            try
            {
                List<IDAL.DO.Drone> DroneT = dal.GetDrones().ToList();

                IDAL.DO.Drone st = DroneT.Find(x => x.Id == id);
                dal.UpdateDrone(new IDAL.DO.Drone
                {
                    Model = st.Model
                });
            }
            catch (Exception)
            {

                throw;
            }       
        }

        public Drone GetDrone(int droneId)
        {
            try
            {
            
                IDAL.DO.Drone DoDrone = dal.GetDrone(droneId);
                Drone BoDrone = new()
                {
                    Id = DoDrone.Id,
                    Model = DoDrone.Model,
                    MaxWeight = (Weight)(IDAL.DO.Weight)DoDrone.MaxWeight,
                    BatteryStatus=
                };
            }
            catch (Exception)
            {

                throw;
            }
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
