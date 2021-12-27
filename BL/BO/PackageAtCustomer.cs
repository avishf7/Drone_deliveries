using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class PackageAtCustomer
    {
        /// <summary>
        /// Gets customer id
        /// </summary>
        public int PackageId { get; set; }

        /// <summary>
        /// Gets the weight.
        /// </summary>
        public Weight Weight { get; set; }

        /// <summary>
        /// Gets the priority.
        /// </summary>
        public Priorities Priority { get; set; }

        /// <summary>
        /// Gets the status of package
        /// </summary>
        public PackageStatus Status { get; set; }

        /// <summary>
        /// Gets the other side of the package
        /// </summary>
        public CustomerInPackage OtherSideCustomer { get; set; }




        public override string ToString()
        {
            return "Details of ID:" + PackageId + "\nWeight: " + Weight + "\nPriority: " + Priority
                + "\nStatus: " + Status + "\nCustomer in package: " + OtherSideCustomer + "\n";
        }
    }
}
