using System;
using System.Collections.Generic;
using System.Text;

namespace ReactJokesHw.Data
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }


        public List<UserLikedJokes> UserLikedJokes { get; set; }
    }
}
