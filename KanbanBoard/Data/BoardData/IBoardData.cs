using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoard.Models;

namespace KanbanBoard.Data.BoardData
{
    public interface IBoardData
    {
        IEnumerable<Board> GetAllBoards();
        Board GetBoard(int id);
        void AddBoard(Board board);
        void UpdateBoard(Board board);
        void DeleteBoard(int id);
    }
}
