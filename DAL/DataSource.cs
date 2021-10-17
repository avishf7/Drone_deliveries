using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    class DataSource
    {
        internal static List<Drone> drones = new();
        internal static List<Station> stations = new();
        internal static List<Package> packages = new();
        internal static List<Customer> customers = new();

        class config
        {
            internal int PackageIdCounter;
        }

        internal static void Initialize()
        {

        }
    }
}
