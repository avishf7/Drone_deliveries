﻿using DalApi;
using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL
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
                if (numOfChargeStation < blSt.ChargingDrones.Count())
                {
                    throw new TooSmallAmount("There is more drones in charge then new charge slots");
                }

                if (numOfChargeStation == 0)
                {
                    throw new TooSmallAmount("There must be at least one charging station," +
                        " to delete the station check the options");
                }
                dalSt.FreeChargeSlots = numOfChargeStation - blSt.ChargingDrones.Count();
            }

            dal.UpdateStation(dalSt);

        }

        public Station GetStation(int stationId)
        {
            try
            {
                DO.Station DoStation = dal.GetStation(stationId);

                return new()
                {
                    Id = DoStation.Id,
                    Name = DoStation.Name,
                    FreeChargeSlots = DoStation.FreeChargeSlots,
                    LocationOfStation = new Location { Lattitude = DoStation.Lattitude, Longitude = DoStation.Longitude },
                    ChargingDrones = dal.GetDronesCharges(x => x.StationId == stationId)
                                     .Select(drCh => new DroneCharge
                                     {
                                         DroneId = drCh.DroneId,
                                         BatteryStatus = droneLists.Find(x => x.Id == drCh.DroneId).BatteryStatus
                                     }).OrderBy(drCh => drCh.BatteryStatus)
                };
            }
            catch (DalApi.NoNumberFoundException ex)
            {
                throw new BlApi.NoNumberFoundException("Station ID not found", ex);
            }
        }

        public IEnumerable<StationToList> GetStations(Predicate<StationToList> predicate = null)
        {
            return dal.GetStations().Select(st => new StationToList()
            {
                Id = st.Id,
                Name = st.Name,
                NumberOfChargingStationsOccupied = dal.GetDronesCharges(drCh => drCh.StationId == st.Id).Count(),
                SeveralAvailableChargingStations = st.FreeChargeSlots
            }).Where(st => predicate != null ? predicate(st) : true);
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
