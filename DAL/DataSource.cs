using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace Dal
{
    /// <summary>
    /// A class that stores the data of all objects.
    /// </summary>
    static class DataSource
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
        static string[] stationNames = new string[2] { "VIRTA Charging Station", "EV-Edge Charging Station" };
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
            /// Power consumption in percent per km when the drone is empty
            /// </summary>
            internal static double DroneAvailable = 1;
            /// <summary>
            /// Power consumption in percent per km when the glider carries light delivery
            /// </summary>
            internal static double LightWeight = 1.5;
            /// <summary>
            /// Power consumption in percent per km when the glider carries medium shipping
            /// </summary>
            internal static double MediumWeight = 2;
            /// <summary>
            /// Power consumption in percent per km when the glider carries heavy shipping
            /// </summary>
            internal static double HeavyWeight = 2.5;
            /// <summary>
            /// Percentage of skimmer charge per hour
            /// </summary>
            internal static double ChargingRate = 10000;

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
                    Id = rand.Next(i * 10, (i + 1) * 10),
                    MaxWeight = Weight.Heavy,
                    Model = models[rand.Next(3)]
                });

            }

            for (int i = 0; i < 2; i++)
            {
                stations.Add(new()
                {
                    Id = rand.Next(i * 10, (i + 1) * 10),
                    Name = stationNames[i],
                    FreeChargeSlots = 4,
                    Lattitude = 32 + rand.NextDouble() / 4,
                    Longitude = 34.5 + rand.NextDouble() / 4
                });
            }


            for (int i = 0; i < 10; i++)
            {
                customers.Add(new()
                {
                    Id = rand.Next(i * 10, (i + 1) * 10),
                    Name = customerNames[rand.Next(10)],
                    Phone = phones[i],
                    Lattitude = 32 + rand.NextDouble() / 4,
                    Longitude = 34.5 + rand.NextDouble() / 4
                });
            }


            //################### Add 10 diffrent packages #######################

            for (int i = 0; i < 5; i++)
            {
                packages.Add(new()
                {
                    Id = i  + 1,
                    SenderId = customers[rand.Next(10)].Id,
                    TargetId = customers[rand.Next(10)].Id,
                    Weight = (Weight)rand.Next(3),
                    Priority = (Priorities)rand.Next(3),
                    Requested = DateTime.Now,
                    Scheduled = null,
                    PickedUp = null,
                    Delivered = null,
                    DroneId = -1
                });
            }

            for (int i = 5; i < 7; i++)
            {
                packages.Add(new()
                {
                    Id = i + 1,
                    SenderId = customers[rand.Next(10)].Id,
                    TargetId = customers[rand.Next(10)].Id,
                    Weight = (Weight)rand.Next(3),
                    Priority = (Priorities)rand.Next(3),
                    Requested = DateTime.Now.AddHours(-3),
                    Scheduled = DateTime.Now,
                    PickedUp = null,
                    Delivered = null,
                    DroneId = dronesList[i - 5].Id
                });
            }

            for (int i = 7; i < 9; i++)
            {
                packages.Add(new()
                {
                    Id = i + 1,
                    SenderId = customers[rand.Next(10)].Id,
                    TargetId = customers[rand.Next(10)].Id,
                    Weight = (Weight)rand.Next(3),
                    Priority = (Priorities)rand.Next(3),
                    Requested = DateTime.Now.AddHours(-6),
                    Scheduled = DateTime.Now.AddHours(-3),
                    PickedUp = DateTime.Now,
                    Delivered = null,
                    DroneId = dronesList[i - 5].Id
                });
            }

            packages.Add(new()
            {
                Id = 10,
                SenderId = customers[rand.Next(10)].Id,
                TargetId = customers[rand.Next(10)].Id,
                Weight = (Weight)rand.Next(3),
                Priority = (Priorities)rand.Next(3),
                Requested = DateTime.Now.AddHours(-9),
                Scheduled = DateTime.Now.AddHours(-6),
                PickedUp = DateTime.Now.AddHours(-3),
                Delivered = DateTime.Now,
                DroneId = -1
            });

            //####################################################################

            Config.PackageIdCounter = 11;


            XmlTools.SaveListToXMLSerializer(dronesList, @"DroneXml.xml");
            XmlTools.SaveListToXMLSerializer(stations, @"StationXml.xml");
            XmlTools.SaveListToXMLSerializer(customers, @"CustomerXml.xml");
            XmlTools.SaveListToXMLSerializer(packages, @"PackageXml.xml");
        }

    }

}
