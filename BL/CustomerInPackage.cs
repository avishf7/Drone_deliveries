using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class CustomerInPackage
    {
        /// <summary>
        /// Gets customer id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// Gets customer name
        /// </summary>
        public string CustomerName { get; set; }


        public override string ToString()
        {
            return "Details of customer in delivery:" + "\nCustomer id: " + CustomerId +
                "\nCustomer name: " + CustomerName + "\n";
        }
    }
}
