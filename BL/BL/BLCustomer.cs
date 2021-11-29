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
            IDAL.DO.Customer dalCus;
            Customer blCus;

            try
            {
                dalCus = dal.GetCustomer(customerId);
                blCus = GetCustomer(customerId);

            }
            catch (IDAL.NoNumberFoundException ex)
            {
                throw new IBL.NoNumberFoundException("Customer ID not found", ex);
            }

            if (name != "")
                dalCus.Name = name;
            if (phone != "")
                dalCus.Phone = phone;
            

            dal.UpdateCustomer(dalCus);

        }

        public Customer GetCustomer(int customerId)
        {
            try {

                var DoCustomer = dal.GetCustomer(customerId);
                

                Customer BoCustomer = new()
                {
                    Id = DoCustomer.Id,
                    Name = DoCustomer.Name,
                    Phone = DoCustomer.Phone,
                    CustomerLocation = new Location { Lattitude = DoCustomer.Lattitude, Longitude = DoCustomer.Longitude },
                    PackageAtCustomerFromCustomer = new(),
                    PackageAtCustomerToCustomer = new()                       
                };

                foreach (var pck in dal.GetPackages(x => x.SenderId == customerId))
                    BoCustomer.PackageAtCustomerFromCustomer.Add(new()
                    {
                        PackageId = pck.Id,
                        Weight = (IBL.BO.Weight)pck.Weight,
                        Priority = (IBL.BO.Priorities)pck.Priority,
                        Status = GetPackages(x => x.Id == pck.Id).ToList()[0].PackageStatus,
                        OtherSideCustomer = new()
                        {
                            CustomerId = pck.TargetId,
                            CustomerName = dal.GetCustomer(pck.TargetId).Name
                        }
                    });

                foreach (var pck in dal.GetPackages(x => x.TargetId == customerId))
                    BoCustomer.PackageAtCustomerToCustomer.Add(new()
                    {
                        PackageId = pck.Id,
                        Weight = (IBL.BO.Weight)pck.Weight,
                        Priority = (IBL.BO.Priorities)pck.Priority,
                        Status = GetPackages(x => x.Id == pck.Id).ToList()[0].PackageStatus,
                        OtherSideCustomer = new()
                        {
                            CustomerId = pck.SenderId,
                            CustomerName = dal.GetCustomer(pck.SenderId).Name
                        }
                    });

                return BoCustomer;
            }
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
