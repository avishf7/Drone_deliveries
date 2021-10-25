using System;

namespace IDAL
{
    namespace DO
    {
        /// <summary>
        ///  A structure containing the details of the customer.
        /// </summary>
        public struct Customer
        {
            /// <summary>
            /// Gets the id.
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// Gets the name.
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Gets the phone.
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// Gets the longhitude.
            /// </summary>
            public double Longitude { get; set; }
            /// <summary>
            /// Gets the lattitude.
            /// </summary>
            public double Lattitude { get; set; }

            /// <summary>
            ///  Calculate the distance (KM) between the received location and the customer 
            /// according to their coordinates,
            /// Using a distance calculation formula
            /// </summary>
            /// <param name="lattitude">Start latitude on the map</param>
            /// <param name="longitude">Start longitude on the map</param>
            /// <returns>the distance (KM) between the received location and the customer</returns>
            public double distanceFrom(double lattitude, double longitude)
            {
                //Converts decimal degrees to radians:
                var rlat1 = Math.PI * Lattitude / 180;
                var rlat2 = Math.PI * lattitude / 180;
                var rLon1 = Math.PI * Longitude / 180;
                var rLon2 = Math.PI * longitude / 180;
                var theta = Longitude - longitude;
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

            public override string ToString()
            {
                return "Details of ID :" + Id + "\nName:" + Name + "\nPhone:" + 
                    Phone + "\nLattitude: " + Lattitude + "\nLongitude: " + Longitude + "\n";
            }
        }
    }

}
