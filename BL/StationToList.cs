using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class StationToList
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
        /// 
        /// </summary>
        public int SeveralAvailableChargingStations { get; set; }
        /// <summary>
        /// 
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
