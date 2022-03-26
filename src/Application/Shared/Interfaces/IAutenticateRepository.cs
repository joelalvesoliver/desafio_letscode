
using Lets.Code.Application.Shared.Domain.Models;
using System.Threading.Tasks;

namespace Lets.Code.Application.Shared.Interfaces
{
    public interface IAutenticateRepository
    {
        Task<User> Get(string username, string password);
    }
}
