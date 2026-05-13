using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Logistics.Application.Interfaces.IServices;
using Logistics.Application.DTOs.ShipmentStatusHistoryDTOs;
using Logistics.Application.Interfaces;

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
            var shipmentExists = await _unitOfWork.Shipments.FindAsync(s => s.ShipmentId == shipmentId);
            if (!shipmentExists.Any())
            {
                throw new Exception("Shipment not found.");
            }

            var history = await _unitOfWork.ShipmentStatusHistories
                .FindAsync(h => h.ShipmentId == shipmentId, disableTracking: true);

            var orderedHistory = history.OrderBy(h => h.StatusDate);

            return _mapper.Map<IEnumerable<ShipmentStatusHistoryDto>>(orderedHistory);
        }
    }
}