using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneInPackage
    {
        public int Id { get; set; }
        public double BatteryStatus { get; set; }
        //מיקום

        public override string ToString()
        {
            return "Details of Id :" + Id + "\nStatus of battery: " + BatteryStatus + "\n";
        }
    }
}
