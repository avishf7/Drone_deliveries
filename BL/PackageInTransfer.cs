using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class PackageInTransfer
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsCollected { get; set; }
        /// <summary>
        /// Gets the priority.
        /// </summary>
        public Priorities Priority { get; set; }
        /// <summary>
        /// 
        /// </summary>
        CustomerInPackage SenderCustomerInPackage;
        /// <summary>
        /// 
        /// </summary>
        CustomerInPackage TargetCustomerInPackage;
        /// <summary>
        /// 
        /// </summary>
        Location CollectionLocation;
        /// <summary>
        /// 
        /// </summary>
        Location DeliveryDestinationLocation;
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
