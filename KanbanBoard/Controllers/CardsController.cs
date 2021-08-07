using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KanbanBoard.Data;
using KanbanBoard.Data.CardData;
using KanbanBoard.Data.ColumnData;
using KanbanBoard.Models;

namespace KanbanBoard.Controllers
{
    public class CardsController : Controller
    {
        private readonly ICardData _cardData;
        private readonly IColumnData _columnData;

        public CardsController(ICardData cardData, IColumnData columnData)
        {
            _cardData = cardData;
            _columnData = columnData;
        }
        
        // GET: Cards/Create
        public IActionResult Create(int columnId, int backupId)
        {
            
            TempData["columnId"] = columnId;
            TempData.Keep();
            var card = new Card {ColumnId = columnId};
            return PartialView("_CardModelPartial", card);
        }

        // POST: Cards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Description")] Card card)
        {
            if (ModelState.IsValid)
            {
                card.ColumnId = (int) TempData["columnId"];
                card.Column = _columnData.GetColumn(card.ColumnId);
                _cardData.AddCard(card);

                return RedirectToAction("Details", "Boards", new { id = card.Column.BoardId });
            }
            return View(card);
        }

        // GET: Cards/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = _cardData.GetCard(id.Value);
            if (card == null)
            {
                return NotFound();
            }
            card.Column = _columnData.GetColumn(card.ColumnId);
            TempData["columnId"] = card.ColumnId;
            TempData.Keep();
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Description,ColumnId")] Card card)
        {
            if (id != card.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                card.ColumnId = (int)TempData["columnId"];
                Console.WriteLine(card.ColumnId + "post");
                card.Column = _columnData.GetColumn(card.ColumnId);
                _cardData.EditCard(card);
                return RedirectToAction("Details", "Boards", new { id = card.Column.BoardId });
            }
            return View(card);
        }

        // GET: Cards/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = _cardData.GetCard(id.Value);
            if (card == null)
            {
                return NotFound();
            }
            card.Column = _columnData.GetColumn(card.ColumnId);
            Console.WriteLine(card.Column.Name);
            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var card = _cardData.GetCard(id);

            if (card != null)
            {
                card.Column = _columnData.GetColumn(card.ColumnId);
                _cardData.DeleteCard(id);
                return RedirectToAction("Details", "Boards", new { id = card.Column.BoardId });
            }

            return NotFound();
        }

    }
}
