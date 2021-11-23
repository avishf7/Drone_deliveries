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
        public void AddPackage(Package package)
        {
            try
            {
                dal.AddPackage(new IDAL.DO.Package
                {
                    SenderId = package.SenderCustomerInPackage,
                    TargetId = package.TargetCustomerInPackage,
                    Weight = (IDAL.DO.Weight)package.Weight,
                    Priority = (IDAL.DO.Priorities)package.Priority,
                    DroneId = 0,
                    Requested = DateTime.Now,
                    Scheduled = DateTime.MinValue,
                    PickedUp = DateTime.MinValue,
                    Delivered = DateTime.MinValue
                }); ;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdatePackage(PackageToList Package)
        {
            throw new NotImplementedException();
        }

        public Package GetPackage(int packageId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PackageToList> GetPackages(Predicate<Package> predicate = null)
        {
            List<IDAL.DO.Package> DoPackage = (List<IDAL.DO.Package>)dal.GetPackages(predicate);
            List<PackageToList> BoPpackageToLists = new();

            foreach (var item in DoPackage)
            {
                BoPpackageToLists.Add(new()
                {
                    Id = item.Id,
                    SenderId = item.SenderId,
                    TargetId = item.TargetId,
                    Priority = (Priorities)item.Priority,
                    Weight = (Weight)item.Weight,
                    PackageStatus =
                });
            }
            return BoPpackageToLists;

        }

        public void DeletePackage(int id)
        {
            throw new NotImplementedException();
        }
    }
}
