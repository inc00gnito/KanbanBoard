using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoard.Models;

namespace KanbanBoard.Data.ColumnData
{
    interface IColumnData
    {
        public interface IColumnData
        {
            IEnumerable<Column> GetColumns(int boardId);
            Column GetColumn(int id);
            void AddColumn(Column column);
            void UpdateColumn(Column column);
            void DeleteColumn(int id);
            //void Move(MoveCard moveCard);
        }
    }
}
