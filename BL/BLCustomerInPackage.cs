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
    public partial class BL : IBl
    {
        public CustomerInPackage GetCusomerInPackage(int customerId)
        {
            var DoCus = dal.GetCustomer(customerId);

            CustomerInPackage BoCustomerInPackage = new()
            {
                CustomerId = DoCus.Id,
                CustomerName = DoCus.Name
            };
            return BoCustomerInPackage;
        }
    }
}
