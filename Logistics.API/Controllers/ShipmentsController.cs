using Logistics.Application.DTOs.ShipmentDTOs;
using Logistics.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentsController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;
        private readonly IPaymentService _paymentService;

        public ShipmentsController(IShipmentService shipmentService, IPaymentService paymentService)
        {
            _shipmentService = shipmentService;
            _paymentService = paymentService;
        }

        [HttpGet] 
        public async Task<IActionResult> GetAll() => Ok(await _shipmentService.GetAllShipmentsAsync());
        [HttpGet("{id}")] 
        public async Task<IActionResult> GetById(int id) => Ok(await _shipmentService.GetShipmentByIdAsync(id));
       
        [HttpPost] 
        public async Task<IActionResult> Create([FromBody] CreateShipmentDto dto)
        {
         var res = await _shipmentService.CreateShipmentAsync(dto); 
         return CreatedAtAction(nameof(GetById), new { id = res.ShipmentId }, res); 
        }

        [HttpPut("{id}")] 
        public async Task<IActionResult> Update(int id, [FromBody] CreateShipmentDto dto) 
        { await _shipmentService.UpdateShipmentAsync(id, dto); return NoContent(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { await _shipmentService.DeleteShipmentAsync(id); return NoContent(); }

        // Custom: Update Status
        public class StatusUpdateRequest { public string Status { get; set; } = string.Empty; }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusUpdateRequest request)
        {
            await _shipmentService.UpdateShipmentStatusAsync(id, request.Status);
            return NoContent();
        }


        // Custom: Get Shipment Payments (GET /api/shipments/{id}/payments)
        [HttpGet("{id}/payments")]
        public async Task<IActionResult> GetShipmentPayments(int id)
        {
            var payments = await _paymentService.GetPaymentByIdAsync(id);
            return Ok(payments);
        }
    }
}
