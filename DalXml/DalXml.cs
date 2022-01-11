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

        private DalXml()//CTOR for Initialize first data
        {     
            
        }

        #region Drone
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

        #region Station
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

        #region Customer 


        public void AddCustomer(Customer customer)
        {
            XElement element = XmlTools.LoadListFromXMLElement(@"CustomerXml.xml");

            XElement Customer = (from cus in element.Elements()
                                 where cus.Element("Id").Value == customer.Id.ToString()
                                 select cus).FirstOrDefault();

            if (Customer != null)
            {
                throw new ExistsNumberException();
            }
            XElement XCustomer = new XElement("Customer",
                                new XElement("Id", customer.Id),
                                new XElement("Name", customer.Name),
                                new XElement("PhoneNumber", customer.Phone),
                                new XElement("Longitude", customer.Longitude),
                                new XElement("Latitude", customer.Lattitude));

            element.Add(XCustomer);

            XmlTools.SaveListToXMLElement(element, @"CustomerXml.xml");
        }


        public void UpdateCustomer(Customer customer)
        {
            XElement element = XmlTools.LoadListFromXMLElement(@"CustomerXml.xml");

            XElement Customer = (from cus in element.Elements()
                                 where cus.Element("Id").Value == customer.Id.ToString()
                                 select cus).FirstOrDefault();
            if (Customer == null)
            {
                throw new NoNumberFoundException();
            }

            Customer.Element("Id").Value = customer.Id.ToString();
            Customer.Element("Name").Value = customer.Name;
            Customer.Element("PhoneNumber").Value = customer.Phone;
            Customer.Element("Longitude").Value = customer.Longitude.ToString();
            Customer.Element("Latitude").Value = customer.Lattitude.ToString();

            XmlTools.SaveListToXMLElement(element, @"CustomerXml.xml");
        }


        public Customer GetCustomer(int customerId)
        {
            XElement element = XmlTools.LoadListFromXMLElement(@"CustomerXml.xml");

            Customer Customer = (from cus in element.Elements()
                                 where cus.Element("Id").Value == customerId.ToString()
                                 select new Customer()
                                 {
                                     Id = int.Parse(cus.Element("Id").Value),
                                     Name = cus.Element("Name").Value,
                                     Phone = cus.Element("PhoneNumber").Value,
                                     Longitude = double.Parse(cus.Element("Longitude").Value),
                                     Lattitude = double.Parse(cus.Element("Latitude").Value)
                                 }
                        ).FirstOrDefault();


            if (Customer.Id != customerId)
            {
                throw new NoNumberFoundException();
            }

            return Customer;
        }


        /// <summary>
        /// Displays a list of customers.
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param>
        /// <returns>The list of customers</returns>
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> predicate = null)
        {
            XElement element = XmlTools.LoadListFromXMLElement(@"CustomerXml.xml");
            IEnumerable<Customer> customer = from cus in element.Elements()
                                             select new Customer()
                                             {
                                                 Id = int.Parse(cus.Element("Id").Value),
                                                 Name = cus.Element("Name").Value,
                                                 Phone = cus.Element("PhoneNumber").Value,
                                                 Longitude = double.Parse(cus.Element("Longitude").Value),
                                                 Lattitude = double.Parse(cus.Element("Latitude").Value)
                                             };


            return customer.Select(item => item);
        }

        #endregion

        #region Package functions

        /// <summary>
        /// Function of adding a package.
        /// </summary>
        /// <param name="package">Package to add</param>
        public void AddPackage(Package package)
        {
            package.Id =int.Parse(XElement.Load(@"config.xml").Element("config-elements").Element("PackageIdCounter").Value) ;
            //DataSource.packages.Add(package);
        }

        public void DeleteDrone(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePackage(Package Package)
        {
            throw new NotImplementedException();
        }

        public Package GetPackage(int packageId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Package> GetPackages(Predicate<Package> predicate = null)
        {
            throw new NotImplementedException();
        }

        public void PickUp(int id)
        {
            throw new NotImplementedException();
        }

        public void PackageDeliver(int id)
        {
            throw new NotImplementedException();
        }

        public void ConnectPackageToDrone(int id, int droneId)
        {
            throw new NotImplementedException();
        }

        public void DeletePackage(int id)
        {
            throw new NotImplementedException();
        }

        public void AddDroneCharge(DroneCharge droneCharge)
        {
            throw new NotImplementedException();
        }

        public void UpdateDroneCharge(DroneCharge droneCharge)
        {
            throw new NotImplementedException();
        }

        public DroneCharge GetDroneCharge(int droneId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DroneCharge> GetDronesCharges(Predicate<DroneCharge> predicate = null)
        {
            throw new NotImplementedException();
        }

        public void DeleteDroneCharge(int id)
        {
            throw new NotImplementedException();
        }

        ///// <summary>
        ///// Function of updating a package.
        ///// </summary>
        ///// <param name="Package">Package to update</param>
        //public void UpdatePackage(Package package)
        //{
        //    if (!DataSource.packages.Exists(x => x.Id == package.Id))
        //    {
        //        throw new NoNumberFoundException();
        //    }

        //    int iU = DataSource.packages.FindIndex(pck => pck.Id == package.Id);
        //    DataSource.packages.Insert(iU, package);
        //}

        ///// <summary>
        /////Function for displaying package.
        ///// </summary>
        ///// <param name="packageId"> The id of package</param>
        ///// <returns>A copy of the package function</returns>
        //public Package GetPackage(int packageId)
        //{
        //    if (!DataSource.packages.Exists(x => x.Id == packageId))
        //    {
        //        throw new NoNumberFoundException();
        //    }

        //    return DataSource.packages.First(pck => pck.Id == packageId);
        //}


        ///// <summary>
        ///// Displays a list of package's.
        ///// </summary>
        ///// <param name="predicate">The list will be filtered according to the conditions obtained</param>
        ///// <returns>The list of packages</returns>
        //public IEnumerable<Package> GetPackages(Predicate<Package> predicate = null)
        //{
        //    return DataSource.packages.Where(i => predicate == null ? true : predicate(i));
        //}

        ///// <summary>
        ///// A function that implements a state of connecting a package to a skimmer
        ///// </summary>
        ///// <param name="id">The id of the package </param>
        ///// <param name="droneId">The id of the drone</param>
        //public void ConnectPackageToDrone(int id, int droneId)
        //{
        //    Package package = GetPackage(id);
        //    int indexPackage = DataSource.packages.FindIndex(pck => pck.Id == id);


        //    package.DroneId = droneId;
        //    package.Scheduled = DateTime.Now;

        //    DataSource.packages[indexPackage] = package;
        //}

        ///// <summary>
        ///// A function that implements the state of a collected package
        ///// </summary>
        ///// <param name="id">The id of the package </param>
        //public void PickUp(int id)
        //{
        //    Package package = GetPackage(id);
        //    int indexPackage = DataSource.packages.FindIndex(pck => pck.Id == id);

        //    package.PickedUp = DateTime.Now;
        //    DataSource.packages[indexPackage] = package;

        //}

        ///// <summary>
        ///// A function that implements the state of a delivered package
        ///// </summary>
        ///// <param name="id">The id of the package</param>
        //public void PackageDeliver(int id)
        //{
        //    Package package = GetPackage(id);
        //    int indexPackage = DataSource.packages.FindIndex(pck => pck.Id == id);

        //    package.Delivered = DateTime.Now;
        //    package.DroneId = -1;
        //    DataSource.packages[indexPackage] = package;
        //}


        ///// <summary>
        ///// Delete a package from the list
        ///// </summary>
        ///// <param name="id">The id of the package</param>
        //public void DeletePackage(int id)
        //{
        //    int index = DataSource.packages.FindIndex(pck => pck.Id == id);
        //    DataSource.packages.RemoveAt(index != -1 ? index : throw new NoNumberFoundException(" "));
        //}

        //#endregion

        //#region Drone charge functions

        ///// <summary>
        ///// Function of adding a droneCharge.
        ///// </summary>
        ///// <param name="droneCharge">Drone charge to add</param>
        //public void AddDroneCharge(DroneCharge droneCharge)
        //{
        //    if (DataSource.droneCharges.Exists(x => x.DroneId == droneCharge.DroneId))
        //    {
        //        throw new ExistsNumberException();
        //    }

        //    DataSource.droneCharges.Add(droneCharge);
        //}

        ///// <summary>
        ///// Function of updating a drone charge.
        ///// </summary>
        ///// <param name="Package">Package to update</param>
        //public void UpdateDroneCharge(DroneCharge droneCharge)
        //{
        //    if (!DataSource.droneCharges.Exists(x => x.DroneId == droneCharge.DroneId))
        //    {
        //        throw new NoNumberFoundException();
        //    }

        //    int iU = DataSource.droneCharges.FindIndex(drCh => drCh.DroneId == droneCharge.DroneId);
        //    DataSource.droneCharges.Insert(iU, droneCharge);
        //}

        ///// <summary>
        ///// Function for displaying drone charges.
        ///// </summary>
        ///// <param name="droneId">The id of drone</param>
        ///// <returns>A copy of the drone function</returns>
        //public DroneCharge GetDroneCharge(int droneId)
        //{
        //    if (!DataSource.droneCharges.Exists(x => x.DroneId == droneId))
        //    {
        //        throw new NoNumberFoundException();
        //    }

        //    return DataSource.droneCharges.First(dr => dr.DroneId == droneId);
        //}

        ///// <summary>
        ///// Displays a list of drone chrarges.
        ///// </summary>
        ///// <param name="predicate">The list will be filtered according to the conditions obtained</param>
        ///// <returns>The list of dronesList</returns>
        //public IEnumerable<DroneCharge> GetDronesCharges(Predicate<DroneCharge> predicate = null)
        //{
        //    return DataSource.droneCharges.Where(i => predicate == null ? true : predicate(i));
        //}

        ///// <summary>
        ///// Delete a drone charge from the list
        ///// </summary>
        ///// <param name="id">The id of drone</param>
        //public void DeleteDroneCharge(int id)
        //{
        //    if (!DataSource.droneCharges.Exists(x => x.DroneId == id))
        //    {
        //        throw new NoNumberFoundException();
        //    }

        //    int Id = DataSource.droneCharges.FindIndex(drc => drc.DroneId == id);
        //    DataSource.droneCharges.RemoveAt(Id != -1 ? Id : throw new NoNumberFoundException(" "));
        //}

        /// <summary>
        /// Information on power consumption and charging time
        /// </summary>
        /// <returns>A list of the charging requests</returns>
        /// 
        public List<double> ChargingRequest()
        {
            IEnumerable<double> config = XmlTools.LoadListFromXMLElement(@"config.xml");

            
            List<double> ChargingRequests = new()
            {               
            config.DroneAvailable,
                DataSource.Config.LightWeight,
                DataSource.Config.MediumWeight,
                DataSource.Config.HeavyWeight,
                DataSource.Config.ChargingRate
            };

            return ChargingRequests;
        }


        #endregion

    }
}
