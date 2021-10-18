using IDAL.DO;
using System;
using System.Collections.Generic;

namespace DalObject
{
	public class DalObject
	{
		 List<DroneCharge> droneCharges = new();

		public void AddDrone()
        {
            Console.WriteLine("Enter drones ID: ");
			int id = int.Parse(Console.ReadLine());



			DataSource.drones.Add(new() {Id = id,  });
        }
	}
}
