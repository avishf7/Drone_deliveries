using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneInPackage
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double BatteryStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        Location LocationOfDrone;
        public override string ToString()
        {
            return "Details of Id :" + Id + "\nStatus of battery: " + BatteryStatus + "\nLocation of drone: " 
                + LocationOfDrone +"\n";
        }
    }
}
