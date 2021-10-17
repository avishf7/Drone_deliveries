using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Package
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public Weight Weight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime Requested { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            public int DroneId { get; set; }

            public override string ToString()
            {
                return "Details of ID:" + Id + "\nSender ID: " + SenderId +
                    "\nTarget ID: " + TargetId + "\nWeight: " + Weight + "\nPriority: " + Priority
                    + "\nRequested: " + Requested + "\nScheduled: " + Scheduled + "\nPicked up: "
                    + PickedUp + "\nDelivered: " + Delivered + "\nDrone ID: " + DroneId + "\n";
            }
        }
    }
}
