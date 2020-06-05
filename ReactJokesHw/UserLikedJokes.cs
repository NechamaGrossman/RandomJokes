using System;
using System.Collections.Generic;
using System.Text;

namespace ReactJokesHw
{
    public class UserLikedJokes
    {
        public int UserId { get; set; }
        public int JokeId { get; set; }
        public DateTime Date { get; set; }
        public int Id { get; set; }
        public bool Liked { get; set; }


        public Users User { get; set; }
        public Jokes Joke { get; set; }
    }
}
