using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
 
    
        /// <summary>
        /// Weight types
        /// </summary>
        public enum Weight { Light, Medium, Heavy }

        /// <summary>
        /// Situations where the drone can be
        /// </summary>
        public enum DroneStatuses { Available , Maintenance , Sendering  }

        /// <summary>
        /// Delivery priority
        /// </summary>
        public enum Priorities { Normal, Fast, Emergency }    

        /// <summary>
        /// Status of package
        /// </summary>
        public enum PackageStatus { Defined, Associated, Collected, Provided }  
    
}
