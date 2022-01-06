using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class Package : INotifyPropertyChanged
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
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        /// <summary>
        /// SenderCustomerInPackage
        /// </summary>
        private CustomerInPackage senderCustomerInPackage;
        /// <summary>
        /// property for senderCustomerInPackage
        /// </summary>
        public CustomerInPackage SenderCustomerInPackage
        {
            get => senderCustomerInPackage;
            set
            {
                senderCustomerInPackage = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(SenderCustomerInPackage)));
            }

        }

        /// <summary>
        /// TargetCustomerInPackage
        /// </summary>
        private CustomerInPackage targetCustomerInPackage;
        /// <summary>
        /// property for targetCustomerInPackage
        /// </summary>
        public CustomerInPackage TargetCustomerInPackage
        {
            get => targetCustomerInPackage;
            set
            {
                targetCustomerInPackage = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(TargetCustomerInPackage)));
            }

        }

        /// <summary>
        /// weight
        /// </summary>
        private Weight weight;
        /// <summary>
        /// property for weight
        /// </summary>
        public Weight Weight
        {
            get => weight;
            set
            {
                weight = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Weight)));
            }

        }


        /// <summary>
        /// Priority
        /// </summary>
        private Priorities priority;
        /// <summary>
        /// property for priority
        /// </summary>
        public Priorities Priority
        {
            get => priority;
            set
            {
                priority = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Priority)));
            }
        }

        /// <summary>
        /// DroneInPackage
        /// </summary>
        private DroneInPackage droneInPackage;
        /// <summary>
        /// property for droneInPackage
        /// </summary>
        public DroneInPackage DroneInPackage
        {
            get => droneInPackage;
            set
            {
                droneInPackage = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(DroneInPackage)));
            }

        }

        /// <summary>
        /// Requested
        /// </summary>
        private DateTime? requested;
        /// <summary>
        /// property for requested
        /// </summary>
        public DateTime? Requested
        {
            get => requested;
            set
            {
                requested = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Requested)));
            }
        }


        /// <summary>
        /// Scheduled
        /// </summary>
        private DateTime? scheduled;
        /// <summary>
        /// property for scheduled
        /// </summary>
        public DateTime? Scheduled
        {
            get => scheduled;
            set
            {
                scheduled = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Scheduled)));
            }
        }

        /// <summary>
        /// PickedUp
        /// </summary>
        private DateTime? pickedUp;
        /// <summary>
        /// property for pickedUp
        /// </summary>
        public DateTime? PickedUp
        {
            get => pickedUp;
            set
            {
                pickedUp = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(PickedUp)));
            }
        }

        /// <summary>
        /// Delivered
        /// </summary>
        private DateTime? delivered;
        /// <summary>
        /// property for pickedUp
        /// </summary>
        public DateTime? Delivered
        {
            get => delivered;
            set
            {
                delivered = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Delivered)));
            }
        }
                                        
    }
}
