using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneCharge
    {
        /// <summary>
        /// Gets the droneId;
        /// </summary>
        public int DroneId { get; set; }

        /// <summary>
        /// Gets the drone's battery stutus;
        /// </summary>
        public double BatteryStatus { get; set; }

        public override string ToString()
        {
            return "Details of DroneCharge: " + "\nDrone ID: " + DroneId + "\nDrone's battery stutus: "
                + BatteryStatus + "\n";
        }
    }
}

