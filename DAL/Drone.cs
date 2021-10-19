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
        /// 
        /// </summary>
        public struct Drone
        {
            /// <summary>
            /// 
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Model { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Weight MaxWeight { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public DroneStatuses Status { get; set; }
            /// <summary>
            /// 
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
