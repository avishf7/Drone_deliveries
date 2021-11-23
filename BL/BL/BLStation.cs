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

        public void UpdateStation(int stationId, string name, int numOfChargeStation)
        {
            try
            {
                List<IDAL.DO.Station> stationsT = dal.GetStations().ToList();
                IDAL.DO.Station st = stationsT.Find(x => x.Id == stationId);
                Station station = GetStation(stationId);

                st.Name = name;
                if (numOfChargeStation < station.ChargingDrones.Count)
                {
                    // throw לא ניתן להכניס כמות זו;
                }
                st.FreeChargeSlots = numOfChargeStation - station.ChargingDrones.Count;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Station GetStation(int stationId)
        {
            try
            {
                IDAL.DO.Station DoStation = dal.GetStation(stationId);
                Station BoStation = new()
                {
                    Id = DoStation.Id,
                    Name = DoStation.Name,
                    FreeChargeSlots = DoStation.FreeChargeSlots,
                    LocationOfStation = new Location { Lattitude = DoStation.Lattitude, Longitude = DoStation.Longitude },

                };
                List<IDAL.DO.DroneCharge> doDroneCharge = dal.GetDronesCharges(x => x.StationId == stationId).ToList();
                foreach (var i in doDroneCharge)
                {
                    BoStation.ChargingDrones.Add(new DroneCharge
                    {
                        DroneId = i.DroneId,
                        BatteryStatus = DroneLists[DroneLists.FindIndex(x => x.Id == i.DroneId)].BatteryStatus
                    });
                }
                BoStation.ChargingDrones.OrderBy(i => i.BatteryStatus);

                return BoStation;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<StationToList> GetStations(Predicate<StationToList> predicate = null)
        {
            List<IDAL.DO.Station> doStations = (List<IDAL.DO.Station>)dal.GetStations();
            
            List<StationToList> boStations = new();
            foreach (var st in doStations)
            {

                int numberOfChargingStationsOccupied = DroneLists.FindAll(dr => dr.DroneStatus == DroneStatuses.MAINTENANCE &&
                                                                          dr.LocationOfDrone.Lattitude == st.Lattitude &&
                                                                          dr.LocationOfDrone.Longitude == st.Longitude).Count;

                StationToList stToList = new()
                {
                    Id = st.Id,
                    Name = st.Name,
                    NumberOfChargingStationsOccupied = numberOfChargingStationsOccupied,
                    SeveralAvailableChargingStations = st.FreeChargeSlots                    
                };

                if (predicate != null ? predicate(stToList) : true)
                    boStations.Add(stToList);

            }

            return boStations;
        }

        public void DeleteStation(int id)
        {
            throw new NotImplementedException();
        }
    }
}
