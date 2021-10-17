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
        static string[] customerNames = new string[10] { "Shlomi", "Meir", "Miki", "Shalom", "Dani", "Avishay", "Chaim", "Moti", "Shimon", "Bibi" };
        static string[] phones = new string[10] { "0546273849", "0546223849", "0546211849", "0546413849", "0546254849", "0546273878", "0547273849", "0546273749", "0546277849", "0546273847" };
        static string[] stationNames = new string[4] { "Electric Charging Station", "Gnrgy Charging Station", "VIRTA Charging Station", "EV-Edge Charging Station"};
        static string[] models = new string[3] { "MAVIC MINI 2", "Mavic Air 2", "COMBO AIR 2S"};


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
                    Model = models[rand.Next(3)],
                    Status = (DroneStatuses)rand.Next(3),
                    Battery = 90
                }) ;

            }
            for (int i = 0; i < 10; i++)
            {

            }
        }
    }
}
