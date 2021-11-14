using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class PackageAtCustomer
    {
        /// <summary>
        /// Gets customer id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// Gets the weight.
        /// </summary>
        public Weight Weight { get; set; }
        /// <summary>
        /// Gets the priority.
        /// </summary>
        public Priorities Priority { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PackageStatus status { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Details of ID:" + CustomerId + "\nWeight: " + Weight + "\nPriority: " + Priority
                + "\nStatus: " + status + "\n";
        }
    }
}
