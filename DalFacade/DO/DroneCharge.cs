using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace DO
    {
        /// <summary>
        /// A structure containing the details of the charging station
        /// </summary>
        public struct DroneCharge
        {
            /// <summary>
            /// the droneId;
            /// </summary>
            public int DroneId { get; set; }
            /// <summary>
            /// the stationId;
            /// </summary>
            public int StationId { get; set; }
            /// <summary>
            /// Charging start time
            /// </summary>
            public DateTime? ChargeStart { get; set; }



            public override string ToString()
            {
                return "Details of DroneCharge: " + "\nDrone ID: " + DroneId + "\nStation ID: "
                    + StationId + "\nCharging start time: " + ChargeStart + "\n";
            }
        }
    }

