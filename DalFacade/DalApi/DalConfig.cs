using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalApi
{

    /// <summary>
    /// Static class for importing the data of the data layer configuration.
    /// </summary>
    static class DalConfig
    {
        /// <summary>
        /// Structure for representing a configuration package.
        /// </summary>
        internal struct DalPackage
        {
            /// <summary>
            /// The name of the configuration element in the file.
            /// </summary>
            public string Name;
            /// <summary>
            /// The name of the DLL Cube to load.
            /// </summary>
            public string PackageName;
            /// <summary>
            /// The namespace of the loaded class.
            /// </summary>
            public string Namespace;
            /// <summary>
            /// The name of the loaded class.
            /// </summary>
            public string ClassName;
        }

        /// <summary>
        /// Name of the selected configuration element.
        /// </summary>
        internal static string DalName;
        /// <summary>
        /// A dictionary that contains the elements of the configuration that are represented by the name of the element.
        /// </summary>
        internal static Dictionary<string, DalPackage> DalPackages;

        /// <summary>
        /// CTOR
        /// </summary>
        static DalConfig()
        {
            XElement dalConfig = XElement.Load(@"xml\dal-config.xml");
            DalName = dalConfig.Element("dal").Value;
            DalPackages = (from pkg in dalConfig.Element("dal-packages").Elements()
                           let tmpNamespace = pkg.Attribute("namespace")
                           let tmpClassName = pkg.Attribute("class")
                           select new DalPackage()
                           {
                               Name = pkg.Name.ToString(),
                               PackageName = pkg.Value,
                               Namespace = tmpNamespace != null ? tmpNamespace.Value : "Dal",
                               ClassName = tmpClassName != null ? tmpClassName.Value : pkg.Value
                           }
                          ).ToDictionary(p => "" + p.Name, p => p);
        }
    }

    /// <summary>
    /// Exception which represents an exception to the configuration
    /// </summary>
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }
}

