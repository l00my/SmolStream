using System;
using System.Runtime.Serialization;

namespace SmolStream.StreamerService
{
    [DataContract]
    public class Stream
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }
        [DataMember(Name = "user_id")]
        public string UserID { get; set; }
        [DataMember(Name = "user_name")]
        public string UserName { get; set; }
        [DataMember(Name = "game_id")]
        public int GameID { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "viewer_count")]
        public int Viewers { get; set; }
        [DataMember(Name = "started_at")]
        public DateTime StartTime { get; set; }
        [DataMember(Name = "language")]
        public string Language { get; set; }
        [DataMember(Name = "thumbnail_url")]
        public string Thumbnail { get; set; }

        public override string ToString()
        {
            return $"{UserName}, User ID: {UserID}, Game ID: {GameID}, Title: {Title}, Viewers: {Viewers}";
        }
    }
}
