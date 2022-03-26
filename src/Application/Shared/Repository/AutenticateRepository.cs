using Lets.Code.Application.Shared.Domain.Models;
using Lets.Code.Application.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lets.Code.Application.Shared.Repository
{
    public class AutenticateRepository : IAutenticateRepository
    {
        private readonly IConfiguration _configuration;
        public AutenticateRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<User> Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Login = Settings.GetLogin(_configuration), Password = Settings.GetPassword(_configuration), Role = "manager" });
            return Task.FromResult(users.Where(x => x.Login.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault());
        }

    }
}
