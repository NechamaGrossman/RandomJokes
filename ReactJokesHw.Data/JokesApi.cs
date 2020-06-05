using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace ReactJokesHw.Data
{
    public class JokesApi
    {
        public Jokes AddJoke()
        {
             using (var client = new HttpClient())
            {
                var json = client.GetStringAsync("https://official-joke-api.appspot.com/jokes/programming/random")
                   .Result;
                var joke = JsonConvert.DeserializeObject<IEnumerable<Jokes>>(json).FirstOrDefault();
                return joke;
            }
        }
    }
}
