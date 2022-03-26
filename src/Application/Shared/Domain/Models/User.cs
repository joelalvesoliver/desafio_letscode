
using System.ComponentModel.DataAnnotations;

namespace Lets.Code.Application.Shared.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
