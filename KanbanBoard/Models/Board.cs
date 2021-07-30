using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KanbanBoard.Models
{
    public class Board
    {
        public int Id { get; set; }
        [Required, MaxLength(80)]
        public string Name { get; set; }
        public ICollection<Column> Columns { get; set; } = new List<Column>();
    }
}
