using System.Collections.Generic;
using System.Linq;
using KanbanBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Data.CardData
{
    public class CardData : ICardData
    {
        private readonly KanbanContext _cardContext;

        public CardData(KanbanContext cardContext)
        {
            _cardContext = cardContext;
        }
        public IEnumerable<Card> GetCards(int columnId)
        {
            return _cardContext.Cards.Where(c => c.ColumnId == columnId);

        }

        public Card GetCard(int cardId)
        {
            return _cardContext.Cards.FirstOrDefault(c => c.Id == cardId);
        }

        public void AddCard(Card card)
        {
            _cardContext.Cards.Add(card);
            _cardContext.SaveChanges();
        }

        public void EditCard(Card card)
        {
            var entity = _cardContext.Entry(card);
            entity.State = EntityState.Modified;
            _cardContext.SaveChanges();
        }

        public void DeleteCard(int cardId)
        {
            var card = GetCard(cardId);
            if (card != null)
                _cardContext.Cards.Remove(card);
            _cardContext.SaveChanges();
        }

    }
}