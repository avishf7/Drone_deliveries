using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalXml
{
    public class DalXml : IDal
    {
        static DalXml()// static ctor to ensure instance init is done just before first usage
        {
            //DataSource.Initialize();
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
    }
}
