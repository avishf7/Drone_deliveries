using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    /// <summary>
    /// Throws when an ID number already exists
    /// </summary>
    [Serializable]
    public class ExistsNumberException : Exception
    {
        public ExistsNumberException() : base() { }
        public ExistsNumberException(string message) : base(message) { }
        public ExistsNumberException(string message, Exception inner) : base(message, inner) { }
        protected ExistsNumberException(SerializationInfo info, StreamingContext context)
     : base(info, context) { }


        override public string ToString()
        { return "Exists number: " + "\n" + Message; }
    }

    /// <summary>
    /// Throws when no ID number exists
    /// </summary>
    [Serializable]
    public class NoNumberFoundException : Exception
    {

        public NoNumberFoundException() : base() { }
        public NoNumberFoundException(string message) : base(message) { }
        public NoNumberFoundException(string message, Exception inner) : base(message, inner) { }
        protected NoNumberFoundException(SerializationInfo info, StreamingContext context)
     : base(info, context) { }

        override public string ToString()
        { return "Number not found: " + "\n" + Message; }
    }

    /// <summary>
    /// Throws when trying to delete an already associated package
    /// </summary>
    [Serializable]
    public class PakcageConnectToDroneException : Exception
    {

        public PakcageConnectToDroneException() : base() { }
        public PakcageConnectToDroneException(string message) : base(message) { }
        public PakcageConnectToDroneException(string message, Exception inner) : base(message, inner) { }
        protected PakcageConnectToDroneException(SerializationInfo info, StreamingContext context)
     : base(info, context) { }

        override public string ToString()
        { return "Can not be deleted: " + "\n" + Message; }
    }

    /// <summary>
    /// Throws when the drone is not available
    /// </summary>
    [Serializable]
    public class DroneNotAvailableException : Exception
    {
        public DroneNotAvailableException() : base() { }

        public DroneNotAvailableException(string message) : base(message) { }

        public DroneNotAvailableException(string message, Exception innerException) : base(message, innerException) { }

        protected DroneNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        override public string ToString()
        { return "Drone is not available: " + "\n" + Message; }
    }

    /// <summary>
    /// Throws when trying to release from a charger a drone that is not charging
    /// </summary>
    [Serializable]
    public class DroneNotMaintenanceException : Exception
    {
        public DroneNotMaintenanceException() : base() { }

        public DroneNotMaintenanceException(string message) : base(message) { }

        public DroneNotMaintenanceException(string message, Exception innerException) : base(message, innerException) { }

        protected DroneNotMaintenanceException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        override public string ToString()
        { return "Drone is not maintenance status: " + "\n" + Message; }
    }

    /// <summary>
    /// Throws when no package has been found that can be associated with the drone
    /// </summary>
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
        { return "There is no suitable package for scheduled: " + "\n" + Message; }
    }

    /// <summary>
    /// Throws when trying to insert an invalid amount (too small)
    /// </summary>
    [Serializable]
    public class TooSmallAmount : Exception
    {
        public TooSmallAmount() { }
        public TooSmallAmount(string message) : base(message) { }
        public TooSmallAmount(string message, Exception inner) : base(message, inner) { }
        protected TooSmallAmount(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }

        override public string ToString()
        { return "Too small amount:" + "\n" + Message; }

    }

    /// <summary>
    /// Throws when there is not enough battery for drone to perform operation
    /// </summary>
    [Serializable]
    public class NotEnoughBattery : Exception
    {
        public NotEnoughBattery() { }
        public NotEnoughBattery(string message) : base(message) { }
        public NotEnoughBattery(string message, Exception inner) : base(message, inner) { }
        protected NotEnoughBattery(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }

        override public string ToString()
        { return "There is not enough battery: " + "\n" + Message; }

    }

    /// <summary>
    /// Throws when there is no package associated with the drone trying to make delivery
    /// </summary>
    [Serializable]
    public class NoPackageAssociatedWithDrone : Exception
    {
        public NoPackageAssociatedWithDrone() { }
        public NoPackageAssociatedWithDrone(string message) : base(message) { }
        public NoPackageAssociatedWithDrone(string message, Exception innerException) : base(message, innerException) { }
        protected NoPackageAssociatedWithDrone(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString()
        { return "  There is no package associated with the drone: " + "\n" + Message; }
    }

    /// <summary>
    /// Throws when trying to pick up a package that has already been collected
    /// </summary>
    [Serializable]
    public class PackageAlreadyCollectedException : Exception
    {
        public PackageAlreadyCollectedException() { }
        public PackageAlreadyCollectedException(string message) : base(message) { }
        public PackageAlreadyCollectedException(string message, Exception inner) : base(message, inner) { }
        protected PackageAlreadyCollectedException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }

        override public string ToString()
        { return "The package registered with the drone has already been collected: " + "\n" + Message; }
    }

    /// <summary>
    /// Throws when trying to deliver a package that has not yet been collected
    /// </summary>
    [Serializable]
    public class PackageNotCollectedException : Exception
    {
        public PackageNotCollectedException() { }
        public PackageNotCollectedException(string message) : base(message) { }
        public PackageNotCollectedException(string message, Exception innerException) : base(message, innerException) { }
        protected PackageNotCollectedException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        override public string ToString()
        { return "The package registered with the drone has not yet been collected: " + "\n" + Message; }
    }

    /// <summary>
    /// When trying to add a package whose destination is invalid
    /// </summary>
    [Serializable]
    public class NotValidTargetException : Exception
    {
        public NotValidTargetException() { }
        public NotValidTargetException(string message) : base(message) { }
        public NotValidTargetException(string message, Exception innerException) : base(message, innerException) { }
        protected NotValidTargetException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        override public string ToString()
        { return "The target is not valid: " + "\n" + Message; }
    }



}



