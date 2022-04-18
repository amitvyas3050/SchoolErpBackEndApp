/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using ECommerce.Entities.System;
using System;
using System.Collections.Generic;

namespace ECommerce.Entities
{
    public class AppFeature : TrackableEntity
    {
        public string Name { get; set; }
        public string MenuText { get; set; }
        public string MenuIcon { get; set; }
        public string BgColor1 { get; set; }
        public string BgColor2 { get; set; }
        public int ModuleId { get; set; }
        public virtual AppModule Module { get; set; }
        public string NavUrl { get; set; }

    }
}
