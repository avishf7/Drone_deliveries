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
            catch (IDAL.ExistsNumberException ex)
            {
                throw new IBL.NoNumberFoundException("Customer already exists", ex);
            }
        }

        public void UpdateCustomer(int customerId,string name, string phone)
        {
            try { }
            catch (IDAL.NoNumberFoundException ex)
            {
                throw new IBL.NoNumberFoundException("Customer ID not found", ex);
            }
        }

        public Customer GetCustomer(int customerId)
        {
            try { }
            catch (IDAL.NoNumberFoundException ex)
            {
                throw new IBL.NoNumberFoundException("Customer ID not found", ex);
            }


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
