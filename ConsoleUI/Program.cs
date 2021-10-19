using System;
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
                                    break;
                                case MenuOptions.UpdateOptions.SUPPLY:
                                    break;
                                case MenuOptions.UpdateOptions.CHARGING:
                                    break;
                                case MenuOptions.UpdateOptions.RELEASE:
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
                                    break;
                                case MenuOptions.DisplayOptions.DRONE:
                                    break;
                                case MenuOptions.DisplayOptions.CUSTOMER:
                                    break;
                                case MenuOptions.DisplayOptions.PACKAGE:
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
                                    break;
                                case MenuOptions.ListViewOptions.DRONES:
                                    break;
                                case MenuOptions.ListViewOptions.CUSTOMERS:
                                    break;
                                case MenuOptions.ListViewOptions.PACKAGES:
                                    break;
                                case MenuOptions.ListViewOptions.UNASSIGNED_STATIONS:
                                    break;
                                case MenuOptions.ListViewOptions.AVAILABLE_FOR_CHARGING:
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
