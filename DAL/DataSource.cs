using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    /// <summary>
    /// A class that stores the data of all objects.
    /// </summary>
    class DataSource
    {
        /// <summary>
        /// Create a static random show.
        /// </summary>
        static Random rand = new Random();
        /// <summary>
        /// Create a static array that contains customer names
        /// </summary>
        static string[] customerNames = new string[10] { "Shlomi", "Meir", "Miki", "Shalom", "Dani", "Avishay", "Chaim", "Moti", "Shimon", "Bibi" };
        /// <summary>
        /// Create a static array that contains customers' phone numbers.
        /// </summary>
        static string[] phones = new string[10] { "0546273849", "0546223849", "0546211849", "0546413849", "0546254849", "0546273878", "0547273849", "0546273749", "0546277849", "0546273847" };
        /// <summary>
        /// Create a static array that contains stations name.
        /// </summary>
        static string[] stationNames = new string[4] { "Electric Charging Station", "Gnrgy Charging Station", "VIRTA Charging Station", "EV-Edge Charging Station" };
        /// <summary>
        ///  Create a static array that contains drone's model.
        /// </summary>
        static string[] models = new string[3] { "MAVIC MINI 2", "Mavic Air 2", "COMBO AIR 2S" };

        /// <summary>
        /// Create a static drone list. 
        /// </summary>
        internal static List<Drone> dronesList = new();
        /// <summary>
        /// Create a static station list.
        /// </summary>
        internal static List<Station> stations = new();
        /// <summary>
        /// Create a static package list.
        /// </summary>
        internal static List<Package> packages = new();
        /// <summary>
        /// Create a static Customer list.
        /// </summary>
        internal static List<Customer> customers = new();
        /// <summary>
        /// Set up a list of charging stations.
        /// </summary>
        internal static List<DroneCharge> droneCharges = new();

        /// <summary>
        /// A class containing static fields to indicate the first free element in each list.
        /// </summary>
        internal class Config
        {
            /// <summary>
            /// Set up a static function with internal permission to make a running code for a package.
            /// </summary>
            internal static int PackageIdCounter;
            /// <summary>
            /// 
            /// </summary>
            internal static double DroneAvailable;
            /// <summary>
            /// 
            /// </summary>
            internal static double LightWeight;
            /// <summary>
            /// 
            /// </summary>
            internal static double MediumWeight;
            /// <summary>
            /// 
            /// </summary>
            internal static double HeavyWeight;
            /// <summary>
            /// 
            /// </summary>
            internal static double ChargingRate;

        }

        /// <summary>
        /// Enter data for all the structures we defined in CS.
        /// </summary>
        internal static void Initialize()
        {

            for (int i = 0; i < 5; i++)
            {
                dronesList.Add(new()
                {
                    Id = rand.Next(10),
                    MaxWeight = Weight.HEAVY,
                    Model = models[rand.Next(3)]
                });

            }

            for (int i = 0; i < 2; i++)
            {
                stations.Add(new()
                {
                    Id = rand.Next(10),
                    Name = stationNames[rand.Next(4)],
                    FreeChargeSlots = rand.Next(4),
                    Lattitude = 32 + rand.NextDouble(),
                    Longitude = 35 + rand.NextDouble(),
                });
            }


            for (int i = 0; i < 10; i++)
            {
                customers.Add(new()
                {
                    Id = rand.Next(10),
                    Name = customerNames[rand.Next(10)],
                    Phone = phones[rand.Next(10)],
                    Lattitude = 32 + rand.NextDouble(),
                    Longitude = 35 + rand.NextDouble(),
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
            }

                Config.PackageIdCounter = packages.Max(pck => pck.Id) + 1;
        }

        /// <summary>
        /// Looking for a free drone.
        /// </summary>
        /// <returns>Available drone<drone/returns>
       private static Drone GetAvailableDrone()
        {
            Drone drone;
           // do
            //{
                drone = dronesList[rand.Next(5)];
            //} //while (drone.Status != DroneStatuses.AVAILABLE);

            return drone;
        }
    }
  
}
