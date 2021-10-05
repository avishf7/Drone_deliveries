using System;

namespace targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
             Welcome9738();
            Console.ReadKey();
        }

        private static void Welcome9738()
        {
            Console.WriteLine("Enter your name: ");
            string s = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", s);
        }

        static partial void Welcome8790();

    }
}
