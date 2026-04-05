using Logistics.Domain.Entities;
using Logistics.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.DTOs.PaymentDTOs
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool IsPaid { get; set; } = false;
        public DateTime? PaidDate { get; set; }

        public int ShipmentId { get; set; }
    }
}
