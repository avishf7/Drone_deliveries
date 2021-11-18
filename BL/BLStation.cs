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
        public void AddStation(Station station)
        {
            try
            {
                dal.AddStation(new IDAL.DO.Station
                {
                    Id = station.Id,
                    Name = station.Name,
                    Longitude = station.LocationOfStation.Longitude,
                    Lattitude = station.LocationOfStation.Lattitude,
                    FreeChargeSlots = station.FreeChargeSlots,
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public void UpdateStation(StationToList station)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int stationId)
        {
            try
            {
                IDAL.DO.Station DoStation = dal.GetStation(stationId);
                Station BoStation= new()
                {
                    Id = DoStation.Id,
                    Name = DoStation.Name,
                    FreeChargeSlots = DoStation.FreeChargeSlots,
                    LocationOfStation = new Location { Lattitude = DoStation.Lattitude, Longitude = DoStation.Longitude },
                   
                };
                List <IDAL.DO.DroneCharge> doDroneCharge = dal.GetDronesCharges(x => x.StationId == stationId).ToList();
                foreach (var i in doDroneCharge)
                {
                    BoStation.ChargingDrones.Add(new DroneCharge { DroneId = i.DroneId, 
                        BatteryStatus = DroneLists[DroneLists.FindIndex(x => x.Id == i.DroneId)].BatteryStatus });
                }
                BoStation.ChargingDrones.OrderBy(i => i.BatteryStatus);
       
                return BoStation;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<StationToList> GetStations(Predicate<Station> predicate = null)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int id)
        {
            throw new NotImplementedException();
        }
    }
}
