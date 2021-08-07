using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoard.Data.ColumnData;

namespace KanbanBoard.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnPositionController : ControllerBase
    {
        private readonly IColumnData _columnData;

        public ColumnPositionController(IColumnData columnData)
        {
            _columnData = columnData;
        }
        [HttpPost("columnupdate")]
        public void UpdateColumns(List<Position> columnPositions)
        {
            foreach (var columnPosition in columnPositions)
            {
                var col = _columnData.GetColumn(Convert.ToInt32(columnPosition.ColumnId));
                
                col.Position = Convert.ToInt32(columnPosition.PositionId);
                _columnData.UpdateColumn(col);
            }
            

        }
    }

    public class Position
    {
        public string ColumnId { get; set; }
        public string PositionId { get; set; }
    }

}
