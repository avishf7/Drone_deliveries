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
        public struct Package
        {
            /// <summary>
            /// 
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int SenderId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int TargetId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Weight Weight { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Priorities Priority { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public DateTime Requested { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public DateTime Scheduled { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public DateTime PickedUp { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public DateTime Delivered { get; set; }
            /// <summary>
            /// 
            /// </summary>
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
