using System;

namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// 
        /// </summary>
        public struct Customer
        {
            /// <summary>
            /// 
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double Longitude { get; set; }
            /// <summary>
            /// 
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
