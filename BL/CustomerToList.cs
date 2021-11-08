using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class CustomerToList
    {
        /// <summary>
        /// 
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerPhone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int NumOfPackagesProvided { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int NumOfPackagesNotProvided { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int NumOfPackagesReceived { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SeveralPackagesOnWay { get; set; }


        public override string ToString()
        {
            return "Details customer to list" + "\nCustomer id: " + CustomerId +
                "\nCustomer name: " + CustomerName + "\nCustomer phone: " + CustomerPhone +
                "\nNumber of packages provided: " + NumOfPackagesProvided + "\nNumber of packages not provided: " +
               NumOfPackagesNotProvided + "\nNumber of packages received: " + NumOfPackagesReceived +
               "\nSeveralpPackages on way: " + SeveralPackagesOnWay + "\n";
        }
    }
}
