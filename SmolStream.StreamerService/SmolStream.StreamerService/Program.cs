using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmolStream.StreamerService
{
    class Program
    {
        private static readonly int _targetViewerCount = 150;
        private static readonly int _firstValue = 100;
        private static readonly string _twitchUrl = "https://api.twitch.tv/helix";
        private static List<Game> _games = new List<Game>();

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

            var twitchClient = new RestClient(_twitchUrl);

            GetGames(clientId, twitchClient);

            GetStreams(clientId, twitchClient);
        }

        private static void GetGames(string clientId, RestClient twitchClient)
        {
            var request = new RestRequest("games/top", Method.GET);
            request.AddHeader("Client-ID", clientId);
            request.AddParameter("first", _firstValue);

            var response = twitchClient.Execute(request);
            ParseResponseData(response, _games);
            WaitForUser();
        }

        private static void GetStreams(string clientId, RestClient twitchClient)
        {
            var request = new RestRequest("streams", Method.GET);
            request.AddHeader("Client-ID", clientId);
            request.AddParameter("first", _firstValue);
            request.AddParameter("game_id", _games[0].ID);

            var streams = new List<Stream>();
            var response = twitchClient.Execute(request);
            ParseResponseData(response, streams);

            AddBreak();
            Console.WriteLine($"Top 100 {_games[0].Name} Streamers:");
            streams.ForEach(s => Console.WriteLine(s.ToString()));
            WaitForUser();

            AddBreak();
            Console.WriteLine($"Top {_games[0].Name} Streamers with less than {_targetViewerCount} viewers:");
            streams.Where(s => s.Viewers <= _targetViewerCount).ToList().ForEach(s => Console.WriteLine(s.ToString()));
            WaitForUser();
        }

        private static void ParseResponseData<T>(IRestResponse response, IList<T> collection)
        {
            var parent = JObject.Parse(response.Content);
            var data = parent.Value<JArray>("data");
            foreach (var d in data)
            {
                collection.Add(d.ToObject<T>());
            }

            collection.ToList().ForEach(c => Console.WriteLine(c.ToString()));
        }

        private static void AddBreak()
        {
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void WaitForUser()
        {
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
        }

    }
}