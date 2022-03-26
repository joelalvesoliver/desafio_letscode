

using Lets.Code.Application.Shared.Validations;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lets.Code.Application.Shared.Domain.Models
{
    public class CardInput
    {
        [JsonProperty("id")]
        [Required]
        public Guid Id { get; set; }

        [JsonProperty("titulo")]
        [Required, TitleValidation]
        public string Title { get; set; }

        [JsonProperty("conteudo")]
        [Required, ContentsValidation]
        public string Content { get; set; }

        [JsonProperty("lista")]
        [Required, ListValidation]
        public string List { get; set; }

    }
}
