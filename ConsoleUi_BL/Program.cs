using System;
using System.Linq;
using IBL;
using IBL.BO;
using System.Collections.Generic;

namespace ConsoleUiBL
{
    class Program
    {
        static void Main(string[] args)
        {
            IBl bl = new BL.BL();
            MenuOptions.OpeningOptions ch;

            do
            {
                try { ch = MenuOptions.PrintOpeningMenu(); }
                catch (FormatException) { ch = MenuOptions.OpeningOptions.Default; }

                switch (ch)
                {
                    case MenuOptions.OpeningOptions.Exit:
                        Console.WriteLine("\ngood bye!");
                        break;

                    case MenuOptions.OpeningOptions.Add:
                        MenuOptions.InsertOptions IChoice;

                        do
                        {
                            try { IChoice = MenuOptions.PrintInsertMenu(); }
                            catch (FormatException) { IChoice = MenuOptions.InsertOptions.Default; }

                            switch (IChoice)
                            {
                                case MenuOptions.InsertOptions.Back:
                                    Console.WriteLine("\n");

                                    break;
                                case MenuOptions.InsertOptions.Station:
                                    Console.WriteLine("Enter station ID: ");
                                    int stationId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter station name: ");
                                    string name = (Console.ReadLine());
                                    Console.WriteLine("Enter num of Charge Slots: ");
                                    int ChargeSlots = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter longitude of stations adress: ");
                                    double longitude = (int)double.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter lattitude of stations adress: ");
                                    double lattitude = (int)double.Parse(Console.ReadLine());

                                    try
                                    {
                                        bl.AddStation(new()
                                        {
                                            Id = stationId,
                                            Name = name,
                                            FreeChargeSlots = ChargeSlots,
                                            LocationOfStation = new()
                                            {
                                                Longitude = longitude,
                                                Lattitude = lattitude
                                            }
                                        });

                                        //Output that displays the success of a request:
                                        bl.GetStations().ToList().ForEach(st => Console.WriteLine(st));
                                    }
                                    catch (ExistsNumberException ex)
                                    {
                                        Console.WriteLine(ex);
                                    }

                                    break;

                                case MenuOptions.InsertOptions.Drone:
                                    Console.WriteLine("Enter drone ID: ");
                                    int droneId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter drone model: ");
                                    string model = (Console.ReadLine());
                                    Console.WriteLine("Enter drone Max Weight - To Light enter 0, to Medum enter 1 and to Heavy enter 2: ");
                                    Weight maxWeight = (Weight)int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter a station ID for initial charging: ");
                                    int firstStationId = int.Parse(Console.ReadLine());

                                    try
                                    {
                                        bl.AddDrone(
                                            new()
                                            {
                                                Id = droneId,
                                                Model = model,
                                                MaxWeight = maxWeight,
                                            },
                                        firstStationId);

                                        //Output that displays the success of a request:
                                        bl.GetDrones().ToList().ForEach(dr => Console.WriteLine(dr));
                                    }
                                    catch (ExistsNumberException ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
                                    catch (NoNumberFoundException ex)
                                    {
                                        Console.WriteLine(ex);
                                    }

                                    break;

                                case MenuOptions.InsertOptions.Customer:
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
                                    try
                                    {
                                        bl.AddCustomer(new()
                                        {
                                            Id = cusId,
                                            Name = cusName,
                                            Phone = phone,
                                            CustomerLocation = new()
                                            {
                                                Longitude = cusLongitude,
                                                Lattitude = cusLattitude
                                            }
                                        });

                                        //Output that displays the success of a request:
                                        bl.GetCustomers().ToList().ForEach(cus => Console.WriteLine(cus));
                                    }
                                    catch (ExistsNumberException ex)
                                    {
                                        Console.WriteLine(ex);
                                    }

                                    break;

                                case MenuOptions.InsertOptions.Package:
                                    Console.WriteLine("Enter target ID: ");
                                    int sendersId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter sender ID: ");
                                    int targetsId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter package Weight - To Light enter 0, to Medum enter 1 and to Heavy enter 2: ");
                                    Weight weight = (Weight)int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter pariority - To Normal enter 0, to Fast enter 1 and to Emerency enter 2: ");
                                    Priorities priority = (Priorities)int.Parse(Console.ReadLine());
                                    try
                                    {
                                        bl.AddPackage(new()
                                        {
                                            SenderCustomerInPackage = new()
                                            {
                                                CustomerId = sendersId,
                                            },
                                            TargetCustomerInPackage = new()
                                            {
                                                CustomerId = targetsId,
                                            },
                                            Weight = weight,
                                            Priority = priority,
                                        });

                                        //Output that displays the success of a request:
                                        bl.GetPackages().ToList().ForEach(pck => Console.WriteLine(pck));
                                    }
                                    catch (ExistsNumberException ex)
                                    {
                                        Console.WriteLine(ex);
                                    }

                                    break;

                                default:
                                    Console.WriteLine("\nERROR: invalid choice\n");
                                    break;
                            }

                        } while ((int)IChoice != 0);

                        break;

                    case MenuOptions.OpeningOptions.Update:
                        MenuOptions.UpdateOptions UChoice;

                        do
                        {
                            try { UChoice = MenuOptions.PrintUpdateMenu(); }
                            catch (FormatException) { UChoice = MenuOptions.UpdateOptions.Default; }

                            switch (UChoice)
                            {
                                case MenuOptions.UpdateOptions.Back:
                                    Console.WriteLine("\n");

                                    break;

                                case MenuOptions.UpdateOptions.Station:
                                    Console.WriteLine("Enter station ID: ");
                                    int stationId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter new station name: ");
                                    string name = (Console.ReadLine());
                                    Console.WriteLine("Enter new num of Charge Slots: ");
                                    var chSl = Console.ReadLine();
                                    int ChargeSlots = int.Parse(chSl != "" ? chSl : "-1");

                                    try
                                    {
                                        bl.UpdateStation(stationId, name, ChargeSlots);

                                        //Output that displays the success of a request:
                                        Console.WriteLine(bl.GetStation(stationId));

                                    }
                                    catch (NoNumberFoundException ex) { Console.WriteLine(ex); }
                                    catch (TooSmallAmount ex) { Console.WriteLine(ex); }



                                    break;
                                case MenuOptions.UpdateOptions.Drone:
                                    Console.WriteLine("Enter drone ID: ");
                                    int droneId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter drone model: ");
                                    string model = (Console.ReadLine());

                                    try
                                    {
                                        bl.UpdateDrone(droneId, model);

                                        //Output that displays the success of a request:
                                        Console.WriteLine(bl.GetDrone(droneId));
                                    }
                                    catch (NoNumberFoundException ex) { Console.WriteLine(ex); }

                                    break;
                                case MenuOptions.UpdateOptions.Customer:
                                    Console.WriteLine("Enter customer ID: ");
                                    int cusId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter customer name: ");
                                    string cusName = (Console.ReadLine());
                                    Console.WriteLine("Enter customer phone: ");
                                    string phone = (Console.ReadLine());

                                    try
                                    {
                                        bl.UpdateCustomer(cusId, cusName, phone);

                                        //Output that displays the success of a request:
                                        Console.WriteLine(bl.GetCustomer(cusId));
                                    }
                                    catch (NoNumberFoundException ex) { Console.WriteLine(ex); }

                                    break;
                                case MenuOptions.UpdateOptions.Association:
                                    Console.WriteLine("Enter drone ID: ");
                                    int assoDroneId = int.Parse(Console.ReadLine());


                                    try
                                    {
                                        bl.packageAssigning(assoDroneId);

                                        //Output that displays the success of a request:
                                        Console.WriteLine(bl.GetDrone(assoDroneId));
                                    }
                                    catch (NoNumberFoundException ex) { Console.WriteLine(ex); }
                                    catch (NoSuitablePackageForScheduledException ex) { Console.WriteLine(ex); }
                                    catch (DroneNotAvailableException ex) { Console.WriteLine(ex); }

                                    break;
                                case MenuOptions.UpdateOptions.PickingUp:
                                    Console.WriteLine("Enter drone ID: ");
                                    int pickUpDroneId = int.Parse(Console.ReadLine());
                                    try
                                    {
                                        bl.PickUp(pickUpDroneId);

                                        //Output that displays the success of a request:
                                        Console.WriteLine(bl.GetDrone(pickUpDroneId));
                                    }
                                    catch (NoNumberFoundException ex) { Console.WriteLine(ex); }
                                    catch (NoPackageAssociatedWithDrone ex) { Console.WriteLine(ex); }
                                    catch (PackageAlreadyCollectedException ex) { Console.WriteLine(ex); }


                                    break;
                                case MenuOptions.UpdateOptions.Supply:
                                    Console.WriteLine("Enter drone ID: ");
                                    int supDroneId = int.Parse(Console.ReadLine());

                                    try
                                    {
                                        bl.Deliver(supDroneId);

                                        //Output that displays the success of a request:
                                        Console.WriteLine(bl.GetDrone(supDroneId));
                                    }
                                    catch (NoNumberFoundException ex) { Console.WriteLine(ex); }
                                    catch (NoPackageAssociatedWithDrone ex) { Console.WriteLine(ex); }
                                    catch (PackageNotCollectedException ex) { Console.WriteLine(ex); }

                                    break;
                                case MenuOptions.UpdateOptions.Charging:
                                    Console.WriteLine("Enter drone ID : ");
                                    int chDroneId = (int.Parse(Console.ReadLine()));

                                    try
                                    {
                                        bl.SendDroneForCharge(chDroneId);

                                        //Output that displays the success of a request:
                                        Console.WriteLine(bl.GetDrone(chDroneId));
                                    }
                                    catch (NoNumberFoundException ex) { Console.WriteLine(ex); }
                                    catch (DroneNotAvailableException ex) { Console.WriteLine(ex); }
                                    catch (NotEnoughBattery ex) { Console.WriteLine(ex); }

                                    break;
                                case MenuOptions.UpdateOptions.Release:

                                    Console.WriteLine("Enter drone ID : ");
                                    int reDroneId = (int.Parse(Console.ReadLine()));
                                    Console.WriteLine("Enter the number of charging hours:");
                                    int h = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter the number of charging minutes:");
                                    int m = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter the number of charging seconds:");
                                    int s = int.Parse(Console.ReadLine());

                                    try
                                    {
                                        bl.RealeseDroneFromCharge(reDroneId, new(h, m, s));

                                        //Output that displays the success of a request:
                                        Console.WriteLine(bl.GetDrone(reDroneId));
                                    }
                                    catch (NoNumberFoundException ex) { Console.WriteLine(ex); }
                                    catch (DroneNotMaintenanceException ex) { Console.WriteLine(ex); }

                                    break;

                                default:
                                    Console.WriteLine("\nERROR: invalid choice\n");
                                    break;
                            }

                        } while ((int)UChoice != 0);

                        break;

                    case MenuOptions.OpeningOptions.Print:
                        MenuOptions.DisplayOptions DChoice;

                        do
                        {
                            try { DChoice = MenuOptions.PrintDisplayMenu(); }
                            catch (FormatException) { DChoice = MenuOptions.DisplayOptions.Default; }

                            switch (DChoice)
                            {
                                case MenuOptions.DisplayOptions.Back:
                                    Console.WriteLine("\n");

                                    break;
                                case MenuOptions.DisplayOptions.Station:
                                    Console.WriteLine("Enter station ID to see: ");
                                    try { Console.WriteLine("\n" + bl.GetStation(int.Parse(Console.ReadLine()))); }
                                    catch (NoNumberFoundException ex) { Console.WriteLine(ex); }

                                    break;
                                case MenuOptions.DisplayOptions.Drone:
                                    Console.WriteLine("Enter drone ID to see: ");
                                    try { Console.WriteLine("\n" + bl.GetDrone(int.Parse(Console.ReadLine()))); }
                                    catch (NoNumberFoundException ex) { Console.WriteLine(ex); }

                                    break;
                                case MenuOptions.DisplayOptions.Customer:
                                    Console.WriteLine("Enter customer ID to see: ");
                                    try { Console.WriteLine("\n" + bl.GetCustomer(int.Parse(Console.ReadLine()))); }
                                    catch (NoNumberFoundException ex) { Console.WriteLine(ex); }

                                    break;
                                case MenuOptions.DisplayOptions.Package:
                                    Console.WriteLine("Enter package ID to see: ");
                                    try { Console.WriteLine("\n" + bl.GetPackage(int.Parse(Console.ReadLine()))); }
                                    catch (NoNumberFoundException ex) { Console.WriteLine(ex); }


                                    break;

                                default:
                                    Console.WriteLine("\nERROR: invalid choice\n");
                                    break;
                            }

                        } while ((int)DChoice != 0);

                        break;

                    case MenuOptions.OpeningOptions.PrintLists:
                        MenuOptions.ListViewOptions LVChoice;

                        do
                        {
                            try { LVChoice = MenuOptions.PrintListViewMenu(); }
                            catch (FormatException) { LVChoice = MenuOptions.ListViewOptions.Default; }

                            switch (LVChoice)
                            {
                                case MenuOptions.ListViewOptions.Back:
                                    Console.WriteLine("\n");

                                    break;
                                case MenuOptions.ListViewOptions.Stations:
                                    var st1 = bl.GetStations();
                                    foreach (var st in st1)
                                    {
                                        Console.WriteLine("\n" + st);
                                    }

                                    break;
                                case MenuOptions.ListViewOptions.Drones:
                                    foreach (var dr in bl.GetDrones())
                                    {
                                        Console.WriteLine("\n" + dr);
                                    }

                                    break;
                                case MenuOptions.ListViewOptions.CUSTOMERS:
                                    foreach (var cus in bl.GetCustomers())
                                    {
                                        Console.WriteLine("\n" + cus);
                                    }

                                    break;
                                case MenuOptions.ListViewOptions.Packages:
                                    foreach (var pck in bl.GetPackages())
                                    {
                                        Console.WriteLine("\n" + pck);
                                    }

                                    break;
                                case MenuOptions.ListViewOptions.UnassignedPackages:
                                    foreach (var pck in bl.GetPackages(pck => pck.PackageStatus == PackageStatus.Defined))
                                    {
                                        Console.WriteLine("\n" + pck);
                                    }

                                    break;
                                case MenuOptions.ListViewOptions.AvailableForCharging:
                                    foreach (var st in bl.GetStations(st => st.SeveralAvailableChargingStations != 0))
                                    {
                                        Console.WriteLine("\n" + st);
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
