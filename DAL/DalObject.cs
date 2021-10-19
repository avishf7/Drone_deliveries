using IDAL.DO;
using System;
using System.Collections.Generic;
using IDAL;


namespace DalObject
{
    public class DalObject
    {
        List<DroneCharge> droneCharges = new();

        public void AddDrone()
        {
            Console.WriteLine("Enter drones ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter drones model: ");
            string model = (Console.ReadLine());
            Console.WriteLine("Enter drones Weight - To LIGHT enter 0, to MEDIUM enter 1 and to HEAVY enter 2: ");
            Weight maxweight = (Weight)int.Parse(Console.ReadLine());
            Console.WriteLine("Enter drones status - To  AVAILABLE  enter 0, to MAINTENANCE enter 1 and to DELIVERY enter 2: ");
            DroneStatuses status = (DroneStatuses)int.Parse(Console.ReadLine());
            double battery = 100.0;


            DataSource.drones.Add(new() { Id = id, Model = model, MaxWeight = maxweight, Status = status, Battery = battery });
        }

        public void AddStation()
        {
            Console.WriteLine("Enter stations ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter drones name: ");
            string name = (Console.ReadLine());
            Console.WriteLine("Enter num of free station: ");
            int numOfFreeStation = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter longitude of stations adress: ");
            double longitude = (int)double.Parse(Console.ReadLine());
            Console.WriteLine("Enter lattitude of stations adress: ");
            double lattitude = (int)double.Parse(Console.ReadLine());


            DataSource.stations.Add(new()
            {
                Id = id,
                Name = name,
                FreeChargeSlots = numOfFreeStation,
                Longitude = lattitude,
                Lattitude = lattitude
            });
        }

        public void AddCustomer()
        {
            Console.WriteLine("Enter customers ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter customers name: ");
            string name = (Console.ReadLine());
            Console.WriteLine("Enter customers phone: ");
            string phone = (Console.ReadLine());
            Console.WriteLine("Enter longitude of customers adress: ");
            double longitude = (int)double.Parse(Console.ReadLine());
            Console.WriteLine("Enter lattitude of customers adress: ");
            double lattitude = (int)double.Parse(Console.ReadLine());

            DataSource.customers.Add(new()
            {
                Id = id,
                Name = name,
                Phone = phone,
                Longitude = lattitude,
                Lattitude = lattitude
            });
        }

        public void AddPackage()
        {
         ///  Console.WriteLine("Enter package's ID: ");
            ///int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter targets ID: ");
            int sendersId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter senders ID: ");
            int targetsId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter package's Weight - To LIGHT enter 0, to MEDIUM enter 1 and to HEAVY enter 2: ");
            Weight maxweight = (Weight)int.Parse(Console.ReadLine());
            Console.WriteLine("Enter pariority - To NORMAL enter 0, to FAST enter 1 and to EMERENCY enter 2: ");
            Priorities priority = (Priorities)int.Parse(Console.ReadLine());



        }


    }
}
