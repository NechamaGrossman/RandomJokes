using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReactJokesHw.Data
{
    public class UserLikedJokes
    {
        public int UserId { get; set; }
        public int JokeId { get; set; }
        public DateTime Date { get; set; }
        public int Id { get; set; }
        public bool Liked { get; set; }

        [JsonIgnore]
        public Users User { get; set; }
        [JsonIgnore]
        public Jokes Joke { get; set; }
    }
}
