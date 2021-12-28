using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    class Customer : INotifyPropertyChanged
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
        /// Phone
        /// </summary>
        private string phone;
        /// <summary>
        ///  property for phone
        /// </summary>
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Phone"));
            }
        }

        /// <summary>
        /// location of customerLocation
        /// </summary>
        private Location customerLocation;
        /// <summary>
        /// property for location of station
        /// </summary>
        public Location CustomerLocation
        {
            get => customerLocation;
            set
            {
                customerLocation = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CustomerLocation"));
            }

        }

        /// <summary>
        ///  PackageAtCustomerFromCustomer
        /// </summary>
        private List<PackageAtCustomer> packageAtCustomerFromCustomer;
        /// <summary>
        /// property for packageAtCustomerFromCustomer
        /// </summary>
        public List<PackageAtCustomer> PackageAtCustomerFromCustomer
        {
            get => packageAtCustomerFromCustomer;
            set
            {
                packageAtCustomerFromCustomer = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("PackageAtCustomerFromCustomer "));
            }
        }

    }
}
