using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class Customer : INotifyPropertyChanged
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
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(CustomerLocation)));
            }

        }

        /// <summary>
        ///  PackageAtCustomerFromCustomer
        /// </summary>
        private IEnumerable<PackageAtCustomer> packageAtCustomerFromCustomer;
        /// <summary>
        /// property for packageAtCustomerFromCustomer
        /// </summary>
        public IEnumerable<PackageAtCustomer> PackageAtCustomerFromCustomer
        {
            get => packageAtCustomerFromCustomer;
            set
            {
                packageAtCustomerFromCustomer = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(PackageAtCustomerFromCustomer)));
            }
        }

        /// <summary>
        ///  PackageAtCustomerToCustomer
        /// </summary>
        private IEnumerable<PackageAtCustomer> packageAtCustomerToCustomer;
        /// <summary>
        /// property for packageAtCustomerToCustomer
        /// </summary>
        public IEnumerable<PackageAtCustomer> PackageAtCustomerToCustomer
        {
            get => packageAtCustomerToCustomer;
            set
            {
                packageAtCustomerToCustomer = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(PackageAtCustomerToCustomer)));
            }
        }

    }
}
