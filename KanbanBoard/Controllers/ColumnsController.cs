using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KanbanBoard.Data;
using KanbanBoard.Data.ColumnData;
using KanbanBoard.Data.ColumnData.
using KanbanBoard.Models;

namespace KanbanBoard.Controllers
{
    public class ColumnsController : Controller
    {
        private readonly IColumnData _columnData;

        public ColumnsController(IColumnData columnData)
        {
            _columnData = columnData;
            
        }

        
        // GET: Columns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var column = _columnData.GetColumn(id.Value);
            if (column == null)
            {
                return NotFound();
            }

            return View(column);
        }

        // GET: Columns/Create
        public IActionResult Create([FromQuery(Name = "boardId")] int boardId)
        {
            TempData["boardId"] = boardId;
            TempData.Keep();
            return View();
        }

        // POST: Columns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Column column)
        {
            if (ModelState.IsValid)
            {
                column.BoardId = (int)TempData["boardId"];
                _columnData.AddColumn(column);

                return RedirectToAction("Details", "Boards", new { id = column.BoardId });
            }

            return View(column);
        }

        // GET: Columns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var column = _columnData.GetColumn(id.Value);
            if (column == null)
            {
                return NotFound();
            }

            TempData["boardId"] = column.BoardId;
            TempData.Keep();
            
            return View(column);
        }

        // POST: Columns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,BoardId")] Column column)
        {
            if (id != column.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                column.BoardId = (int)TempData["boardId"];
                _columnData.UpdateColumn(column);
                return RedirectToAction("Details", "Boards", new { id = column.BoardId });
            }
            return View(column);
        }

        // GET: Columns/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var column = _columnData.GetColumn(id.Value);
            if (column == null)
            {
                return NotFound();
            }

            return View(column);
        }

        // POST: Columns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var column = _columnData.GetColumn(id);
            if (column != null)
            {
                _columnData.DeleteColumn(id);
                return RedirectToAction("Details", "Boards", new { id = column.BoardId });
            }

            return NotFound();
        }
    }
}
