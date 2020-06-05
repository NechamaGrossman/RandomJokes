using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactJokesHw.Web.Models
{
    public class JokeWithCounteViewModel
    {
        public int Id { get; set; }
        public string Setup { get; set; }
        public string Punchline { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
    }
}
