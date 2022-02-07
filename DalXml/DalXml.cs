using DalApi;
using DO;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;


namespace Dal
{
    /// <summary>
    /// A class that manages access to data represented by XML files.
    /// </summary>
    sealed class DalXml : IDal
    {
        /// <summary>
        /// A variable that holds one and only instance of the class (singleton).
        /// 
        /// </summary>
        internal static DalXml Instance { get; } = new DalXml();

        /// <summary>
        /// private CTOR to prevent the creation of another instance of the class.
        /// </summary>
        private DalXml() { }

        #region Drone functions

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int droneId)
        {
            List<Drone> dronesList = XmlTools.LoadListFromXMLSerializer<Drone>(@"DroneXml.xml");

            if (!dronesList.Exists(x => x.Id == droneId))
            {
                throw new NoNumberFoundException(" ");
            }

            return dronesList.First(dr => dr.Id == droneId);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDrones(Predicate<Drone> predicate = null)
        {
            List<Drone> dronesList = XmlTools.LoadListFromXMLSerializer<Drone>(@"DroneXml.xml");
            return dronesList.Where(i => predicate == null ? true : predicate(i));
        }

        #endregion

        #region Station functions

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int stationId)
        {
            List<Station> Stations = XmlTools.LoadListFromXMLSerializer<Station>(@"StationXml.xml");

            if (!Stations.Exists(x => x.Id == stationId))
            {
                throw new NoNumberFoundException();
            }
            return Stations.First(st => st.Id == stationId);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStations(Predicate<Station> predicate = null)
        {
            List<Station> Stations = XmlTools.LoadListFromXMLSerializer<Station>(@"StationXml.xml");
            return Stations.Where(i => predicate == null ? true : predicate(i));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UsingChargingStation(int stationId)
        {
            List<Station> Stations = XmlTools.LoadListFromXMLSerializer<Station>(@"StationXml.xml");

            Station station = GetStation(stationId);
            int indexStation = Stations.FindIndex(st => st.Id == stationId);

            station.FreeChargeSlots--;
            Stations[indexStation] = station;

            XmlTools.SaveListToXMLSerializer(Stations, @"StationXml.xml");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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
                                new XElement("Phone", customer.Phone),
                                new XElement("Longitude", customer.Longitude),
                                new XElement("Lattitude", customer.Lattitude));

            element.Add(Customer);

            XmlTools.SaveToXMLElement(element, @"CustomerXml.xml");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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
            Customer.Element("Phone").Value = customer.Phone;
            Customer.Element("Longitude").Value = customer.Longitude.ToString();
            Customer.Element("Lattitude").Value = customer.Lattitude.ToString();

            XmlTools.SaveToXMLElement(element, @"CustomerXml.xml");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        #region Package functions

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Package GetPackage(int packageId)
        {
            List<Package> package = XmlTools.LoadListFromXMLSerializer<Package>(@"PackageXml.xml");

            if (!package.Exists(x => x.Id == packageId))
            {
                throw new NoNumberFoundException();
            }

            return package.First(pck => pck.Id == packageId);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Package> GetPackages(Predicate<Package> predicate = null)
        {
            List<Package> package = XmlTools.LoadListFromXMLSerializer<Package>(@"PackageXml.xml");


            return package.Where(i => predicate == null ? true : predicate(i));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PickUp(int id)
        {
            List<Package> packageList = XmlTools.LoadListFromXMLSerializer<Package>(@"PackageXml.xml");

            Package package = GetPackage(id);
            int indexPackage = packageList.FindIndex(pck => pck.Id == id);

            package.PickedUp = DateTime.Now;
            packageList[indexPackage] = package;

            XmlTools.SaveListToXMLSerializer(packageList, @"PackageXml.xml");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeletePackage(int id)
        {
            List<Package> packageList = XmlTools.LoadListFromXMLSerializer<Package>(@"PackageXml.xml");

            int index = packageList.FindIndex(pck => pck.Id == id);
            packageList.RemoveAt(index != -1 ? index : throw new NoNumberFoundException(" "));

            XmlTools.SaveListToXMLSerializer(packageList, @"PackageXml.xml");
        }

        #endregion

        #region Drone charge functions

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge GetDroneCharge(int droneId)
        {
            List<DroneCharge> droneCharge = XmlTools.LoadListFromXMLSerializer<DroneCharge>(@"DroneCharge.xml");

            if (!droneCharge.Exists(x => x.DroneId == droneId))
            {
                throw new NoNumberFoundException();
            }

            return droneCharge.First(dr => dr.DroneId == droneId);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDronesCharges(Predicate<DroneCharge> predicate = null)
        {
            List<DroneCharge> droneCharge = XmlTools.LoadListFromXMLSerializer<DroneCharge>(@"DroneCharge.xml");

            return droneCharge.Where(i => predicate == null ? true : predicate(i));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDroneCharge(int id)
        {
            List<DroneCharge> droneCharge = XmlTools.LoadListFromXMLSerializer<DroneCharge>(@"DroneCharge.xml");

            //if (!droneCharge.Exists(x => x.DroneId == id))
            //{
            //    throw new NoNumberFoundException();
            //}

            int Id = droneCharge.FindIndex(drc => drc.DroneId == id);
            droneCharge.RemoveAt(Id != -1 ? Id : throw new NoNumberFoundException(" "));

            XmlTools.SaveListToXMLSerializer(droneCharge, @"DroneCharge.xml");
        }

        #endregion

        [MethodImpl(MethodImplOptions.Synchronized)]
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
