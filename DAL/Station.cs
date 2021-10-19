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
        /// 
        /// </summary>
        public struct Station
        {
            /// <summary>
            /// 
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int FreeChargeSlots { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double Longitude { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double Lattitude { get; set; }


            public override string ToString()
            {
                return "Details of ID :" + Id + "\nName: " + Name + "\nFree charge slots: " + FreeChargeSlots
                     + "\nLongitude: " + Longitude + "\nLattitude: " + Lattitude + "\n";
            }
        }
    }
}
