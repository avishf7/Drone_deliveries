using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi.BO
{
    public class DroneToList
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets the model.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets the max weight.
        /// </summary>
        public Weight MaxWeight { get; set; }

        /// <summary>
        /// Gets the drone's battery stutus;
        /// </summary>
        public double BatteryStatus { get; set; }

        /// <summary>
        /// Gets drone status
        /// </summary>
        public DroneStatuses DroneStatus { get; set; }

        /// <summary>
        /// Gets location of drone
        /// </summary>
        public Location LocationOfDrone { get; set; }

        /// <summary>
        /// Gets package number
        /// </summary>
        public int PackageNumber { get; set; }



        public override string ToString()
        {
            return "Details of Id :" + Id + "\nModel: " + Model + "\nMax weight: " +
                 MaxWeight + "\nStatus of battery: " + BatteryStatus + "\nDrone status:" +
                 DroneStatus + "\nLocation of drone: " + LocationOfDrone + "\nPackage Number:" + PackageNumber + "\n";
        }
    }
}
