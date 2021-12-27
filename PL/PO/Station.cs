using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            get => Name;
            set
            {
                Name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }


    }
}
