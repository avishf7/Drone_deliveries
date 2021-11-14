using IDAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IBL.BO
{
    public class BL
    {
        Random rd = new Random();



        List <DroneToList> DroneLists;
        IDal dal;
        internal static double DroneAvailable;
        internal static double LightWeight;
        internal static double MediumWeight;
        internal static double HeavyWeight;
        internal static double ChargingRate;

        public BL()
        {
            DroneLists = new();
            dal = new DalObject.DalObject();
            List<double> tmp = dal.ChargingRequest();
            DroneAvailable = tmp[0];
            LightWeight = tmp[1];
            MediumWeight = tmp[2];
            HeavyWeight = tmp[3];
            ChargingRate = tmp[4];

            //הבאת רשימת הרחפנים משכבת הנתונים
            List<IDAL.DO.Drone> drones = dal.GetDrones().ToList();
            for (int i = 0; i < drones.Count; i++)
            {
                DroneLists[i].Id = drones[i].Id;
            }


            List<IDAL.DO.Package> packages = dal.GetPackages(x=>x.DroneId!=0 && x.Delivered==DateTime.MinValue).ToList();
            foreach (var item in DroneLists)
            {
                int index = packages.FindIndex(x => x.DroneId == item.Id);
                if (index != -1)
                {
                    item.DroneStatus = DroneStatuses.SENDERING;
                    int index1 = packages.FindIndex(x => x.PickedUp == DateTime.MinValue);
                   // int index2 = packages.FindIndex(x => x.Delivered == DateTime.MinValue);
                    if (index1==-1)
                    {
                        //item.Location =בתחנה הקרובה לשולח;
                    }
                    else
                    {
                      //  item.Location =במיקום השולח;
                    }
                    item.BatteryStatus = rd.Next((int)20.0, (int)100.0);                 
                }
                else
                {
                //    item.DroneStatus = rd.Next(DroneStatuses.0,1);
                }
            }     
            int index2=drones.FindIndex(x=> x.)
        }
    }
}
