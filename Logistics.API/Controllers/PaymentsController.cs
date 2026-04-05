using Logistics.Application.DTOs.PaymentDTOs;
using Logistics.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _service;
        public PaymentsController(IPaymentService service) => _service = service;

        [HttpGet("{id}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetPaymentByIdAsync(id));
        [HttpGet]
        public async Task<IActionResult> GetAllPayments() => Ok(await _service.GetAllPaymentsAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentDto dto)
        {
            var result = await _service.CreatePaymentAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.PaymentId }, result);
        }

        [HttpPatch("{id}/pay")]
        public async Task<IActionResult> MarkAsPaid(int id,bool ispaid)
        {
            await _service.UpdatePaymentStatusAsync(id, ispaid);
            return NoContent();
        }
    }
}
