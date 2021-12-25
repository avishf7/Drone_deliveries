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
        public void AddStation(Station station)
        {
            try
            {
                dal.AddStation(new DO.Station
                {
                    Id = station.Id,
                    Name = station.Name,
                    Longitude = station.LocationOfStation.Longitude,
                    Lattitude = station.LocationOfStation.Lattitude,
                    FreeChargeSlots = station.FreeChargeSlots,
                });
            }
            catch (DalApi.ExistsNumberException ex)
            {
                throw new BlApi.ExistsNumberException("Station already exists ", ex);
            }
        }

        public void UpdateStation(int stationId, string name, int numOfChargeStation)
        {
            DO.Station dalSt;
            Station blSt;

            try
            {
                dalSt = dal.GetStation(stationId);
                blSt = GetStation(stationId);

            }
            catch (DalApi.NoNumberFoundException ex)
            {
                throw new BlApi.NoNumberFoundException("Station ID not found", ex);
            }

            if (name != "")
                    dalSt.Name = name;
                if (numOfChargeStation != -1)
                {
                    if (numOfChargeStation < blSt.ChargingDrones.Count)
                    {
                        throw new TooSmallAmount("There is more drones in charge then new charge slots");
                    }
                    dalSt.FreeChargeSlots = numOfChargeStation - blSt.ChargingDrones.Count;
                }

                dal.UpdateStation(dalSt);
            
        }

        public Station GetStation(int stationId)
        {
            try
            {
                DO.Station DoStation = dal.GetStation(stationId);

                Station BoStation = new()
                {
                    Id = DoStation.Id,
                    Name = DoStation.Name,
                    FreeChargeSlots = DoStation.FreeChargeSlots,
                    LocationOfStation = new Location { Lattitude = DoStation.Lattitude, Longitude = DoStation.Longitude },
                    ChargingDrones = new()
                };

                List<DO.DroneCharge> doDroneCharge = dal.GetDronesCharges(x => x.StationId == stationId).ToList();

                foreach (var i in doDroneCharge)
                {
                    BoStation.ChargingDrones.Add(new DroneCharge
                    {
                        DroneId = i.DroneId,
                        BatteryStatus = droneLists.Find(x => x.Id == i.DroneId).BatteryStatus
                    });
                }
                BoStation.ChargingDrones.OrderBy(i => i.BatteryStatus);

                return BoStation;
            }
            catch (DalApi.NoNumberFoundException ex)
            {
                throw new BlApi.NoNumberFoundException("Station ID not found", ex);
            }
        }

        public IEnumerable<StationToList> GetStations(Predicate<StationToList> predicate = null)
        {
            List<DO.Station> doStations = (List<DO.Station>)dal.GetStations();
            List<StationToList> boStations = new();

            foreach (var st in doStations)
            {
                StationToList stToList = new()
                {
                    Id = st.Id,
                    Name = st.Name,
                    NumberOfChargingStationsOccupied = dal.GetDronesCharges(drCh => drCh.StationId == st.Id).ToList().Count,
                    SeveralAvailableChargingStations = st.FreeChargeSlots
                };

                if (predicate != null ? predicate(stToList) : true)
                    boStations.Add(stToList);
            }

            return boStations;
        }

        public void DeleteStation(int id)
        {
            try
            {
                dal.DeleteStation(id);
            }
            catch (Exception)
            {
                throw new NotImplementedException(); 
            }
           
        }
    }
}
