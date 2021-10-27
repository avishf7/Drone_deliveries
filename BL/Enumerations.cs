using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Enumerations
    {
        /// <summary>
        /// Weight types
        /// </summary>
        public enum Weight { LIGHT, MEDIUM, HEAVY }
        /// <summary>
        /// Situations where the drone can be
        /// </summary>
        public enum DroneStatuses { AVAILABLE, MAINTENANCE, DELIVERY }
        /// <summary>
        /// Delivery priority
        /// </summary>
        public enum Priorities { NORMAL, FAST, EMERENCY }
    }
}
