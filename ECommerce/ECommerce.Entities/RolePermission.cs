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
    public class RolePermission : TrackableEntity
    {
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int ModuleId { get; set; }
        public virtual AppModule AppModule { get; set; }

        public int FeatureId { get; set; }
        public virtual AppFeature AppFeature { get; set; }

        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanView { get; set; }

    }
}
