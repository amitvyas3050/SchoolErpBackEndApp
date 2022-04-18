/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using System;

namespace ECommerce.DTO
{
    public class RoleModuleDTO
    {
        public int Id { get; set; }
        public AppModuleDTO Module { get; set; }
        public RoleDTO Role { get; set; }
        public string NavUrl { get; set; }

    }
}
