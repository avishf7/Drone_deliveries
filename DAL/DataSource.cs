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
        static Random rand = new Random();
        static string[] customerNames = new string[10] { "Shlomi", "Meir", "Miki", "Shalom", "Dani", "Avishay", "Chaim", "Moti", "Shimon", "Bibi" };
        static string[] phones = new string[10] { "0546273849", "0546223849", "0546211849", "0546413849", "0546254849", "0546273878", "0547273849", "0546273749", "0546277849", "0546273847" };
        static string[] stationNames = new string[4] { "Electric Charging Station", "Gnrgy Charging Station", "VIRTA Charging Station", "EV-Edge Charging Station" };
        static string[] models = new string[3] { "MAVIC MINI 2", "Mavic Air 2", "COMBO AIR 2S" };


        internal static List<Drone> drones = new();
        internal static List<Station> stations = new();
        internal static List<Package> packages = new();
        internal static List<Customer> customers = new();
        /// <summary>
        /// 
        /// </summary>
        internal class Config
        {
            internal int PackageIdCounter;

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
                    Scheduled = DateTime.Now.AddDays(rand.Next(3)),
                    PickedUp = DateTime.Now.AddHours(rand.Next(3)),
                    Delivered = DateTime.Now.AddHours(rand.Next(4)),
                    DroneId = GetAvailableDrone().Id

                });
            }

                    DroneId = drones[rand.Next(5)].Id

                });
            }

            static Drone GetAvailableDrone()
            {
                Drone drone;
                do
                {
                    drone = drones[rand.Next(5)];
                } while (drone.Status != DroneStatuses.AVAILABLE);
            }


        }
    }
  
}
