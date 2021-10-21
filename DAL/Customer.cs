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

            public override string ToString()
            {
                return "Details of ID :" + Id + "\nName:" + Name + "\nPhone:" + 
                    Phone + "\nLongitude: " + Longitude + "\nLattitude: " + Lattitude + "\n";
            }
        }
    }

}
