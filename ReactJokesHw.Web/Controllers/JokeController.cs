using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReactJokesHw.Data;
using ReactJokesHw.Web.Models;

namespace ReactJokesHw.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokeController : ControllerBase
    {
        private string _connectionString;
        private const int MinutesAllowedToChangeLike = 5;
        public JokeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpGet]
        [Route("GetAllJokes")]
        public IEnumerable<Jokes> GetAllJokes()
        {
            var repo = new JokesRepository(_connectionString);
            return repo.GetJokes();
        }

        [HttpPost]
        [Route("AddJoke")]
        public void AddJoke()
        {
            JokesRepository repos = new JokesRepository(_connectionString);
            repos.AddJoke();
        }
        [HttpPost]
        [Route("AddLike")]
        public void AddLike(Jokes joke)
        {
            JokesRepository repos = new JokesRepository(_connectionString);
            AccountRepository account = new AccountRepository(_connectionString);
            Users currentUser = account.GetByEmail(User.Identity.Name);
            repos.AddLikeOrDislike(joke, currentUser, true);
        }
        [HttpPost]
        [Route("AddDislike")]
        public void AddDislike(Jokes joke)
        {
            JokesRepository repos = new JokesRepository(_connectionString);
            AccountRepository account = new AccountRepository(_connectionString);
            Users currentUser = account.GetByEmail(User.Identity.Name);
            repos.AddLikeOrDislike(joke, currentUser, false);
        }
        [HttpGet]
        [Route("GetJokeForId")]
        public JokeWithCounteViewModel GetJokeForId(int JokeId)
        {
            JokesRepository repos = new JokesRepository(_connectionString);
            Jokes j = repos.GetJokesId(JokeId);
            JokeWithCounteViewModel joke = new JokeWithCounteViewModel
            {
                Id = j.Id,
                Setup = j.Setup,
                Punchline = j.Punchline,
                LikeCount = j.UserjokeLikes.Where(ulj => ulj.Liked == true).ToList().Count,
                DislikeCount = j.UserjokeLikes.Where(ulj => ulj.Liked == false).ToList().Count
            };

            return joke;
        }
        [HttpGet]
        [Route("GetStatusOfUser")]
        public object GetStatusOfUser(int JokeId)
        {
            StatusOfUser status =  GetStatus(JokeId);
            return new { status };
        }
       
        [HttpGet]
        [Route("GetLikesAndDislikesCount")]
        public LikesDislikesViewModel GetLikesAndDislikesCount(int JokeId)
        {
            var jokeRepo = new JokesRepository(_connectionString);
            Jokes j = jokeRepo.GetJokesId(JokeId);
            LikesDislikesViewModel ldl = new LikesDislikesViewModel();
            ldl.LikesCount = j.UserjokeLikes.Where(ulj => ulj.Liked == true).ToList().Count();
            ldl.DislikesCount = j.UserjokeLikes.Where(ulj => ulj.Liked == false).ToList().Count();
            return ldl;
        }
        public StatusOfUser GetStatus(int JokeId)
        {
            var userRepo = new AccountRepository(_connectionString);
            var user = userRepo.GetByEmail(User.Identity.Name);
            var jokeRepo = new JokesRepository(_connectionString);
            UserLikedJokes likeStatus = jokeRepo.GetLike(user.Id, JokeId);

            if (likeStatus == null)
            {
                return StatusOfUser.NeverInteracted;
            }

            else if (likeStatus.Date.AddMinutes(MinutesAllowedToChangeLike) < DateTime.Now)
            {
                return StatusOfUser.CanNoLongerInteract;
            }

            else if(likeStatus.Liked==true)
            {
                return StatusOfUser.Liked;
            }

            return StatusOfUser.Disliked;

        }
    }
}
