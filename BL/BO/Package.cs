using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
   public class Package
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets the id of sender customer
        /// </summary>
        public CustomerInPackage SenderCustomerInPackage { get; set; }
        /// <summary>
        /// Gets the id of target customer
        /// </summary>
        public CustomerInPackage TargetCustomerInPackage { get; set; }

        /// <summary>
        /// Gets the weight.
        /// </summary>
        public Weight Weight { get; set; }

        /// <summary>
        /// Gets the priority.
        /// </summary>
        public Priorities Priority { get; set; }

        /// <summary>
        /// Gets the id of the drone
        /// </summary>
        public DroneInPackage droneInPackage { get; set; }

        /// <summary>
        /// Gets the requested
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



        public override string ToString()
        {
            return "Details of ID:" + Id + "\nSender customer in package: " + SenderCustomerInPackage +
                "\nTarget customer in package: " + TargetCustomerInPackage + "\nWeight: " + Weight + "\nPriority: " + Priority
                + "\nRequested: " + Requested + "\nScheduled: " + Scheduled + "\nPicked up: "
                + PickedUp + "\nDelivered: " + Delivered + "\nDrone in package: " + droneInPackage + "\n";
        }
    }
}
