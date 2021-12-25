using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi.BO
{
    public class DroneInPackage
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets battery status
        /// </summary>
        public double BatteryStatus { get; set; }

        /// <summary>
        /// Gets location of drone
        /// </summary>
        public Location LocationOfDrone { get; set; }



        public override string ToString()
        {
            return "Details of Id :" + Id + "\nStatus of battery: " + BatteryStatus + "\nLocation of drone: " 
                + LocationOfDrone +"\n";
        }
    }
}
