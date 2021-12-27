using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Location
    {
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
            return "{ longitude: " + Longitude + ", Lattitude: " + Lattitude + "}\n";
        }
    }
}
