using System;
using System.Collections.Generic;
using IDAL.DO;


namespace ConsoleUI
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
            DalObject.DalObject dalObject = new();
            MenuOptions.OpeningOptions ch;

            
            do
            {
                try { ch = MenuOptions.PrintOpeningMenu(); }
                catch (FormatException) { ch = MenuOptions.OpeningOptions.DEFAULT; }

                switch (ch)
                {
                    case MenuOptions.OpeningOptions.EXIT:
                        Console.WriteLine("\ngood bye!");
                        break;

                    case MenuOptions.OpeningOptions.ADD:
                        MenuOptions.InsertOptions IChoice;

                        do
                        {
                            try { IChoice = MenuOptions.PrintInsertMenu(); }
                            catch (FormatException) { IChoice = MenuOptions.InsertOptions.DEFAULT; }

                            switch (IChoice)
                            {
                                case MenuOptions.InsertOptions.BACK:
                                    Console.WriteLine("\n");
                                    break;
                                case MenuOptions.InsertOptions.STATION:
                                    Console.WriteLine("Enter station ID: ");
                                    int stationId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter station name: ");
                                    string name = (Console.ReadLine());
                                    Console.WriteLine("Enter num of free station: ");
                                    int numOfFreeStation = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter longitude of stations adress: ");
                                    double longitude = (int)double.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter lattitude of stations adress: ");
                                    double lattitude = (int)double.Parse(Console.ReadLine());

                                    dalObject.AddStation(stationId, name, numOfFreeStation, longitude, lattitude);
                                    break;
                                case MenuOptions.InsertOptions.DRONE:
                                    Console.WriteLine("Enter drone ID: ");
                                    int droneId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter drone model: ");
                                    string model = (Console.ReadLine());
                                    Console.WriteLine("Enter drone Weight - To LIGHT enter 0, to MEDIUM enter 1 and to HEAVY enter 2: ");
                                    Weight maxweight = (Weight)int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter drone status - To  AVAILABLE  enter 0, to MAINTENANCE enter 1 and to DELIVERY enter 2: ");
                                    DroneStatuses status = (DroneStatuses)int.Parse(Console.ReadLine());

                                    dalObject.AddDrone(droneId, model, maxweight, status);
                                    break;
                                case MenuOptions.InsertOptions.CUSTOMER:
                                    Console.WriteLine("Enter customer ID: ");
                                    int cusId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter customer name: ");
                                    string cusName = (Console.ReadLine());
                                    Console.WriteLine("Enter customer phone: ");
                                    string phone = (Console.ReadLine());
                                    Console.WriteLine("Enter longitude of customers adress: ");
                                    double cusLongitude = (int)double.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter lattitude of customers adress: ");
                                    double cusLattitude = (int)double.Parse(Console.ReadLine());

                                    dalObject.AddCustomer(cusId, cusName, phone, cusLongitude, cusLattitude);
                                    break;
                                case MenuOptions.InsertOptions.PACKAGE:
                                    Console.WriteLine("Enter package ID: ");
                                    int id = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter target ID: ");
                                    int sendersId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter sender ID: ");
                                    int targetsId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter package Weight - To LIGHT enter 0, to MEDIUM enter 1 and to HEAVY enter 2: ");
                                    Weight weight = (Weight)int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter pariority - To NORMAL enter 0, to FAST enter 1 and to EMERENCY enter 2: ");
                                    Priorities priority = (Priorities)int.Parse(Console.ReadLine());

                                    dalObject.AddPackage(sendersId, targetsId, weight, priority);
                                    break;

                                default:
                                    Console.WriteLine("\nERROR: invalid choice\n");
                                    break;
                            }

                        } while ((int)IChoice != 0);

                        break;

                    case MenuOptions.OpeningOptions.UPDATE:
                        MenuOptions.UpdateOptions UChoice;

                        do
                        {
                            try { UChoice = MenuOptions.PrintUpdateMenu(); }
                            catch (FormatException) { UChoice = MenuOptions.UpdateOptions.DEFAULT; }

                            switch (UChoice)
                            {
                                case MenuOptions.UpdateOptions.BACK:
                                    Console.WriteLine("\n");
                                    break;
                                case MenuOptions.UpdateOptions.ASSOCIATION:
                                    Console.WriteLine("Enter package ID  for associating: ");
                                    int id = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter drone ID: ");
                                    int droneId = int.Parse(Console.ReadLine());

                                    dalObject.ConnectedPackagToDrone(id, droneId);
                                    break;
                                case MenuOptions.UpdateOptions.PICKING_UP:
                                    Console.WriteLine("Enter package ID for picking up: ");
                                    dalObject.PickUp(int.Parse(Console.ReadLine()));
                                    break;
                                case MenuOptions.UpdateOptions.SUPPLY:
                                    Console.WriteLine("Enter package ID for supply : ");
                                    dalObject.Deliver(int.Parse(Console.ReadLine()));
                                    break;
                                case MenuOptions.UpdateOptions.CHARGING:
                                    Console.WriteLine("Enter drone ID for charge : ");
                                   int drId =  (int.Parse(Console.ReadLine()));
                                    Console.WriteLine("Enter sttion ID for charge : ");
                                    int stId = (int.Parse(Console.ReadLine()));
                                    dalObject.SendDroneForCharge(drId, stId);
                                    break;
                                case MenuOptions.UpdateOptions.RELEASE:
                                    Console.WriteLine("Enter drone ID for release : ");
                                    dalObject.RealeseDroneFromCharge(int.Parse(Console.ReadLine()));

                                    break;

                                default:
                                    Console.WriteLine("\nERROR: invalid choice\n");
                                    break;
                            }

                        } while ((int)UChoice != 0);

                        break;

                    case MenuOptions.OpeningOptions.PRINT:
                        MenuOptions.DisplayOptions DChoice;

                        do
                        {
                            try { DChoice = MenuOptions.PrintDisplayMenu(); }
                            catch (FormatException) { DChoice = MenuOptions.DisplayOptions.DEFAULT; }

                            switch (DChoice)
                            {
                                case MenuOptions.DisplayOptions.BACK:
                                    Console.WriteLine("\n");
                                    break;
                                case MenuOptions.DisplayOptions.STATION:
                                    Console.WriteLine("Enter statio ID to see: ");
                                    dalObject.GetStation(int.Parse(Console.ReadLine()));
                                    break;
                                case MenuOptions.DisplayOptions.DRONE:
                                    Console.WriteLine("Enter drone ID to see: ");
                                    dalObject.GetDrone(int.Parse(Console.ReadLine()));
                                    break;
                                case MenuOptions.DisplayOptions.CUSTOMER:
                                    Console.WriteLine("Enter customer ID to see: ");
                                    dalObject.GetCustomer(int.Parse(Console.ReadLine()));
                                    break;
                                case MenuOptions.DisplayOptions.PACKAGE:
                                    Console.WriteLine("Enter package ID to see: ");
                                    dalObject.GetPackage(int.Parse(Console.ReadLine()));
                                    break;

                                default:
                                    Console.WriteLine("\nERROR: invalid choice\n");
                                    break;
                            }

                        } while ((int)DChoice != 0);

                        break;

                    case MenuOptions.OpeningOptions.PRINT_LISTS:
                        MenuOptions.ListViewOptions LVChoice;

                        do
                        {
                            try { LVChoice = MenuOptions.PrintListViewMenu(); }
                            catch (FormatException) { LVChoice = MenuOptions.ListViewOptions.DEFAULT; }

                            switch (LVChoice)
                            {
                                case MenuOptions.ListViewOptions.BACK:
                                    Console.WriteLine("\n");
                                    break;
                                case MenuOptions.ListViewOptions.STATIONS:
                                    foreach (var st in dalObject.GetStations())
                                    {
                                        Console.WriteLine(st + "\n");
                                    }
                                    break;
                                case MenuOptions.ListViewOptions.DRONES:
                                    foreach (var dr in dalObject.GetDrones())
                                    {
                                        Console.WriteLine(dr + "\n");
                                    }
                                    break;
                                case MenuOptions.ListViewOptions.CUSTOMERS:
                                    foreach (var cus in dalObject.GetCustomers())
                                    {
                                        Console.WriteLine(cus + "\n");
                                    }
                                    break;
                                case MenuOptions.ListViewOptions.PACKAGES:
                                    foreach (var pck in dalObject.GetPackages())
                                    {
                                        Console.WriteLine(pck + "\n");
                                    }
                                    break;
                                case MenuOptions.ListViewOptions.UNASSIGNED_PACKAGES:
                                    foreach (var pck in dalObject.GetNotScheduledPackages())
                                    {
                                        Console.WriteLine(pck + "\n");
                                    }
                                    break;
                                case MenuOptions.ListViewOptions.AVAILABLE_FOR_CHARGING:
                                    foreach (var st in dalObject.GetFreeStations())
                                    {
                                        Console.WriteLine(st + "\n");
                                    }
                                    break;
                                default:
                                    Console.WriteLine("\nERROR: invalid choice\n");
                                    break;
                            }

                        } while ((int)LVChoice != 0);

                        break;

                    default:
                        Console.WriteLine("\nERROR: invalid choice\n");
                        break;
                }

            }
            while ((int)ch != 0);

                Console.Read();

            }
    }
}
