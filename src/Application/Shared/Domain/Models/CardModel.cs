using Lets.Code.Application.Shared.Validations;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lets.Code.Application.Shared.Domain.Models
{
    public class CardModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string List { get; set; }

    }
}
