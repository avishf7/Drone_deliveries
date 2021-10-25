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
            EXIT, ADD, UPDATE, PRINT, PRINT_LISTS, DISTANCE,
            FIRST_MENU_OPTION = EXIT, LAST_MENU_OPTION = DISTANCE, DEFAULT
        };

        public enum InsertOptions
        {
            BACK, STATION, DRONE, CUSTOMER, PACKAGE,
            FIRST_MENU_OPTION = BACK, LAST_MENU_OPTION = PACKAGE, DEFAULT
        };

        public enum UpdateOptions
        {
            BACK, ASSOCIATION, PICKING_UP, SUPPLY, CHARGING, RELEASE,
            FIRST_MENU_OPTION = BACK, LAST_MENU_OPTION = RELEASE, DEFAULT
        };

        public enum DisplayOptions
        {
            BACK, STATION, DRONE, CUSTOMER, PACKAGE, 
            FIRST_MENU_OPTION = BACK, LAST_MENU_OPTION = PACKAGE, DEFAULT
        };

        public enum ListViewOptions
        {
            BACK, STATIONS, DRONES, CUSTOMERS, PACKAGES, UNASSIGNED_PACKAGES, AVAILABLE_FOR_CHARGING,
            FIRST_MENU_OPTION = BACK, LAST_MENU_OPTION = AVAILABLE_FOR_CHARGING, DEFAULT
        };

        public enum DistanceOptions
        {
            BACK, STATION, CUSTOMER,
            FIRST_MENU_OPTION = BACK, LAST_MENU_OPTION = CUSTOMER, DEFAULT
        };


        private static string[] OpeningMenuOptionLine = new string[(int)OpeningOptions.LAST_MENU_OPTION + 1]
        {
            "Exit",
            "Insert options",
            "Update options",
            "Display options",
            "List View options",
            "Distance options(bonus)"
        };

        private static string[] InsertMenuOptionLine = new string[(int)InsertOptions.LAST_MENU_OPTION + 1]
        {
            "Back to the main menu",
            "Add station",
            "Add drone",
            "Add customer",
            "Add package"
        };

        private static string[] UpdateMenuOptionLine = new string[(int)UpdateOptions.LAST_MENU_OPTION + 1]
        {
            "Back to the main menu",
            "Assigning a Package to a Drone",
            "Pickup of a package by a drone",
            "End of supply",
            "Sending a drone for charging at a base station",
            "Releasing a Drone from a Charger",
        };

        private static string[] DispalyMenuOptionLine = new string[(int)DisplayOptions.LAST_MENU_OPTION + 1]
        {
            "Back to the main menu",
            "Show station",
            "Show drone",
            "Show customer",
            "Show package"
        };

        private static string[] ListViewMenuOptionLine = new string[(int)ListViewOptions.LAST_MENU_OPTION + 1]
       {
            "Back to the main menu",
            "Show a list of stations",
            "Show the list of drones",
            "Show customer list",
            "Show the list of packages",
            "Show a list of packages that have not yet been assigned to the drone",
            "Show of stations with available charging stations",
       };

        private static string[] DistanceMenuOptionLine = new string[(int)DistanceOptions.LAST_MENU_OPTION + 1]
       {
            "Back to the main menu",
            "Calculate distance from station",
            "Calculate distance from customer"
       };


        public static OpeningOptions PrintOpeningMenu()
        {
            OpeningOptions chosen;

            Console.WriteLine("Menu options:\n");

            for (chosen = OpeningOptions.FIRST_MENU_OPTION; chosen <= OpeningOptions.LAST_MENU_OPTION; ++chosen)
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

            for (chosen = InsertOptions.FIRST_MENU_OPTION; chosen <= InsertOptions.LAST_MENU_OPTION; ++chosen)
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

            for (chosen = UpdateOptions.FIRST_MENU_OPTION; chosen <= UpdateOptions.LAST_MENU_OPTION; ++chosen)
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

            for (chosen = DisplayOptions.FIRST_MENU_OPTION; chosen <= DisplayOptions.LAST_MENU_OPTION; ++chosen)
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

            for (chosen = ListViewOptions.FIRST_MENU_OPTION; chosen <= ListViewOptions.LAST_MENU_OPTION; ++chosen)
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

            for (chosen = DistanceOptions.FIRST_MENU_OPTION; chosen <= DistanceOptions.LAST_MENU_OPTION; ++chosen)
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
