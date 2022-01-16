using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalApi
{
    static class DalConfig
    {
        internal struct DalPackage
        {
            public string Name;
            public string PackageName;
            public string Namespace;
            public string ClassName;
        }

        internal static string DalName;
        internal static Dictionary<string, DalPackage> DalPackages;

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
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }
}

