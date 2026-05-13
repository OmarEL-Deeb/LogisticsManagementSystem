using Logistics.Application.DTOs.ShipmentDTOs;
using Logistics.Domain.Entities;
using Logistics.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.DTOs.ShipmentStatusHistoryDTOs
{
    public class ShipmentStatusHistoryDto
    {
        public int Id { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public DateTime StatusDate { get; set; }
        public int ShipmentId { get; set; }
    }
}
