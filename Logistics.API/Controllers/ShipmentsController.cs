using Logistics.Application.DTOs.ShipmentDTOs;
using Logistics.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 //   [Authorize]
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
      //  [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Create([FromBody] RequireShipmentDto dto)
        {
         var res = await _shipmentService.CreateShipmentAsync(dto); 
         return CreatedAtAction(nameof(GetById), new { id = res.ShipmentId }, res); 
        }

        [HttpPut("{id}")]
   //     [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] RequireShipmentDto dto) 
        { await _shipmentService.UpdateShipmentAsync(id, dto); return NoContent(); }

      

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusUpdateDto request)
        {
            await _shipmentService.UpdateShipmentStatusAsync(id, request.Status);
            return NoContent();
        }


        [HttpGet("{id}/payments")]
        public async Task<IActionResult> GetShipmentPayments(int id)
        {
            var payments = await _paymentService.GetPaymentByIdAsync(id);
            return Ok(payments);
        }
    }
}
