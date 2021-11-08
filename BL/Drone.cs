using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enumerations;

namespace IBL.BO
{
    class Drone
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
        public int NumberOfDeliveryInProgress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double CurrentLocationOfDrone { get; set; }



        public override string ToString()
        {
            return "Details of Id :" + Id + "\nModel: " + Model + "\nMax weight: " +
                 MaxWeight + "\nStatus of battery: " + BatteryStatus + "\nDeliver in progress: " + NumberOfDeliveryInProgress+
                 "\nCurrent location of drone: " + CurrentLocationOfDrone +"\n";
        }

    }
}



