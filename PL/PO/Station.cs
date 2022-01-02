using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BO;


namespace PO
{
    public class Station : INotifyPropertyChanged
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
        /// name
        /// </summary>
        private string name;
        /// <summary>
        ///  property for name
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        /// <summary>
        /// location of station
        /// </summary>
        private Location locationOfStation;
        /// <summary>
        /// property for location of station
        /// </summary>
        public Location LocationOfStation
        {
            get => locationOfStation;
            set
            {
                locationOfStation = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("LocationOfStation"));
            }

        }

        /// <summary>
        ///  FreeChargeSlots
        /// </summary>
        private int freeChargeSlots;
        /// <summary>
        /// property for freeChargeSlots
        /// </summary>
        public int FreeChargeSlots
        {
            get => freeChargeSlots;
            set
            {
                freeChargeSlots = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("FreeChargeSlots"));
            }
        }


        /// <summary>
        ///  ChargingDrones
        /// </summary>
        private IEnumerable<DroneCharge> chargingDrones;
        /// <summary>
        /// property for chargingDrones
        /// </summary>
        public IEnumerable<DroneCharge> ChargingDrones
        {
            get => chargingDrones;
            set
            {
                chargingDrones = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ChargingDrones"));
            }
        }

    }
}
