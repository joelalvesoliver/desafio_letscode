using Lets.Code.Application.Shared.Domain.Models;
using Lets.Code.Application.Shared.Extensions;
using Lets.Code.Application.Shared.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lets.Code.Application.Features.Cards
{
    public class CardApplication : ICardApplication
    {
        private readonly ICardRepository _cardRepository;
        public CardApplication(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<CardModel> AddCard(CardInput card)
        {
            card.ThrowIfIsNotValid();

            var cardModel = new CardModel
            {
                Content = card.Content,
                List = card.List,
                Title = card.Title
            };
            
            return await Task.Run(() => _cardRepository.AddCard(cardModel));
        }

        public async Task<List<CardModel>> GetCards()
        {
            return await Task.Run(() => _cardRepository.GetCards());
        }

        public async Task<bool> RemoveCard(Guid id)
        {
            var result = await Task.Run(() => _cardRepository.DeleteCard(id));
            
            return result;
        }

        public async Task<CardModel> UpdateCard(CardInput card)
        {
            card.ThrowIfIsNotValid();

            var cardOutput = await Task.Run(() => _cardRepository.UpdateCard(new CardModel
            {
                Id = card.Id,
                Content = card.Content,
                List = card.List,
                Title = card.Title
            }));

            return cardOutput;
        }

    }
}
