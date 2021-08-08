using System;
using KanbanBoard.Data.CardData;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardPositionController : ControllerBase
    {
        private readonly ICardData _cardData;

        public CardPositionController(ICardData cardData)
        {
            _cardData = cardData;
        }

        [HttpPost("cardupdate")]
        public void UpdateCardPosition(CardPosition position)
        {
            var card = _cardData.GetCard(Convert.ToInt32(position.cardId));
            card.ColumnId = Convert.ToInt32(position.onColumnId);
            _cardData.EditCard(card);
        }
    }

    public class CardPosition
    {
        public string cardId { get; set; }
        public string onColumnId { get; set; }
    }
}