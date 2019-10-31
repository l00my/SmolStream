using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;
using SmolStream.BlazorApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmolStream.BlazorApp.Services
{
    public class StreamDataService
    {
        private readonly int _targetViewerCount = 150;
        private readonly int _firstValue = 100;
        private readonly string _twitchUrl = "https://api.twitch.tv/helix";
        private IConfiguration _configuration;

        public Game[] Games;

        public StreamDataService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Game[]> GetGameDataAsync()
        {
            var games = new List<Game>();
            var twitchClient = new RestClient(_twitchUrl);
            var request = new RestRequest("games/top", Method.GET);
            request.AddHeader("Client-ID", _configuration.GetValue<string>("TwitchClientID"));
            request.AddParameter("first", _firstValue);

            var response = await twitchClient.ExecuteTaskAsync(request);
            ParseResponseData(response, games);
            return games.ToArray();
        }

        private void ParseResponseData<T>(IRestResponse response, IList<T> collection)
        {
            var parent = JObject.Parse(response.Content);
            var data = parent.Value<JArray>("data");
            foreach (var d in data)
            {
                collection.Add(d.ToObject<T>());
            }
        }

    }
}
