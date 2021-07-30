using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.Models
{
    public class Card
    {
        public int Id { get; set; }
        [Required, MaxLength(225)]
        public string Description { get; set; }
        public Column Column { get; set; }
        public int ColumnId { get; set; }
    }
}