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
            public int Capacity { get; private set; }

            public ExistsNumberException() : base() { }
            public ExistsNumberException(string message) : base(message) { }
            public ExistsNumberException(string message, Exception inner) : base(message, inner) { }
            protected ExistsNumberException(SerializationInfo info, StreamingContext context)
         : base(info, context) { }
            // special constructor for our custom exception

            override public string ToString()
            { return "Exists number exception:" + Capacity + "\n" + Message; }
        }

        [Serializable]
        public class NoNumberFoundException : Exception
        {
            public int Capacity { get; private set; }

            public NoNumberFoundException() : base() { }
            public NoNumberFoundException(string message) : base(message) { }
            public NoNumberFoundException(string message, Exception inner) : base(message, inner) { }
            protected NoNumberFoundException(SerializationInfo info, StreamingContext context)
         : base(info, context) { }
            // special constructor for our custom exception

            override public string ToString()
            { return "No number found exeptions:" + Capacity + "\n" + Message; }
        }    
}
