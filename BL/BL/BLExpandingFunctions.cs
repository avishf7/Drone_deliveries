using DalApi;
using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    static class BLExpandingFunctions
    {
        /// <summary>
        /// The function converts a "DO.Customer" object to a "CustomerInPackage" object
        /// </summary>
        /// <param name="customer">"DO.Customer" object</param>
        /// <returns>"CustomerInPackage" object</returns>
        public static CustomerInPackage GetCusomerInPackage(this DO.Customer customer)
        {
            CustomerInPackage BoCustomerInPackage = new()
            {
                CustomerId = customer.Id,
                CustomerName = customer.Name
            };

            return BoCustomerInPackage;
        }


    }
}
