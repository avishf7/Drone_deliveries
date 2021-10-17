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

        static Random rand=new Random();
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
            for (int i = 0; i < 5; i++)
            {
                drones.Add(new()
                {
                    Id = rand.Next(10),
                    MaxWeight = (Weight)rand.Next(3),
                    Model = "COMBO AIR 2S\n",
                    Status = (DroneStatuses)rand.Next(3),
                    Battery = 100
                });

            }
            for (int i = 0; i < 10; i++)
            {

            }
        }
    }
}
