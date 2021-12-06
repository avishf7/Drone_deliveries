using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    /// <summary>
    /// MenuOptions Class is an auxiliary class 
    /// for printing a selection menu on the console
    /// </summary>
    class MenuOptions
    {
        public enum OpeningOptions
        {
            Exit, Add, Update, Print, PrintLists, Distance,
            FirstMenuOption = Exit, LastMenuOption = Distance, Default
        };

        public enum InsertOptions
        {
            Back, Station, Drone, Customer, Package,
            FirstMenuOption = Back, LastMenuOption = Package, Default
        };

        public enum UpdateOptions
        {
            Back, Association, PickingUp, Supply, Charging, Release,
            FirstMenuOption = Back, LastMenuOption = Release, Default
        };

        public enum DisplayOptions
        {
            Back, Station, Drone, Customer, Package, 
            FirstMenuOption = Back, LastMenuOption = Package, Default
        };

        public enum ListViewOptions
        {
            Back, Stations, Drones, Customers, Packages, UnassignedPackages, AvailableForCharging,
            FirstMenuOption = Back, LastMenuOption = AvailableForCharging, Default
        };

        public enum DistanceOptions
        {
            Back, Station, Customer,
            FirstMenuOption = Back, LastMenuOption = Customer, Default
        };


        private static string[] OpeningMenuOptionLine = new string[(int)OpeningOptions.LastMenuOption + 1]
        {
            "Exit",
            "Insert options",
            "Update options",
            "Display options",
            "List View options",
            "Distance options(bonus)"
        };

        private static string[] InsertMenuOptionLine = new string[(int)InsertOptions.LastMenuOption + 1]
        {
            "Back to the main menu",
            "Add station",
            "Add drone",
            "Add customer",
            "Add package"
        };

        private static string[] UpdateMenuOptionLine = new string[(int)UpdateOptions.LastMenuOption + 1]
        {
            "Back to the main menu",
            "Assigning a Package to a Drone",
            "Pickup of a package by a drone",
            "End of supply",
            "Sending a drone for charging at a base station",
            "Releasing a Drone from a Charger",
        };

        private static string[] DispalyMenuOptionLine = new string[(int)DisplayOptions.LastMenuOption + 1]
        {
            "Back to the main menu",
            "Show station",
            "Show drone",
            "Show customer",
            "Show package"
        };

        private static string[] ListViewMenuOptionLine = new string[(int)ListViewOptions.LastMenuOption + 1]
       {
            "Back to the main menu",
            "Show a list of stations",
            "Show the list of dronesList",
            "Show customer list",
            "Show the list of packages",
            "Show a list of packages that have not yet been assigned to the drone",
            "Show of stations with available charging stations",
       };

        private static string[] DistanceMenuOptionLine = new string[(int)DistanceOptions.LastMenuOption + 1]
       {
            "Back to the main menu",
            "Calculate distance from station",
            "Calculate distance from customer"
       };


        public static OpeningOptions PrintOpeningMenu()
        {
            OpeningOptions chosen;

            Console.WriteLine("Menu options:\n");

            for (chosen = OpeningOptions.FirstMenuOption; chosen <= OpeningOptions.LastMenuOption; ++chosen)
                Console.WriteLine("{0,10}\t--\t" + OpeningMenuOptionLine[(int)chosen], (int)chosen);

            Console.Write("\nPlease choose a menu option: ");
            if (int.TryParse(Console.ReadLine(), out int s))
                chosen = (OpeningOptions)s;
            else
                throw new FormatException();

            return chosen;
        }

        public static InsertOptions PrintInsertMenu()
        {
            InsertOptions chosen;

            Console.WriteLine("\nInsert options:\n");

            for (chosen = InsertOptions.FirstMenuOption; chosen <= InsertOptions.LastMenuOption; ++chosen)
                Console.WriteLine("{0,10}\t--\t" + InsertMenuOptionLine[(int)chosen], (int)chosen);

            Console.Write("\nPlease choose an option: ");
            if (int.TryParse(Console.ReadLine(), out int s))
                chosen = (InsertOptions)s;
            else
                throw new FormatException();

            return chosen;
        }

        public static UpdateOptions PrintUpdateMenu()
        {
            UpdateOptions chosen;

            Console.WriteLine("\nUpdate options:\n");

            for (chosen = UpdateOptions.FirstMenuOption; chosen <= UpdateOptions.LastMenuOption; ++chosen)
                Console.WriteLine("{0,10}\t--\t" + UpdateMenuOptionLine[(int)chosen], (int)chosen);

            Console.Write("\nPlease choose an option: ");
            if (int.TryParse(Console.ReadLine(), out int s))
                chosen = (UpdateOptions)s;
            else
                throw new FormatException();

            return chosen;
        }

        public static DisplayOptions PrintDisplayMenu()
        {
            DisplayOptions chosen;

            Console.WriteLine("\nDisplay options:\n");

            for (chosen = DisplayOptions.FirstMenuOption; chosen <= DisplayOptions.LastMenuOption; ++chosen)
                Console.WriteLine("{0,10}\t--\t" + DispalyMenuOptionLine[(int)chosen], (int)chosen);

            Console.Write("\nPlease choose an option: ");
            if (int.TryParse(Console.ReadLine(), out int s))
                chosen = (DisplayOptions)s;
            else
                throw new FormatException();

            
            return chosen;
        }

        public static ListViewOptions PrintListViewMenu()
        {
            ListViewOptions chosen;

            Console.WriteLine("\nList View options:\n");

            for (chosen = ListViewOptions.FirstMenuOption; chosen <= ListViewOptions.LastMenuOption; ++chosen)
                Console.WriteLine("{0,10}\t--\t" + ListViewMenuOptionLine[(int)chosen], (int)chosen);

            Console.Write("\nPlease choose an option: ");
            if (int.TryParse(Console.ReadLine(), out int s))
                chosen = (ListViewOptions)s;
            else
                throw new ArgumentException();

            return chosen;
        }

        public static DistanceOptions PrintDistanceMenu()
        {
            DistanceOptions chosen;

            Console.WriteLine("\nDistance options:\n");

            for (chosen = DistanceOptions.FirstMenuOption; chosen <= DistanceOptions.LastMenuOption; ++chosen)
                Console.WriteLine("{0,10}\t--\t" + DistanceMenuOptionLine[(int)chosen], (int)chosen);

            Console.Write("\nPlease choose an option: ");
            if (int.TryParse(Console.ReadLine(), out int s))
                chosen = (DistanceOptions)s;
            else
                throw new FormatException();

            return chosen;
        }

    }
}
