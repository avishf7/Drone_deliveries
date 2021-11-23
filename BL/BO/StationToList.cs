using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class StationToList
    {
        /// <summary>
        /// Gets the id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets several available charging stations
        /// </summary>
        public int SeveralAvailableChargingStations { get; set; }

        /// <summary>
        /// Gets number of charging stations occupied
        /// </summary>
        public int NumberOfChargingStationsOccupied { get; set; }



       
        public override string ToString()
        {
            return "Details of ID :" + Id + "\nName: " + Name + "\nSeveral available charging stations: "
                + SeveralAvailableChargingStations + "\nNumber of charging stations occupied: " +
                NumberOfChargingStationsOccupied + "\n";
        }
    }
}
