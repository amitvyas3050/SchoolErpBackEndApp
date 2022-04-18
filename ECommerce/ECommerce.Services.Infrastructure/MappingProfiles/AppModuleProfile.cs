using AutoMapper;
using Common.DTO;
using ECommerce.DTO;
using ECommerce.Entities;
using System;

namespace ECommerce.Services.Infrastructure.MappingProfiles
{
    public class AppModuleProfile : Profile
    {      

        public AppModuleProfile()
        {
           CreateMap<AppModule, AppModuleDTO>().ReverseMap();
        }
    }
}