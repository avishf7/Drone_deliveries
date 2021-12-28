using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace PO
{

    public class Drone : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///  ID
        /// </summary>
        private int id;
        /// <summary>
        /// property for ID
        /// </summary>
        public int Id
        {
            get => id;
            set
            {
                id = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Id"));
            }
        }

        /// <summary>
        /// model
        /// </summary>
        private string model;
        /// <summary>
        ///  property for model
        /// </summary>
        public string Model
        {
            get => model;
            set
            {
                model = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Model"));
            }
        }

        /// <summary>
        /// max weight
        /// </summary>
        private Weight maxWeight;
        /// <summary>
        /// property for max weight
        /// </summary>
        public Weight MaxWeight
        {
            get => maxWeight;
            set
            {
                maxWeight = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("MaxWeight"));
            }
        }

        /// <summary>
        /// drone's battery stutus;
        /// </summary>
        private double batteryStatus;
        /// <summary>
        /// property for drone's battery stutus;
        /// </summary>
        public double BatteryStatus
        {
            get => batteryStatus;
            set
            {
                batteryStatus = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("BatteryStatus"));
            }
        }

        /// <summary>
        /// drone status
        /// </summary>
        private DroneStatuses droneStatus;
        /// <summary>
        /// property for drone status
        /// </summary>
        public DroneStatuses DroneStatus
        {
            get => droneStatus;
            set
            {
                droneStatus = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("DroneStatus"));
            }

        }

        /// <summary>
        /// Package in progress
        /// </summary>
        private PackageInTransfer packageInProgress;
        /// <summary>
        /// property for Package in progress
        /// </summary>
        public PackageInTransfer PackageInProgress
        {
            get => packageInProgress;
            set
            {
                packageInProgress = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("PackageInProgress"));
            }

        }

        /// <summary>
        /// location of drone
        /// </summary>
        private Location locationOfDrone;
        /// <summary>
        /// property for location of drone
        /// </summary>
        public Location LocationOfDrone
        {
            get => locationOfDrone;
            set
            {
                locationOfDrone = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("LocationOfDrone"));
            }

        }


    }
}
