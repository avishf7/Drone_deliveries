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
            catch (IDAL.ExistsNumberException ex)
            {
                throw new IBL.ExistsNumberException("Package already exists ", ex);
            }
        }
        public void UpdatePackage(PackageToList Package)
        {
            catch (IDAL.NoNumberFoundException ex)
            {
                throw new IBL.NoNumberFoundException("Package ID not found", ex);
            }
        }

        public Package GetPackage(int packageId)
        {
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
                    SenderId = item.SenderId,
                    TargetId = item.TargetId,
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
