using Logistics.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentStatusHistoryController : ControllerBase
    {
        private readonly IShipmentStatusHistoryService _historyService;

        public ShipmentStatusHistoryController(IShipmentStatusHistoryService historyService)
        {
                        _historyService = historyService;
                
        }
        [HttpGet("{id}/status-history")]

        public async Task<IActionResult> GetStatusHistory(int id)
        {
            var history = await _historyService.GetHistoryByShipmentIdAsync(id);

            if (!history.Any())
            {
                return NotFound();
            }
    
            return Ok(history);
        }
    }
}
