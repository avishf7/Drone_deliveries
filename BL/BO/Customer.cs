using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
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
        /// Gets a list of customer in package from the customer
        /// </summary>
        public List<CustomerInPackage> CustomerInPackageFromCustomer { get; set; }

        /// <summary>
        /// Gets a list of customer in package to the customer
        /// </summary>
        public List<CustomerInPackage> CustomerInPackageToCustomer { get; set; }




        public override string ToString()
        {
            return "Details of ID :" + Id + "\nName:" + Name + "\nPhone:" +
                Phone + "\nLocation: "+ CustomerLocation+ "\nCustomer in package from customer: " +
                string.Join(" " , CustomerInPackageFromCustomer) + "\nCustomer in package to customer: " + 
                string.Join(" " , CustomerInPackageToCustomer) + "\n";
        }
    }
}
