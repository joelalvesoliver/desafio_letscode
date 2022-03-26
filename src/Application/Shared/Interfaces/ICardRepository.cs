

using Lets.Code.Application.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lets.Code.Application.Shared.Interfaces
{
    public interface ICardRepository
    {
        CardModel AddCard(CardModel card);
        CardModel UpdateCard(CardModel card);
        List<CardModel> GetCards();
        CardModel GetCard(Guid id);
        bool DeleteCard(Guid id);
    }
}
