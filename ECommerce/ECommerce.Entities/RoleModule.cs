/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using ECommerce.Entities.System;
using System;

namespace ECommerce.Entities
{
    public class RoleModule : TrackableEntity
    {
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int ModuleId { get; set; }
        public virtual AppModule Module { get; set; }
        public string NavUrl { get; set; }
    }
}
