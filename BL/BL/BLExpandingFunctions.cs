using DalApi;
using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace BL
{
    static class BLExpandingFunctions
    {
        /// <summary>
        /// The function converts a "DO.Customer" object to a "CustomerInPackage" object
        /// </summary>
        /// <param name="customer">"DO.Customer" object</param>
        /// <returns>"CustomerInPackage" object</returns>

        public static CustomerInPackage GetCusomerInPackage(this DO.Customer customer)
        {
            CustomerInPackage BoCustomerInPackage = new()
            {
                CustomerId = customer.Id,
                CustomerName = customer.Name
            };

            return BoCustomerInPackage;
        }

        /// <summary>
        /// Calculate the distance (KM) between two received locations 
        /// according to their coordinates,
        /// Using a distance calculation formula.
        /// </summary>
        /// <param name="sLocation">Start location</param>
        /// <param name="eLocation">End location </param>
        /// <returns>distance (KM) between two received locations</returns>
        public static double Distance(this Location sLocation, Location eLocation)
        {
            //Converts decimal degrees to radians:
            var rlat1 = Math.PI * sLocation.Lattitude / 180;
            var rlat2 = Math.PI * eLocation.Lattitude / 180;           
            var theta = sLocation.Longitude - eLocation.Longitude;
            var rtheta = Math.PI * theta / 180;

            //Formula for calculating the distance 
            //between two coordinates represented by radians:
            var dist = (Math.Sin(rlat1) * Math.Sin(rlat2)) + Math.Cos(rlat1) *
                      Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);

            dist = dist * 180 / Math.PI;  //Converts radians to decimal degrees
            dist = dist * 60 * 1.1515;

            return dist * 1.61081082288953;      //Converts to KM
        }

    }
}
