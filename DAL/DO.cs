using System;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }

            public override string ToString()
            {
                return "Details of Id :" + Id + "\nName:" + Name + "\nphone:" + 
                    Phone + "\nLongitude " Longitude + "\nLattitude: " Lattitude + "\n";
            }
        }

        public struct package
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public Weight Weight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime Requested { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            public int DroneId { get; set; }

            public override string ToString()
            {
                return "Details of Id :" + Id + "\nSenderId: " + SenderId +
                    "\nTargetId: " + TargetId + "\nWeight: " + Weight + "\nPriority: " + Priority
                    + "\nRequested: " + Requested + "\nScheduled: " + Scheduled + "\nPickedUp"
                    + PickedUp + "\nDelivered: " + Delivered + "\nDroneId: " + DroneId + "\n";
            }
        }

        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public Weight MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }

            public override string ToString()
            {
                return "Details of Id :" + Id + "\nModel: " + Model + "\nMaxWeight: " +
                     MaxWeight + "\nDroneStatuses: " + Status + "\nBatteryOfDrone: "
                     + Battery + "\n";
            }

        }

        public struct Station
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int ChargeSlots { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            public int freePositions { get; set; }
            public override string ToString()
            {
                return "Details of Id :" + Id + "\nName: " + Name + "\nChargeSlots: " +
                     "\nLongitude: " + Longitude + "\nLattitude: " + Lattitude + 
                     "\nfreePositions" + freePositions +"\n";
            }

        }

        public struct DroneCharge
        {
            public int DroneId { get; set; }
            public int StationId { get; set; }

            public override string ToString()
            {
                return "Details of DroneCharge: " + "\nDroneId: " + DroneId + "\nStationId: "
                    + StationId +"\n";
            }

        }
    }

}
