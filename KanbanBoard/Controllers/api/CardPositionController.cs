using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoard.Data.CardData;

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
            Console.WriteLine("cARD UPDATE");
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
