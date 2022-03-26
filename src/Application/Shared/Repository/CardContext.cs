using Lets.Code.Application.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Lets.Code.Application.Shared.Repository
{
    public class CardContext : DbContext
    {
        public DbSet<CardModel> Cards { get; set; }

        public CardContext(DbContextOptions options) : base(options)
        {

        }
    }
}
