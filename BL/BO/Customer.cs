using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi.BO
{
    public class Customer
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the phone.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets the location of the customer
        /// </summary>
        public Location CustomerLocation { get; set; }

        /// <summary>
        /// Gets a list of packages from the customer
        /// </summary>
        public List<PackageAtCustomer> PackageAtCustomerFromCustomer { get; set; }

        /// <summary>
        /// Gets a list of packages to the customer
        /// </summary>
        public List<PackageAtCustomer> PackageAtCustomerToCustomer { get; set; }




        public override string ToString()
        {
            return "Details of ID :" + Id + "\nName:" + Name + "\nPhone:" +
                Phone + "\nLocation: "+ CustomerLocation+ "\nCustomer in package from customer: " +
                string.Join(" " , PackageAtCustomerFromCustomer) + "\nCustomer in package to customer: " + 
                string.Join(" " , PackageAtCustomerToCustomer) + "\n";
        }
    }
}
