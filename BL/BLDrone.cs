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
        public void AddDrone(DroneToList drone)
        {
            throw new NotImplementedException();
        }

        public void UpdateDrone(DroneToList drone)
        {
            throw new NotImplementedException();
        }

        public Drone GetDrone(int droneId)
        {
            throw new NotImplementedException();
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
