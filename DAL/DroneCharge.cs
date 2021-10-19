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
        public struct DroneCharge
        {
            /// <summary>
            /// 
            /// </summary>
            public int DroneId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int StationId { get; set; }

            public override string ToString()
            {
                return "Details of DroneCharge: " + "\nDrone ID: " + DroneId + "\nStation ID: "
                    + StationId + "\n";
            }
        }
    }
}
