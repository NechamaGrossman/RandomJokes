﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ReactJokesHw
{
    public class Jokes
    {
        public int Id { get; set; }
        public string Setup { get; set; }
        public string Punchline { get; set; }


        public List<UserLikedJokes> UserjokeLikes { get; set; }
    }
}
