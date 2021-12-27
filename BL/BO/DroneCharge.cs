using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneCharge
    {
        /// <summary>
        /// the droneId;
        /// </summary>
        public int DroneId { get; set; }

        /// <summary>
        /// drone's battery stutus;
        /// </summary>
        public double BatteryStatus { get; set; }



        public override string ToString()
        {
            return "Details of DroneCharge: " + "\nDrone ID: " + DroneId + "\nDrone's battery status: "
                + BatteryStatus + "\n";
        }
    }
}

