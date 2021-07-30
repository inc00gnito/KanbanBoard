using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Data
{
    public class KanbanContext :DbContext
    {
        public KanbanContext(DbContextOptions<KanbanContext> options)  :base(options)
        {
            
        }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Card> Cards { get; set; }

    }
}
