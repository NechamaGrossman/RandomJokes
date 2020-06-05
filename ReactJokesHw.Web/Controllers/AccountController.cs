using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReactJokesHw.Data;
using ReactJokesHw.Web.Models;

namespace ReactJokesHw.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        string _connectionString;
        public AccountController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpPost]
        [Route("SignUp")]
        public void SignUp(SignupViewModel signupViewModel)
        {
            var repos = new AccountRepository(_connectionString);
            repos.AddUser(signupViewModel, signupViewModel.Password);
        }

        [HttpPost]
        [Route("Login")]
        public Users Login(LoginViewModel loginViewModel)
        {
            var repos = new AccountRepository(_connectionString);
            Users login = repos.Login(loginViewModel.Email, loginViewModel.Password);
            if (login == null)
            {
                return null;
            }
            var claims = new List<Claim>
            {
                new Claim("user", loginViewModel.Email)
            };
            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();
            return login;
        }

        [HttpGet]
        [Route("logout")]
        public void Logout()
        {
            HttpContext.SignOutAsync().Wait();
        }

        [HttpGet]
        [Route("getcurrentuser")]
        public Users GetCurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }

            var repo = new AccountRepository(_connectionString);
            return repo.GetByEmail(User.Identity.Name);
        }
    }
}