using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoard.Data.BoardData;
using KanbanBoard.Data.CardData;
using KanbanBoard.Data.ColumnData;
using KanbanBoard.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KanbanBoard.Controllers
{
    public class BoardsController : Controller
    {
        private readonly IBoardData _boardData;
        private readonly IColumnData _columnData;
        private readonly ICardData _cardData;

        public BoardsController(IBoardData boardData, IColumnData columnData, ICardData cardData)
        {
            _boardData = boardData;
            _columnData = columnData;
            _cardData = cardData;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = _boardData.GetAllBoards();
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = _boardData.GetBoard(id.Value);
            if (board == null)
            {
                return NotFound();
            }

            board.Columns = _columnData.GetColumns(board.Id).ToList();

            foreach (var boardColumn in board.Columns)
            {
                boardColumn.Cards = _cardData.GetCards(boardColumn.Id).ToList();
            }

            return View(board);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Board board)
        {
            if (ModelState.IsValid)
            {
                _boardData.AddBoard(board);
                return RedirectToAction(nameof(Index));
            }

            return View(board);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var board = _boardData.GetBoard(id.Value);
            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, Name")] Board board)
        {
            if (id != board.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                _boardData.UpdateBoard(board);
                return RedirectToAction("Details", new { id = board.Id });
            }
            return View(board);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var board = _boardData.GetBoard(id.Value);
            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _boardData.DeleteBoard(id);
            //var board = await _context.Boards.FindAsync(id);
            //_context.Boards.Remove(board);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
