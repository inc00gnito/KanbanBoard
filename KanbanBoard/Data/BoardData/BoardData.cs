using System.Collections.Generic;
using System.Linq;
using KanbanBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Data.BoardData
{
    public class BoardData : IBoardData
    {
        private readonly KanbanContext _db;

        public BoardData(KanbanContext db)
        {
            _db = db;
        }
        public IEnumerable<Board> GetAllBoards()
        {
            return _db.Boards;
        }

        public Board GetBoard(int id)
        {
            return _db.Boards.FirstOrDefault(b => b.Id == id);
        }

        public void AddBoard(Board board)
        {
            _db.Boards.Add(board);
            _db.SaveChanges();
        }

        public void UpdateBoard(Board board)
        {
            var entity = _db.Entry(board);
            entity.State = EntityState.Modified;
            _db.SaveChanges();

        }

        public void DeleteBoard(int id)
        {
            var board = _db.Boards.FirstOrDefault(b => b.Id == id);
            if (board != null)
                _db.Boards.Remove(board);
            _db.SaveChanges();
        }

    }
}