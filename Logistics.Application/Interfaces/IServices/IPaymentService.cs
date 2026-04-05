using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistics.Application.DTOs.PaymentDTOs;


namespace Logistics.Application.Interfaces.IServices
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync();
        Task<PaymentDto?> GetPaymentByIdAsync(int id);
      Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto dto);
        Task<PaymentDto> UpdatePaymentStatusAsync(int id, bool isPaid);
    }
}
