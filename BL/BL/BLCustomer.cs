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

        public void UpdateCustomer(int customerId, string name, string phone)
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
                        OtherSideCustomer = dal.GetCustomer(pck.TargetId).GetCusomerInPackage()
                    });

                foreach (var pck in dal.GetPackages(x => x.TargetId == customerId))
                    BoCustomer.PackageAtCustomerToCustomer.Add(new()
                    {
                        PackageId = pck.Id,
                        Weight = (IBL.BO.Weight)pck.Weight,
                        Priority = (IBL.BO.Priorities)pck.Priority,
                        Status = GetPackages(x => x.Id == pck.Id).ToList()[0].PackageStatus,
                        OtherSideCustomer = dal.GetCustomer(pck.SenderId).GetCusomerInPackage()
                    });

                return BoCustomer;
            }
            catch (IDAL.NoNumberFoundException ex)
            {
                throw new IBL.NoNumberFoundException("Customer ID not found", ex);
            }

        }

        public IEnumerable<CustomerToList> GetCustomers(Predicate<CustomerToList> predicate = null)
        {
            var boCustomers = dal.GetCustomers().Select(cus => GetCustomer(cus.Id));
            List<CustomerToList> boCustomersToList = new();

            foreach (var cus in boCustomers)
            {
                CustomerToList customerToList = new()
                {
                    CustomerId = cus.Id,
                    CustomerName = cus.Name,
                    CustomerPhone = cus.Phone,
                    NumOfPackagesNotProvided = (from pck in cus.PackageAtCustomerFromCustomer
                                                where pck.Status != PackageStatus.PROVIDED
                                                select pck).Count(),
                    NumOfPackagesProvided = (from pck in cus.PackageAtCustomerFromCustomer
                                             where pck.Status == PackageStatus.PROVIDED
                                             select pck).Count(),
                    NumOfPackagesReceived = (from pck in cus.PackageAtCustomerToCustomer
                                             where pck.Status == PackageStatus.PROVIDED
                                             select pck).Count(),
                    NumOfPackagesNotReceived = (from pck in cus.PackageAtCustomerToCustomer
                                            where pck.Status != PackageStatus.PROVIDED
                                            select pck).Count(),
                };

                if (predicate != null ? predicate(customerToList) : true)
                    boCustomersToList.Add(customerToList);
            }

            return boCustomersToList;
        }

        public void DeleteCustomer(int id)
        {
            try
            {
                dal.DeleteCustomer(id);
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
