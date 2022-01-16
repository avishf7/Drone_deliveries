﻿using DalApi;
using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL
    {
        public void AddPackage(Package package)
        {

            int senderId = package.SenderCustomerInPackage.CustomerId;
            int targetId = package.TargetCustomerInPackage.CustomerId;

            if (senderId == targetId)
                throw new NotValidTargetException("The destination is the same as the sender");

            try
            {
                dal.AddPackage(new DO.Package
                {
                    SenderId = senderId,
                    TargetId = targetId,
                    Weight = (DO.Weight)package.Weight,
                    Priority = (DO.Priorities)package.Priority,
                    DroneId = 0,
                    Requested = DateTime.Now,
                    Scheduled = null,
                    PickedUp = null,
                    Delivered = null
                });
            }
            catch (DalApi.ExistsNumberException ex)
            {
                throw new BlApi.ExistsNumberException("Package already exists ", ex);
            }
        }

        public Package GetPackage(int packageId)
        {
            try
            {
                var DoPackage = dal.GetPackage(packageId);
                var dr = dronesList.Find(x => x.PackageNumber == packageId);

                Package BoPackage = new()
                {
                    Id = DoPackage.Id,
                    Weight = (Weight)DoPackage.Weight,
                    Priority = (Priorities)DoPackage.Priority,
                    Scheduled = DoPackage.Scheduled,
                    Requested = DoPackage.Requested,
                    PickedUp = DoPackage.PickedUp,
                    Delivered = DoPackage.Delivered,
                    DroneInPackage = dr != null ? new()
                    {
                        Id = dr.Id,
                        BatteryStatus = dr.BatteryStatus,
                        LocationOfDrone = dr.LocationOfDrone
                    }
                    : null,
                    SenderCustomerInPackage = dal.GetCustomer(DoPackage.SenderId).GetCusomerInPackage(),
                    TargetCustomerInPackage = dal.GetCustomer(DoPackage.TargetId).GetCusomerInPackage()
                };

                return BoPackage;
            }
            catch (DalApi.NoNumberFoundException ex)
            {
                throw new BlApi.NoNumberFoundException("Package ID not found", ex);
            }
        }

        public IEnumerable<PackageToList> GetPackages(Predicate<PackageToList> predicate = null)
        {
            return dal.GetPackages().Select(pck => new PackageToList()
            {
                Id = pck.Id,
                SenderName = dal.GetCustomer(pck.SenderId).Name,
                TargetName = dal.GetCustomer(pck.TargetId).Name,
                Priority = (Priorities)pck.Priority,
                Weight = (Weight)pck.Weight,
                PackageStatus = pck.Delivered != null ? PackageStatus.Provided :
                                pck.PickedUp != null ? PackageStatus.Collected :
                                pck.Scheduled != null ? PackageStatus.Associated :
                                PackageStatus.Defined
            }).Where(pck => predicate != null ? predicate(pck) : true);

        }

        public void DeletePackage(int id)
        {
            DO.Package DoPackage;

            try { DoPackage = dal.GetPackage(id); } catch (DalApi.NoNumberFoundException) { throw new BlApi.NoNumberFoundException(); }
            if (DoPackage.Scheduled != null)
                throw new PakcageConnectToDroneException("Package in transfar");

            dal.DeletePackage(id);

        }
    }
}
