using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanBoard.Models
{
    public class Column
    {
        public int Id { get; set; }
        [Required, MaxLength(80)]
        public string Name { get; set; }
        public Board Board { get; set; }
        public int BoardId { get; set; }
        public IList<Card> Cards { get; set; } = new List<Card>();
    }
}