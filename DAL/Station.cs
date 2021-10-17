using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int FreeChargeSlots { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }


            public override string ToString()
            {
                return "Details of ID :" + Id + "\nName: " + Name + "\nFree charge slots: " + FreeChargeSlots
                     + "\nLongitude: " + Longitude + "\nLattitude: " + Lattitude + "\n";
            }
        }
    }
}
