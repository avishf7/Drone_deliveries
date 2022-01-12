using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using System.Runtime.CompilerServices;


namespace Dal
{
    sealed class DalXml : IDal
    {
        public static DalXml Instance { get; } = new DalXml();

        private DalXml() { }

        #region Drone functions

        public void AddDrone(Drone drone)
        {
            List<Drone> dronesList = XmlTools.LoadListFromXMLSerializer<Drone>(@"DroneXml.xml");

            if (dronesList.Exists(x => x.Id == drone.Id))
            {
                throw new ExistsNumberException();
            }
            dronesList.Add(drone);
          
            XmlTools.SaveListToXMLSerializer(dronesList, @"DroneXml.xml");
        }

        public void UpdateDrone(Drone drone)
        {
            List<Drone> dronesList = XmlTools.LoadListFromXMLSerializer<Drone>(@"DroneXml.xml");

            int iU = dronesList.FindIndex(dr => dr.Id == drone.Id);
            if (iU == -1)
            {
                throw new NoNumberFoundException(" ");
            }

            dronesList.Insert(iU, drone);

            XmlTools.SaveListToXMLSerializer(dronesList, @"DroneXml.xml");
        }


        public Drone GetDrone(int droneId)
        {
            List<Drone> dronesList = XmlTools.LoadListFromXMLSerializer<Drone>(@"DroneXml.xml");

            if (!dronesList.Exists(x => x.Id == droneId))
            {
                throw new NoNumberFoundException(" ");
            }

            return dronesList.First(dr => dr.Id == droneId);
        }

        public IEnumerable<Drone> GetDrones(Predicate<Drone> predicate = null)
        {
            List<Drone> dronesList = XmlTools.LoadListFromXMLSerializer<Drone>(@"DroneXml.xml");
            return dronesList.Where(i => predicate == null ? true : predicate(i));
        }

        #endregion

        #region Station functions

        public void AddStation(Station station)
        {
            List<Station> Stations = XmlTools.LoadListFromXMLSerializer<Station>(@"StationXml.xml");
            if (Stations.Exists(x => x.Id == station.Id))
            {
                throw new ExistsNumberException();
            }
            Stations.Add(station);

            XmlTools.SaveListToXMLSerializer(Stations, @"StationXml.xml");
        }


        public void UpdateStation(Station station)
        {
            List<Station> Stations = XmlTools.LoadListFromXMLSerializer<Station>(@"StationXml.xml");
            if (!Stations.Exists(x => x.Id == station.Id))
            {
                throw new NoNumberFoundException();
            }
            int iU = Stations.FindIndex(st => st.Id == station.Id);
            Stations.Insert(iU, station);

            XmlTools.SaveListToXMLSerializer(Stations, @"StationXml.xml");
        }


        public Station GetStation(int stationId)
        {
            List<Station> Stations = XmlTools.LoadListFromXMLSerializer<Station>(@"StationXml.xml");

            if (!Stations.Exists(x => x.Id == stationId))
            {
                throw new NoNumberFoundException();
            }
            return Stations.First(st => st.Id == stationId);
        }


        public IEnumerable<Station> GetStations(Predicate<Station> predicate = null)
        {
            List<Station> Stations = XmlTools.LoadListFromXMLSerializer<Station>(@"StationXml.xml");
            return Stations.Where(i => predicate == null ? true : predicate(i));
        }


        public void UsingChargingStation(int stationId)
        {
            List<Station> Stations = XmlTools.LoadListFromXMLSerializer<Station>(@"StationXml.xml");

            Station station = GetStation(stationId);
            int indexStation = Stations.FindIndex(st => st.Id == stationId);

            station.FreeChargeSlots--;
            Stations[indexStation] = station;

            XmlTools.SaveListToXMLSerializer(Stations, @"StationXml.xml");
        }


        public void RealeseChargingStation(int stationId)
        {
            List<Station> Stations = XmlTools.LoadListFromXMLSerializer<Station>(@"StationXml.xml");

            Station station = GetStation(stationId);
            int indexStation = Stations.FindIndex(st => st.Id == stationId);

            station.FreeChargeSlots++;
            Stations[indexStation] = station;

            XmlTools.SaveListToXMLSerializer(Stations, @"StationXml.xml");
        }

        #endregion

        #region Customer functions


        public void AddCustomer(Customer customer)
        {
            XElement element = XmlTools.LoadFromXMLElement(@"CustomerXml.xml");

            XElement Customer = (from cus in element.Elements()
                                 where cus.Element("Id").Value == customer.Id.ToString()
                                 select cus).SingleOrDefault();

            if (Customer != null)
            {
                throw new ExistsNumberException();
            }

            Customer = new XElement("Customer",
                                new XElement("Id", customer.Id),
                                new XElement("Name", customer.Name),
                                new XElement("PhoneNumber", customer.Phone),
                                new XElement("Longitude", customer.Longitude),
                                new XElement("Latitude", customer.Lattitude));

            element.Add(Customer);

            XmlTools.SaveToXMLElement(element, @"CustomerXml.xml");
        }


        public void UpdateCustomer(Customer customer)
        {
            XElement element = XmlTools.LoadFromXMLElement(@"CustomerXml.xml");

            XElement Customer = (from cus in element.Elements()
                                 where cus.Element("Id").Value == customer.Id.ToString()
                                 select cus).SingleOrDefault();
            if (Customer == null)
            {
                throw new NoNumberFoundException();
            }

            Customer.Element("Id").Value = customer.Id.ToString();
            Customer.Element("Name").Value = customer.Name;
            Customer.Element("PhoneNumber").Value = customer.Phone;
            Customer.Element("Longitude").Value = customer.Longitude.ToString();
            Customer.Element("Latitude").Value = customer.Lattitude.ToString();

            XmlTools.SaveToXMLElement(element, @"CustomerXml.xml");
        }


        public Customer GetCustomer(int customerId)
        {
            XElement element = XmlTools.LoadFromXMLElement(@"CustomerXml.xml");

            XElement Customer = (from cus in element.Elements()
                                 where cus.Element("Id").Value == customerId.ToString()
                                 select cus).SingleOrDefault();

            if (Customer == null)
            {
                throw new NoNumberFoundException();
            }

            Customer customer = new Customer()
            {
                Id = int.Parse(Customer.Element("Id").Value),
                Name = Customer.Element("Name").Value,
                Phone = Customer.Element("Phone").Value,
                Longitude = double.Parse(Customer.Element("Longitude").Value),
                Lattitude = double.Parse(Customer.Element("Lattitude").Value)
            };
            return customer;
  
        }


        /// <summary>
        /// Displays a list of customers.
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param>
        /// <returns>The list of customers</returns>
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> predicate = null)
        {
            XElement element = XmlTools.LoadFromXMLElement(@"CustomerXml.xml");
            IEnumerable<Customer> customers = from cus in element.Elements()
                                             select new Customer()
                                             {
                                                 Id = int.Parse(cus.Element("Id").Value),
                                                 Name = cus.Element("Name").Value,
                                                 Phone = cus.Element("Phone").Value,
                                                 Longitude = double.Parse(cus.Element("Longitude").Value),
                                                 Lattitude = double.Parse(cus.Element("Lattitude").Value)
                                             };


            return customers;
        }

        #endregion

        #region Package 

        /// <summary>
        /// Function of adding a package.
        /// </summary>
        /// <param name="package">Package to add</param>
        public void AddPackage(Package package)
        {
            List<Package> packages = XmlTools.LoadListFromXMLSerializer<Package>(@"PackageXml.xml");
            XElement config = XmlTools.LoadFromXMLElement(@"config.xml");
            XElement packageIdCounter = config.Element("PackageIdCounter");
            int intPackageIdCounter = int.Parse(packageIdCounter.Value);


            package.Id = intPackageIdCounter;
            packages.Add(package);

            packageIdCounter.Value = (intPackageIdCounter + 1).ToString();

            XmlTools.SaveToXMLElement(config, @"config.xml");
            XmlTools.SaveListToXMLSerializer(packages, @"PackageXml.xml");

        }

        /// <summary>
        /// Function of updating a package.
        /// </summary>
        /// <param name="Package">Package to update</param>
        public void UpdatePackage(Package package)
        {
            List<Package> packageList = XmlTools.LoadListFromXMLSerializer<Package>(@"PackageXml.xml");

            if (!packageList.Exists(pck => pck.Id == package.Id))
            {
                throw new NoNumberFoundException();
            }

            int iU = packageList.FindIndex(pck => pck.Id == package.Id);
            packageList.Insert(iU, package);

            XmlTools.SaveListToXMLSerializer(packageList, @"PackageXml.xml");
        }

        /// <summary>
        ///Function for displaying package.
        /// </summary>
        /// <param name="packageId"> The id of package</param>
        /// <returns>A copy of the package function</returns>
        public Package GetPackage(int packageId)
        {
            List<Package> package = XmlTools.LoadListFromXMLSerializer<Package>(@"PackageXml.xml");

            if (!package.Exists(x => x.Id == packageId))
            {
                throw new NoNumberFoundException();
            }

            return package.First(pck => pck.Id == packageId);
        }


        /// <summary>
        /// Displays a list of package's.
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param>
        /// <returns>The list of packages</returns>
        public IEnumerable<Package> GetPackages(Predicate<Package> predicate = null)
        {
            List<Package> package = XmlTools.LoadListFromXMLSerializer<Package>(@"PackageXml.xml");


            return package.Where(i => predicate == null ? true : predicate(i));
        }

        /// <summary>
        /// A function that implements a state of connecting a package to a skimmer
        /// </summary>
        /// <param name="id">The id of the package </param>
        /// <param name="droneId">The id of the drone</param>
        public void ConnectPackageToDrone(int id, int droneId)
        {
            List<Package> packageList = XmlTools.LoadListFromXMLSerializer<Package>(@"PackageXml.xml");

            Package package = GetPackage(id);
            int indexPackage = packageList.FindIndex(pck => pck.Id == id);


            package.DroneId = droneId;
            package.Scheduled = DateTime.Now;

            packageList[indexPackage] = package;

            XmlTools.SaveListToXMLSerializer(packageList, @"PackageXml.xml");
        }

        /// <summary>
        /// A function that implements the state of a collected package
        /// </summary>
        /// <param name="id">The id of the package </param>
        public void PickUp(int id)
        {
            List<Package> packageList = XmlTools.LoadListFromXMLSerializer<Package>(@"PackageXml.xml");

            Package package = GetPackage(id);
            int indexPackage = packageList.FindIndex(pck => pck.Id == id);

            package.PickedUp = DateTime.Now;
            packageList[indexPackage] = package;

            XmlTools.SaveListToXMLSerializer(packageList, @"PackageXml.xml");
        }

        /// <summary>
        /// A function that implements the state of a delivered package
        /// </summary>
        /// <param name="id">The id of the package</param>
        public void PackageDeliver(int id)
        {
            List<Package> packageList = XmlTools.LoadListFromXMLSerializer<Package>(@"PackageXml.xml");

            Package package = GetPackage(id);
            int indexPackage = packageList.FindIndex(pck => pck.Id == id);

            package.Delivered = DateTime.Now;
            package.DroneId = -1;
            packageList[indexPackage] = package;

            XmlTools.SaveListToXMLSerializer(packageList, @"PackageXml.xml");
        }


        /// <summary>
        /// Delete a package from the list
        /// </summary>
        /// <param name="id">The id of the package</param>
        public void DeletePackage(int id)
        {
            List<Package> packageList = XmlTools.LoadListFromXMLSerializer<Package>(@"PackageXml.xml");

            int index = packageList.FindIndex(pck => pck.Id == id);
            packageList.RemoveAt(index != -1 ? index : throw new NoNumberFoundException(" "));

            XmlTools.SaveListToXMLSerializer(packageList, @"PackageXml.xml");
        }

        #endregion

        #region Drone charge functions

        /// <summary>
        /// Function of adding a droneCharge.
        /// </summary>
        /// <param name="droneCharge">Drone charge to add</param>
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            List<DroneCharge> droneChargeList = XmlTools.LoadListFromXMLSerializer<DroneCharge>(@"DroneCharge.xml");

            if (droneChargeList.Exists(x => x.DroneId == droneCharge.DroneId))
            {
                throw new ExistsNumberException();
            }

            droneChargeList.Add(droneCharge);

            XmlTools.SaveListToXMLSerializer(droneChargeList, @"DroneCharge.xml");
        }

        /// <summary>
        /// Function of updating a drone charge.
        /// </summary>
        /// <param name="Package">Package to update</param>
        public void UpdateDroneCharge(DroneCharge droneCharge)
        {
            List<DroneCharge> droneChargeList = XmlTools.LoadListFromXMLSerializer<DroneCharge>(@"DroneCharge.xml");

            if (!droneChargeList.Exists(x => x.DroneId == droneCharge.DroneId))
            {
                throw new NoNumberFoundException();
            }

            int iU = droneChargeList.FindIndex(drCh => drCh.DroneId == droneCharge.DroneId);
            droneChargeList.Insert(iU, droneCharge);

            XmlTools.SaveListToXMLSerializer(droneChargeList, @"DroneCharge.xml");
        }

        /// <summary>
        /// Function for displaying drone charges.
        /// </summary>
        /// <param name="droneId">The id of drone</param>
        /// <returns>A copy of the drone function</returns>
        public DroneCharge GetDroneCharge(int droneId)
        {
            List<DroneCharge> droneCharge = XmlTools.LoadListFromXMLSerializer<DroneCharge>(@"DroneCharge.xml");

            if (!droneCharge.Exists(x => x.DroneId == droneId))
            {
                throw new NoNumberFoundException();
            }

            return droneCharge.First(dr => dr.DroneId == droneId);
        }

        /// <summary>
        /// Displays a list of drone chrarges.
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param>
        /// <returns>The list of dronesList</returns>
        public IEnumerable<DroneCharge> GetDronesCharges(Predicate<DroneCharge> predicate = null)
        {
            List<DroneCharge> droneCharge = XmlTools.LoadListFromXMLSerializer<DroneCharge>(@"DroneCharge.xml");

            return droneCharge.Where(i => predicate == null ? true : predicate(i));
        }

        /// <summary>
        /// Delete a drone charge from the list
        /// </summary>
        /// <param name="id">The id of drone</param>
        public void DeleteDroneCharge(int id)
        {
            List<DroneCharge> droneCharge = XmlTools.LoadListFromXMLSerializer<DroneCharge>(@"DroneCharge.xml");

            if (!droneCharge.Exists(x => x.DroneId == id))
            {
                throw new NoNumberFoundException();
            }

            int Id = droneCharge.FindIndex(drc => drc.DroneId == id);
            droneCharge.RemoveAt(Id != -1 ? Id : throw new NoNumberFoundException(" "));

            XmlTools.SaveListToXMLSerializer(droneCharge, @"DroneCharge.xml");
        }

        #endregion

        public List<double> ChargingRequest()
        {
            XElement config = XmlTools.LoadFromXMLElement(@"config.xml");


            List<double> ChargingRequests = new()
            {
                double.Parse(config.Element("DroneAvailable").Value),
                double.Parse(config.Element("LightWeight").Value),
                double.Parse(config.Element("MediumWeight").Value),
                double.Parse(config.Element("HeavyWeight").Value),
                double.Parse(config.Element("ChargingRate").Value)
            };
            XmlTools.SaveToXMLElement(config, @"config.xml");
            return ChargingRequests;


           
        }

    }
}
