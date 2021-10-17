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
            public int ChargeSlots { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            public int freePositions { get; set; }

            public override string ToString()
            {
                return "Details of ID :" + Id + "\nName: " + Name + "\nCharge slots: " +
                     "\nLongitude: " + Longitude + "\nLattitude: " + Lattitude +
                     "\nFree positions: " + freePositions + "\n";
            }
        }
    }
}
