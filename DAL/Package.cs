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
        ///  A structure containing the details of the package.
        /// </summary>
        public struct Package
        {
            /// <summary>
            /// Gets the id.
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// Gets the sender id.
            /// </summary>
            public int SenderId { get; set; }
            /// <summary>
            /// Gets the target id.
            /// </summary>
            public int TargetId { get; set; }
            /// <summary>
            /// Gets the weight.
            /// </summary>
            public Weight Weight { get; set; }
            /// <summary>
            /// Gets the priority.
            /// </summary>
            public Priorities Priority { get; set; }
            /// <summary>
            /// Gets the requested.
            /// </summary>
            public DateTime? Requested { get; set; }
            /// <summary>
            /// Gets the scheduled.
            /// </summary>
            public DateTime? Scheduled { get; set; }
            /// <summary>
            /// Gets the picked up.
            /// </summary>
            public DateTime? PickedUp { get; set; }
            /// <summary>
            /// Gets the delivered.
            /// </summary>
            public DateTime? Delivered { get; set; }
            /// <summary>
            /// Gets the drone id.
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
