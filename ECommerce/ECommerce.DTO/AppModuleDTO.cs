/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using System;

namespace ECommerce.DTO
{
    public class AppModuleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MenuText { get; set; }
        public string MenuIcon { get; set; }
        public string BgColor1 { get; set; }
        public string BgColor2 { get; set; }
    }
}
