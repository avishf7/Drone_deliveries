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
        public XMLFileLoadCreateException() : base() { }
        public XMLFileLoadCreateException(string message) : base(message) { }
        public XMLFileLoadCreateException(string message, Exception inner) : base(message, inner) { }
        protected XMLFileLoadCreateException(SerializationInfo info, StreamingContext context)
     : base(info, context) { }
        // special constructor for our custom exception

        override public string ToString()
        { return "Exists XML exception:" + Message; }
    }

    //[Serializable]
    //public class XMLFileLoadCreateException : Exception
    //{
    //    public XMLFileLoadCreateException() : base() { }
    //    public XMLFileLoadCreateException(string message) : base(message) { }
    //    public XMLFileLoadCreateException(string message, Exception inner) : base(message, inner) { }
    //    protected XMLFileLoadCreateException(SerializationInfo info, StreamingContext context)
    // : base(info, context) { }
    //    // special constructor for our custom exception

    //    override public string ToString()
    //    { return "Exists XML exception:" + Message; }
    //}


}
