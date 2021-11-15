using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Drone
    {
        /// <summary>
        /// gets the id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// gets the model.
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// gets the max weight.
        /// </summary>
        public Weight MaxWeight { get; set; }
        /// <summary>
        /// Gets the drone's battery stutus;
        /// </summary>
        public double BatteryStatus { get; set; }
        /// <summary>
        /// Gets number of delivery in progress
        /// </summary>
        PackageInTransfer DeliveryInProgress;
        /// <summary>
        /// 
        /// </summary>
        Location LocationOfDrone;


        public override string ToString()
        {
            return "Details of Id :" + Id + "\nModel: " + Model + "\nMax weight: " +
                 MaxWeight + "\nStatus of battery: " + BatteryStatus + "\nDeliver in progress: " + DeliveryInProgress +
                 "\nCurrent location of drone: " + LocationOfDrone +"\n";
        }

    }
}



