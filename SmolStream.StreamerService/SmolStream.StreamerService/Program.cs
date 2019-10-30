using RestSharp;
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

            var twitchClient = new RestClient("https://api.twitch.tv/helix");
            var request = new RestRequest("streams", Method.GET);
            request.AddHeader("Client-ID", clientId);
            request.AddParameter("first", "100");

            var response = twitchClient.Execute(request);

            Console.WriteLine("Top 20 Streams:");
            Console.WriteLine(response.Content);
            Console.ReadLine();
        }
    }
}