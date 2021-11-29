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
        public void AddPackage(Package package, int senderId, int targetId)
        {
            try
            {
                dal.AddPackage(new IDAL.DO.Package
                {
                    SenderId = senderId,
                    TargetId = targetId,
                    Weight = (IDAL.DO.Weight)package.Weight,
                    Priority = (IDAL.DO.Priorities)package.Priority,
                    DroneId = 0,
                    Requested = DateTime.Now,
                    Scheduled = DateTime.MinValue,
                    PickedUp = DateTime.MinValue,
                    Delivered = DateTime.MinValue
                }); 
            }
            catch (IDAL.ExistsNumberException ex)
            {
                throw new IBL.ExistsNumberException("Package already exists ", ex);
            }
        }

        public Package GetPackage(int packageId)
        {
            try
            {
                var DoPackage = dal.GetPackage(packageId);

                var dr = DroneLists.Find(x => x.PackageNumber == packageId);

                DroneInPackage droneInPackage = new()
                {
                    Id = dr.Id,
                    BatteryStatus = dr.BatteryStatus,
                   LocationOfDrone = dr.LocationOfDrone
                };




                Package BoPackage = new()
                {
                    Id = DoPackage.Id,         
                    Weight = (Weight)DoPackage.Weight,
                    Priority = (Priorities)DoPackage.Priority,
                    Scheduled = DoPackage.Scheduled,
                    Requested = DoPackage.Requested,
                    PickedUp = DoPackage.PickedUp,
                    Delivered = DoPackage.Delivered,
                    droneInPackage = droneInPackage
                };

               BoPackage.SenderCustomerInPackage =GetCusomerInPackage(DoPackage.SenderId);
                BoPackage.TargetCustomerInPackage = GetCusomerInPackage(DoPackage.TargetId);



                return BoPackage;

            }
            catch (IDAL.NoNumberFoundException ex)
            {
                throw new IBL.NoNumberFoundException("Package ID not found", ex);
            }


            throw new NotImplementedException();
        }

        public IEnumerable<PackageToList> GetPackages(Predicate<PackageToList> predicate = null)
        {
            List<IDAL.DO.Package> DoPackage = (List<IDAL.DO.Package>)dal.GetPackages();
            List<PackageToList> BoPackageToLists = new();

            foreach (var item in DoPackage)
            {
                PackageStatus p=0;
                if (item.Delivered != DateTime.MinValue)
                {
                    p  = PackageStatus.PROVIDED;
                }
                if (item.PickedUp != DateTime.MinValue)
                {
                    p  = PackageStatus.COLLECTED;
                }
                if (item.Scheduled != DateTime.MinValue)
                {
                     p = PackageStatus.ASSOCIATED;
                }
                if (item.Requested != DateTime.MinValue)
                {
                     p = PackageStatus.DEFINED;
                }

                PackageToList PckToLists = new()
                {
                    Id = item.Id,
                    SenderName = dal.GetCustomer(item.SenderId).Name,
                    TargetName = dal.GetCustomer(item.TargetId).Name,
                    Priority = (Priorities)item.Priority,
                    Weight = (Weight)item.Weight,
                    PackageStatus = p
                };
                
               

            
            if (predicate != null ? predicate(PckToLists) : true)
                    BoPackageToLists.Add(PckToLists);

            }
            return BoPackageToLists;

        }

        public void DeletePackage(int id)
        {
            throw new NotImplementedException();
        }
    }
}
