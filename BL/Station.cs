using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Station
    {
        /// <summary>
        /// Gets the id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets the longitude.
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Gets the lattitude.
        /// </summary>
        public double Lattitude { get; set; }
        /// <summary>
        /// Gets the free charge slot.
        /// </summary>
        public int FreeChargeSlots { get; set; }
        /// <summary>
        /// Gets num of charging drones
        /// </summary>
        public int NumOfChargingDrones { get; set; }




        public override string ToString()
        {
            return "Details of ID :" + Id + "\nName: " + Name + "\nLattitude: " + Lattitude + "\nLongitude: " + Longitude
                + "\nFree charge slots: " + FreeChargeSlots
                  + "\nNum of charging drones" + NumOfChargingDrones +"\n";
        }
    }
}
