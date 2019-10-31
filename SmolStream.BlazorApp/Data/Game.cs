using System;
using System.Runtime.Serialization;

namespace SmolStream.BlazorApp.Data
{
    [DataContract]
    public class Game
    {
        [DataMember(Name = "id")]
        public int ID { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        private string _boxArtUrl;
        [DataMember(Name = "box_art_url")]
        public string BoxArtUrl { get { return _boxArtUrl; } set { _boxArtUrl = value.Replace("{width}", "172").Replace("{height}", "230"); } }

        public override string ToString()
        {
            return $"{Name}, ID: {ID}";
        }
    }
}
