using System;

namespace SmolStream.StreamerService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Enter Twitch Client ID:");
            var clientId = Console.ReadLine();
            while (clientId.Length != 30)
            {
                Console.WriteLine("Invalid ID, please re-enter:");
                clientId = Console.ReadLine();
            }

            Console.WriteLine("Valid ID entered, continuing...");
            Console.ReadLine();
        }
    }
}