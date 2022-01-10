using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;



namespace DalApi
{


    [Serializable]
    public class ExistsNumberException : Exception
    {
        public ExistsNumberException() : base() { }
        public ExistsNumberException(string message) : base(message) { }
        public ExistsNumberException(string message, Exception inner) : base(message, inner) { }
        protected ExistsNumberException(SerializationInfo info, StreamingContext context)
     : base(info, context) { }
        // special constructor for our custom exception

        override public string ToString()
        { return "Exists number exception:"+ Message; }
    }

    [Serializable]
    public class NoNumberFoundException : Exception
    {
        public NoNumberFoundException() : base() { }
        public NoNumberFoundException(string message) : base(message) { }
        public NoNumberFoundException(string message, Exception inner) : base(message, inner) { }
        protected NoNumberFoundException(SerializationInfo info, StreamingContext context)
     : base(info, context) { }
        // special constructor for our custom exception

        override public string ToString()
        { return "No number found exeptions:" + Message; }
    }

    [Serializable]
    public class XMLFileLoadCreateException : Exception
    {
        private string filePath;
        private string v;
        private Exception ex;

        public XMLFileLoadCreateException()
        {
        }

        public XMLFileLoadCreateException(string message) : base(message)
        {
        }

        public XMLFileLoadCreateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public XMLFileLoadCreateException(string filePath, string v, Exception ex)
        {
            this.filePath = filePath;
            this.v = v;
            this.ex = ex;
        }

        protected XMLFileLoadCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
