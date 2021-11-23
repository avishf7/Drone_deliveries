using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PackageInTransfer
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets if package is provided
        /// </summary>
        public bool IsCollected { get; set; }

        /// <summary>
        /// Gets the priority.
        /// </summary>
        public Priorities Priority { get; set; }

        /// <summary>
        /// Gets sender customer in package
        /// </summary>
        public CustomerInPackage SenderCustomerInPackage { get; set; }

        /// <summary>
        /// Gets target customer in package
        /// </summary>
        public CustomerInPackage TargetCustomerInPackage { get; set; }

        /// <summary>
        /// Gets collection location
        /// </summary>
        public Location CollectionLocation { get; set; }

        /// <summary>
        /// Gets delivery destination location
        /// </summary>
        public Location DeliveryDestinationLocation { get; set; }

        /// <summary>
        /// Distance from collection point to destination
        /// </summary>
        public double DistanceCollectionToDestination { get; set; }



        public override string ToString()
        {
            return "Details of ID :" + Id + "\nIs collected?: " + IsCollected + "\nPriority: " + Priority
                + "\nSender customer in package: " + SenderCustomerInPackage +
                "\nTarget customer in package: " + TargetCustomerInPackage + "\nCollection location: " +
                CollectionLocation + "\nDelivery destination location: " + DeliveryDestinationLocation +
                "\nDistance from collection point to destination: " + DistanceCollectionToDestination + "\n";
        }
    }
}
