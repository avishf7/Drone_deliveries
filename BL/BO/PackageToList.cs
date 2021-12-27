

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
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
        public string SenderName { get; set; }

        /// <summary>
        /// Gets the target id.
        /// </summary>
        public string TargetName { get; set; }

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
            return "Details of ID:" + Id + "\nSender ID: " + SenderName +
                "\nTarget ID: " + TargetName + "\nWeight: " + Weight + "\nPriority: " + Priority
                + "\nPackage status: " + PackageStatus + "\n";
        }
    }
}
