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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            try
            {
                dal.AddCustomer(new IDAL.DO.Customer
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    Longitude = customer.CustomerLocation.Longitude,
                    Lattitude = customer.CustomerLocation.Lattitude

                });
            }
            catch (IBL.ExistsNumberException ex)
            {

                throw new IBL.ExistsNumberException("ERROR: ", ex);
            }
        }

        public void UpdateCustomer(CustomerToList customer)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerToList> GetCustomers(Predicate<Customer> predicate = null)
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
