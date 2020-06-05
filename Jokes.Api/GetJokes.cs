using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using ReactJokesHw.Data;

namespace Jokes.Api
{
    public class GetJokes
    {
        public Jokes GetRandomJoke()
        {
            var client = new HttpClient();
            string url = $"https://www.hebcal.com/shabbat/?cfg=json&zip=${zip}";
            string json = client.GetStringAsync(url).Result;
            var result = JsonConvert.DeserializeObject<Jokes>(json);
            return result;
        }
    }
}
