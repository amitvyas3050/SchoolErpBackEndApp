/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using AutoMapper;
using Common.DTO;
using ECommerce.DTO;
using ECommerce.Entities;
using ECommerce.Entities.System;
using System;

namespace ECommerce.Services.Infrastructure.MappingProfiles
{
    public class RolePermissionProfile : Profile
    {
       

        public RolePermissionProfile()
        {
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<AppModule, AppModuleDTO>().ReverseMap();
            CreateMap<AppFeature, AppFeatureDTO>().ReverseMap();
            CreateMap<RoleModule, RoleModuleDTO>().ReverseMap();
            CreateMap<RolePermission, RolePermissionDTO>().ReverseMap();
        }
    }
}