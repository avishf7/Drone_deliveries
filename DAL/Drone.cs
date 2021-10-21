using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        /// <summary>
        ///  A structure containing the details of the drone.
        /// </summary>
        public struct Drone
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
            /// gets the status.
            /// </summary>
            public DroneStatuses Status { get; set; }
            /// <summary>
            /// gets the battery.
            /// </summary>
            public double Battery { get; set; }

            public override string ToString()
            {
                return "Details of Id :" + Id + "\nModel: " + Model + "\nMax weight: " +
                     MaxWeight + "\nDrone statuses: " + Status + "\nBattery of drone: "
                     + Battery + "\n";
            }
        }
    }
}
