using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ReactJokesHw.Data
{
    public class Jokes
    {
        public int Id { get; set; }
        public string Setup { get; set; }
        public string Punchline { get; set; }


        public List<UserLikedJokes> UserjokeLikes { get; set; }
    }
}
