using Lets.Code.Application.Shared.Domain.Models;
using Lets.Code.Application.Shared.Interfaces;
using Lets.Code.Application.Shared.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lets.Code.Application.Shared.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly CardContext _cardContext;

        public CardRepository(CardContext cardContext)
        {
            _cardContext = cardContext;
        }
        public CardModel AddCard(CardModel card)
        {
            var cardInsert = _cardContext.Add(card).Entity;
            if(_cardContext.SaveChanges() > 0)
                return cardInsert;
            else
                return null;
            
        }

        public bool DeleteCard(Guid id)
        {
            var card = this.GetCard(id);
            if(card == null)
                return false;

            _cardContext.Remove(card);
            _cardContext.SaveChanges();

            Log.LogDelete(card);

            return true;   
        }

        public CardModel GetCard(Guid id)
        {
            return _cardContext.Cards.Where(a => a.Id == id).FirstOrDefault();
        }

        public List<CardModel> GetCards()
        {
            return _cardContext.Cards.ToList();
        }

        public CardModel UpdateCard(CardModel card)
        {
            var cardCurrent = this.GetCard(card.Id);
            if(cardCurrent == null)
                return null;

            cardCurrent.Id = card.Id;
            cardCurrent.Title = card.Title;
            cardCurrent.List = card.List;
            cardCurrent.Content = card.Content;

            card = _cardContext.Update(cardCurrent).Entity;
            _cardContext.SaveChanges();

            Log.LogUpdate(card);

            return card;
        }

        
    }
}
