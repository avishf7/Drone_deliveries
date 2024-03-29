﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    /// <summary>
    /// A class that imports the instance of the data access layer.
    /// </summary>
    public static class DalFactory
    {
        /// <summary>
        /// A function that tries to return the instance of the selected data access layer in the configuration file.
        /// </summary>
        /// <returns>Instance of the data access layer</returns>
        public static IDal GetDal()
        {
            string dalType = DalConfig.DalName;
            DalConfig.DalPackage dalPkg = DalConfig.DalPackages[dalType];
            if (dalPkg.PackageName == null) throw new DalConfigException($"Package {dalType} is not found in packages list in dal-config.xml");

            try { Assembly.Load(dalPkg.PackageName); }
            catch (Exception) { throw new DalConfigException("Failed to load the dal-config.xml file"); }

            Type type = Type.GetType($"{dalPkg.Namespace}.{dalPkg.ClassName}, {dalPkg.PackageName}");
            if (type == null) throw new DalConfigException($"Class {dalPkg.ClassName} was not found in the {dalPkg.PackageName}.dll");

            IDal dal = (IDal)type.GetProperty("Instance",
                      BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            if (dal == null) throw new DalConfigException($"Class {dalPkg.ClassName} is not a singleton or wrong propertry name for Instance");

            return dal;
        }
    }
}
