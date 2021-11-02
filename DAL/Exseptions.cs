using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;



namespace IDAL.DO
{
    [Serializable]
    public class ExistsNumberException : Exception
    {
        public int Capacity { get; private set; }

        public ExistsNumberException() : base() { }
        public ExistsNumberException(string message) : base(message) { }
        public ExistsNumberException(string message, Exception inner) : base(message, inner) { }
        protected ExistsNumberException(SerializationInfo info, StreamingContext context)
     : base(info, context) { }
        // special constructor for our custom exception

        override public string ToString()
        { return "ExistsNumberException:" + Capacity + "\n" + Message; }
    }
}
