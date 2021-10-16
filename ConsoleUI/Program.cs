using System;
using IDAL.DO;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
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
                                    break;
                                case MenuOptions.InsertOptions.DRONE:
                                    break;
                                case MenuOptions.InsertOptions.CUSTOMER:
                                    break;
                                case MenuOptions.InsertOptions.PACKAGE:
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
