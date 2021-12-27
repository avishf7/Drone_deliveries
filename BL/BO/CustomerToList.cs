using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CustomerToList
    {
        /// <summary>
        /// Gets customer id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets customer name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets customer phone
        /// </summary>
        public string CustomerPhone { get; set; }

        /// <summary>
        /// Gets number of packages provided
        /// </summary>
        public int NumOfPackagesProvided { get; set; }

        /// <summary>
        /// Gets Number of packages not provided
        /// </summary>
        public int NumOfPackagesNotProvided { get; set; }

        /// <summary>
        /// Gets number of packages received
        /// </summary>
        public int NumOfPackagesReceived { get; set; }

        /// <summary>
        /// Gets several packages on way
        /// </summary>
        public int NumOfPackagesNotReceived { get; set; }


        public override string ToString()
        {
            return "Details customer to list" + "\nCustomer id: " + CustomerId +
                "\nCustomer name: " + CustomerName + "\nCustomer phone: " + CustomerPhone +
                "\nNumber of packages provided: " + NumOfPackagesProvided + "\nNumber of packages that not provided: " +
               NumOfPackagesNotProvided + "\nNumber of packages received: " + NumOfPackagesReceived +
               "\nNumber of packages that not received: " + NumOfPackagesNotReceived + "\n";
        }
    }
}
