using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class BlExeptions
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
        public class NoNumberFoundExeptions : Exception
        {
            public int Capacity { get; private set; }

            public NoNumberFoundExeptions() : base() { }
            public NoNumberFoundExeptions(string message) : base(message) { }
            public NoNumberFoundExeptions(string message, Exception inner) : base(message, inner) { }
            protected NoNumberFoundExeptions(SerializationInfo info, StreamingContext context)
         : base(info, context) { }
            // special constructor for our custom exception

            override public string ToString()
            { return "No number found exeptions:" + Capacity + "\n" + Message; }
        }

    }
}
