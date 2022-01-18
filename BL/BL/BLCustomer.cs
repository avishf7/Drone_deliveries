using DalApi;
using BlApi;
using BO;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer customer)
        {

            try
            {
                lock (dal)
                {
                    dal.AddCustomer(new DO.Customer
                    {
                        Id = customer.Id,
                        Name = customer.Name,
                        Phone = customer.Phone,
                        Longitude = customer.CustomerLocation.Longitude,
                        Lattitude = customer.CustomerLocation.Lattitude
                    });
                }
            }
            catch (DalApi.ExistsNumberException ex)
            {
                throw new BlApi.NoNumberFoundException("Customer already exists", ex);
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(int customerId, string name, string phone)
        {
            lock (dal)
            {
                DO.Customer dalCus;
                Customer blCus;

                try
                {
                    dalCus = dal.GetCustomer(customerId);
                    blCus = GetCustomer(customerId);

                }
                catch (DalApi.NoNumberFoundException ex)
                {
                    throw new BlApi.NoNumberFoundException("Customer ID not found", ex);
                }

                if (name != "")
                    dalCus.Name = name;
                if (phone != "")
                    dalCus.Phone = phone;


                dal.UpdateCustomer(dalCus);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int customerId)
        {
            try
            {
                lock (dal)
                {
                    var DoCustomer = dal.GetCustomer(customerId);


                    Customer BoCustomer = new()
                    {
                        Id = DoCustomer.Id,
                        Name = DoCustomer.Name,
                        Phone = DoCustomer.Phone,
                        CustomerLocation = new Location { Lattitude = DoCustomer.Lattitude, Longitude = DoCustomer.Longitude },
                        PackageAtCustomerFromCustomer = dal.GetPackages(x => x.SenderId == customerId).Select(pck => new PackageAtCustomer()
                        {
                            PackageId = pck.Id,
                            Weight = (BO.Weight)pck.Weight,
                            Priority = (BO.Priorities)pck.Priority,
                            Status = GetPackages(x => x.Id == pck.Id).Single().PackageStatus,
                            OtherSideCustomer = dal.GetCustomer(pck.TargetId).GetCusomerInPackage()
                        }),
                        PackageAtCustomerToCustomer = dal.GetPackages(x => x.TargetId == customerId).Select(pck => new PackageAtCustomer()
                        {
                            PackageId = pck.Id,
                            Weight = (BO.Weight)pck.Weight,
                            Priority = (BO.Priorities)pck.Priority,
                            Status = GetPackages(x => x.Id == pck.Id).Single().PackageStatus,
                            OtherSideCustomer = dal.GetCustomer(pck.SenderId).GetCusomerInPackage()
                        })
                    };

                    return BoCustomer;
                }
            }
            catch (DalApi.NoNumberFoundException ex)
            {
                throw new BlApi.NoNumberFoundException("Customer ID not found", ex);
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> GetCustomers(Predicate<CustomerToList> predicate = null)
        {
            lock (dal)
            {
                return dal.GetCustomers().Select(cus => GetCustomer(cus.Id))
                                     .Select(cus => new CustomerToList()
                                     {
                                         CustomerId = cus.Id,
                                         CustomerName = cus.Name,
                                         CustomerPhone = cus.Phone,
                                         NumOfPackagesNotProvided = (from pck in cus.PackageAtCustomerFromCustomer
                                                                     where pck.Status != PackageStatus.Provided
                                                                     select pck).Count(),
                                         NumOfPackagesProvided = (from pck in cus.PackageAtCustomerFromCustomer
                                                                  where pck.Status == PackageStatus.Provided
                                                                  select pck).Count(),
                                         NumOfPackagesReceived = (from pck in cus.PackageAtCustomerToCustomer
                                                                  where pck.Status == PackageStatus.Provided
                                                                  select pck).Count(),
                                         NumOfPackagesNotReceived = (from pck in cus.PackageAtCustomerToCustomer
                                                                     where pck.Status != PackageStatus.Provided
                                                                     select pck).Count(),
                                     }).Where(cus => predicate != null ? predicate(cus) : true);
            }
        }
    }
}
