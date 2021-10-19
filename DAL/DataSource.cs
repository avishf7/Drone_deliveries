using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    /// <summary>
    /// 
    /// </summary>
     class DataSource
    {
        /// <summary>
        /// 
        /// </summary>
        static Random rand = new Random();
        /// <summary>
        /// 
        /// </summary>
        static string[] customerNames = new string[10] { "Shlomi", "Meir", "Miki", "Shalom", "Dani", "Avishay", "Chaim", "Moti", "Shimon", "Bibi" };
        /// <summary>
        /// 
        /// </summary>
        static string[] phones = new string[10] { "0546273849", "0546223849", "0546211849", "0546413849", "0546254849", "0546273878", "0547273849", "0546273749", "0546277849", "0546273847" };
        /// <summary>
        /// 
        /// </summary>
        static string[] stationNames = new string[4] { "Electric Charging Station", "Gnrgy Charging Station", "VIRTA Charging Station", "EV-Edge Charging Station" };
        /// <summary>
        /// 
        /// </summary>
        static string[] models = new string[3] { "MAVIC MINI 2", "Mavic Air 2", "COMBO AIR 2S" };

        /// <summary>
        /// 
        /// </summary>
        internal static List<Drone> drones = new();
        /// <summary>
        /// 
        /// </summary>
        internal static List<Station> stations = new();
        /// <summary>
        /// 
        /// </summary>
        internal static List<Package> packages = new();
        /// <summary>
        /// 
        /// </summary>
        internal static List<Customer> customers = new();

        /// <summary>
        /// 
        /// </summary>
        internal class Config
        {
            /// <summary>
            /// 
            /// </summary>
            internal static int PackageIdCounter;

        }

        /// <summary>
        /// 
        /// </summary>
        internal static void Initialize()
        {

            for (int i = 0; i < 5; i++)
            {
                drones.Add(new()
                {
                    Id = rand.Next(10),
                    MaxWeight = Weight.HEAVY,
                    Model = models[rand.Next(3)],
                    Status = (DroneStatuses)rand.Next(3),
                    Battery = 90
                });

            }

            for (int i = 0; i < 2; i++)
            {
                stations.Add(new()
                {
                    Id = rand.Next(10),
                    Name = customerNames[rand.Next(10)],
                    FreeChargeSlots = rand.Next(4),
                    Longitude = 32 + rand.NextDouble(),
                    Lattitude = 35 + rand.NextDouble(),
                });
            }


            for (int i = 0; i < 10; i++)
            {
                customers.Add(new()
                {
                    Id = rand.Next(10),
                    Name = customerNames[rand.Next(10)],
                    Phone = phones[rand.Next(10)],
                    Longitude = 32 + rand.NextDouble(),
                    Lattitude = 35 + rand.NextDouble(),
                });
            }

            for (int i = 0; i < 10; i++)
            {
                packages.Add(new()
                {
                    Id = rand.Next(10),
                    SenderId = customers[rand.Next(10)].Id,
                    TargetId = customers[rand.Next(10)].Id,
                    Weight = (Weight)rand.Next(3),
                    Priority = (Priorities)rand.Next(3),
                    Requested = DateTime.Now,
                    Scheduled = DateTime.Now.AddDays(3),
                    PickedUp = DateTime.Now.AddDays(3).AddHours(3),
                    Delivered = DateTime.Now.AddDays(3).AddHours(3).AddHours(4),
                    DroneId = GetAvailableDrone().Id

                });

                Config.PackageIdCounter = packages.Max(pck => pck.Id) + 1;
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static Drone GetAvailableDrone()
        {
            Drone drone;
            do
            {
                drone = drones[rand.Next(5)];
            } while (drone.Status != DroneStatuses.AVAILABLE);

            return drone;
        }
    }
  
}
