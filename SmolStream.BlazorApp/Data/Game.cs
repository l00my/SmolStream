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
        [DataMember(Name = "box_art_url")]
        public string BoxArtUrl { get; set; }

        public override string ToString()
        {
            return $"{Name}, ID: {ID}";
        }
    }
}
