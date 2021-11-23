using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{

    [Serializable]
    public class ExistsNumberException : Exception
    {
        public ExistsNumberException() : base() { }
        public ExistsNumberException(string message) : base(message) { }
        public ExistsNumberException(string message, Exception inner) : base(message, inner) { }
        protected ExistsNumberException(SerializationInfo info, StreamingContext context)
     : base(info, context) { }


        override public string ToString()
        { return "Exists number" + "\n" + Message; }
    }

    [Serializable]
    public class NoNumberFoundException : Exception
    {

        public NoNumberFoundException() : base() { }
        public NoNumberFoundException(string message) : base(message) { }
        public NoNumberFoundException(string message, Exception inner) : base(message, inner) { }
        protected NoNumberFoundException(SerializationInfo info, StreamingContext context)
     : base(info, context) { }

        override public string ToString()
        { return "Number not found " + "\n" + Message; }
    }

    [Serializable]
    internal class DroneNotAvailableException : Exception
    {
        public DroneNotAvailableException() : base() { }

        public DroneNotAvailableException(string message) : base(message) { }

        public DroneNotAvailableException(string message, Exception innerException) : base(message, innerException) { }

        protected DroneNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        override public string ToString()
        { return "Drone is not available " + "\n" + Message; }
    }


    [Serializable]
    public class NoSuitablePackageForScheduledException : Exception
    {
        public NoSuitablePackageForScheduledException() { }
        public NoSuitablePackageForScheduledException(string message) : base(message) { }
        public NoSuitablePackageForScheduledException(string message, Exception inner) : base(message, inner) { }
        protected NoSuitablePackageForScheduledException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }

        override public string ToString()
        { return "There is no suitable package for scheduled" + "\n" + Message; }
    }
}
