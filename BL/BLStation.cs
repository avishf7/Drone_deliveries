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
        public void AddStation(StationToList station)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(StationToList station)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int stationId)
        {
            throw new NotImplementedException();
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
