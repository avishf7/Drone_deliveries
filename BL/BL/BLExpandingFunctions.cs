using IDAL;
using IBL;
using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    static class BLExpandingFunctions
    {
        public static CustomerInPackage GetCusomerInPackage(this IDAL.DO.Customer customer)
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
