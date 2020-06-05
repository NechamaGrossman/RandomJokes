using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReactJokesHw.Data
{
    public class JokesRepository
    {
        string _connectionString;
        public JokesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Jokes> GetJokes()
        {
            using (var context = new JokesContext(_connectionString))
            {
                return context.Jokes.Include(j => j.UserjokeLikes).ToList();
            }
        }
        public Jokes GetJokesId(int Id)
        {
            return GetJokes().FirstOrDefault(j => j.Id == Id);
        }
        public void AddJoke()
        {
            using (var context = new JokesContext(_connectionString))
            {
                JokesApi jokesApi = new JokesApi();
                Jokes joke = jokesApi.AddJoke();
                joke.Id = 0;
                context.Jokes.Add(joke);
                context.SaveChanges();
            }
        }
        public UserLikedJokes GetLike(int userId, int jokeId)
        {
            using (var context = new JokesContext(_connectionString))
            {
                return context.UserLikedJokes.FirstOrDefault(u => u.UserId == userId && u.JokeId == jokeId);
            }
        }
        public void AddLikeOrDislike(Jokes joke, Users user, bool liked)
        {
            UserLikedJokes ulj =  GetLike(user.Id, joke.Id);
            using (var context = new JokesContext(_connectionString))
            {
                var like = new UserLikedJokes
                {
                    JokeId = joke.Id,
                    UserId = user.Id,
                    Liked = liked,
                    Date = DateTime.Now
                };
                if (ulj==null)
                {
                    context.UserLikedJokes.Add(like);
                    context.SaveChanges();
                }
                else
                {
                    context.UserLikedJokes.Attach(like);
                    context.Entry(like).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }
    }
}
