using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// A structure containing the details of the station.
        /// </summary>
        public struct Station
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
            /// Gets the free charge slot.
            /// </summary>
            public int FreeChargeSlots { get; set; }
            /// <summary>
            /// Gets the longitude.
            /// </summary>
            public double Longitude { get; set; }
            /// <summary>
            /// Gets the lattitude.
            /// </summary>
            public double Lattitude { get; set; }

           

            public override string ToString()
            {
                return "Details of ID :" + Id + "\nName: " + Name + "\nFree charge slots: " + FreeChargeSlots
                     + "\nLattitude: " + Lattitude + "\nLongitude: " + Longitude + "\n";
            }
        }
    }
}
