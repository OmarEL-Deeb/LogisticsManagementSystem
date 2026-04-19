using Logistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Logistics.Application.Interfaces.IServices;
using Logistics.Application.DTOs.ShipmentStatusHistoryDTOs;

namespace Logistics.Infrastructure.Services
{
    public class ShipmentStatusHistoryService : IShipmentStatusHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ShipmentStatusHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShipmentStatusHistoryDto>> GetHistoryByShipmentIdAsync(int shipmentId)
        {
            var history = await _unitOfWork.ShipmentStatusHistories.FindAsync(h => h.ShipmentId == shipmentId,c=>c.Shipment) ?? throw new Exception("Shipment not found.");
            return _mapper.Map<IEnumerable<ShipmentStatusHistoryDto>>(history.OrderBy(h => h.StatusDate));
        }
    }
}
