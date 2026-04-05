using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Logistics.Application.DTOs.PaymentDTOs;
using Logistics.Domain.Entities;


namespace Logistics.Application.Mappings
{
    public class PaymentProfiles : Profile
    {
        public PaymentProfiles() 
        { 
            CreateMap<Payment, PaymentDto>()
                .ForMember(dest => dest.ShipmentId, opt => opt.MapFrom(src => src.Shipment.ShipmentId));
            CreateMap<CreatePaymentDto, Payment>();

        }
    }
}
