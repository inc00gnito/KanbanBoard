using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoard.Models;

namespace KanbanBoard.Data.CardData
{
    public interface ICardData
    {
        IEnumerable<Card> GetCards(int columnId);
        Card GetCard(int cardId);
        void AddCard(Card card);
        void EditCard(Card card);
        void DeleteCard(int cardId);
    }
}
