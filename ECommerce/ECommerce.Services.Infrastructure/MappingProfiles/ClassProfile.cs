/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using AutoMapper;
using Common.DTO;
using ECommerce.DTO;
using ECommerce.Entities;
using System;

namespace ECommerce.Services.Infrastructure.MappingProfiles
{
    public class ClassProfile : Profile
    {
        

        public ClassProfile()
        {
            //CreateMap<Order, OrderDTO>()
            //    .ForMember(d => d.Sum, opt => opt.MapFrom(src => new MoneyDTO(src.Value, src.Currency)))
            //    .ForMember(d => d.Status,
            //        opt => opt.MapFrom(src => ((OrderStatusEnum)src.Status).ToString()))
            //    .ForMember(d => d.Type,
            //        opt => opt.MapFrom(src => ((OrderTypeEnum)src.Type).ToString()))
            //    .ReverseMap()
            //    .ForMember(d => d.Value, opt => opt.MapFrom(src => src.Sum.Value))
            //    .ForMember(d => d.Currency, opt => opt.MapFrom(src => src.Sum.Currency))
            //    .ForMember(d => d.Status, opt => opt.MapFrom(src => ParseOrderStatus(src.Status)))
            //    .ForMember(d => d.Type, opt => opt.MapFrom(src => ParseOrderType(src.Type)));



            //CreateMap<Class, ClassDTO>();
            CreateMap<Class, ClassDTO>().ReverseMap();
        }
    }
}