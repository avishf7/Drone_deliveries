

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class PackageToList
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
        /// Gets package status
        /// </summary>
        public PackageStatus PackageStatus { get; set; }



  
        public override string ToString()
        {
            return "Details of ID:" + Id + "\nSender ID: " + SenderId +
                "\nTarget ID: " + TargetId + "\nWeight: " + Weight + "\nPriority: " + Priority
                + "\nPackage status: " + PackageStatus + "\n";
        }
    }
}
