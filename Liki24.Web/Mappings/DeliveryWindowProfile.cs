using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Liki24.Data;
using Liki24.Data.Dto;

namespace Liki24.Web.Mappings
{
    public class DeliveryWindowProfile : Profile
    {
        public DeliveryWindowProfile()
        {
            CreateMap<DeliveryWindowModifyDto,DeliveryWindow>()
                .ForMember(dest => dest.DeliveryStartTime, opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.DeliveryStartTime)))
                .ForMember(dest => dest.DeliveryEndTime, opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.DeliveryEndTime)));
        }
    }
}
