using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;
using SmolStream.BlazorApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmolStream.BlazorApp.Services
{
    public class StreamDataService
    {
        private readonly int _targetViewerCount = 150;
        private readonly int _firstValue = 100;
        private readonly string _twitchUrl = "https://api.twitch.tv/helix";
        private List<Game> _games = new List<Game>();
        private IConfiguration _configuration;

        public StreamDataService(IConfiguration configuration)
        {
            _configuration = configuration;

            if(_games.Count == 0)
            {
                GetGameData();
            }
        }

        private void GetGameData()
        {
            var twitchClient = new RestClient(_twitchUrl);
            var request = new RestRequest("games/top", Method.GET);
            request.AddHeader("Client-ID", _configuration.GetValue<string>("TwitchClientID"));
            request.AddParameter("first", _firstValue);

            var response = twitchClient.Execute(request);
            ParseResponseData(response, _games);
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
