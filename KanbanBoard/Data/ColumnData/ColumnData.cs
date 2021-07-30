using System.Collections.Generic;
using System.Linq;
using KanbanBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Data.ColumnData
{
    public class ColumnData : IColumnData
    {
        private readonly KanbanContext _db;

        public ColumnData(KanbanContext db)
        {
            _db = db;
        }
        public IEnumerable<Column> GetColumns(int boardId)
        {
            return from col in _db.Columns
                where col.BoardId == boardId
                select col;
        }

        public Column GetColumn(int id)
        {
            return _db.Columns.FirstOrDefault(c => c.Id == id);
        }

        public void AddColumn(Column column)
        {
            _db.Columns.Add(column);
            _db.SaveChanges();
        }

        public void UpdateColumn(Column column)
        {
            var entity = _db.Entry(column);
            entity.State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void DeleteColumn(int id)
        {
            var col = _db.Columns.FirstOrDefault(c => c.Id == id);
            if (col != null)
                _db.Columns.Remove(col);
            _db.SaveChanges();
        }

        //public void Move(MoveCard moveCard)
        //{
        //    var card = _db.Cards.SingleOrDefault(x => x.Id == moveCard.CardId);
        //    card.ColumnId = moveCard.ColumnId;
        //    _db.SaveChanges();
        //}
    }
}