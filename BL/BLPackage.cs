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
        public void AddPackage(PackageToList package)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void DeletePackage(int id)
        {
            throw new NotImplementedException();
        }
    }
}
