using Lets.Code.Application.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lets.Code.Application.Shared.Interfaces
{
    public interface ICardApplication
    {
        Task<CardModel> AddCard(CardInput card);
        Task<CardModel> UpdateCard(CardInput card);
        Task<bool> RemoveCard(Guid id);
        Task<List<CardModel>> GetCards();
    }
}
