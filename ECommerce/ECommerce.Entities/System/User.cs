/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using System.Collections.Generic;

namespace ECommerce.Entities.System
{
    public class User : BaseUser
    {
        public virtual ICollection<Order> OrdersCreatedBy { get; set; }
        public virtual ICollection<Order> OrdersUpdatedBy { get; set; }

        public virtual ICollection<Class> ClassCreatedBy { get; set; }
        public virtual ICollection<Class> ClassUpdatedBy { get; set; }
        public virtual ICollection<UserActivity> UserActivities { get; set; }

        public virtual ICollection<AppModule> AppModuleCreatedBy { get; set; }
        public virtual ICollection<AppModule> AppModuleUpdatedBy { get; set; }

        public virtual ICollection<AppFeature> AppFeatureCreatedBy { get; set; }
        public virtual ICollection<AppFeature> AppFeatureUpdatedBy { get; set; }

        public virtual ICollection<RoleModule> RoleModuleCreatedBy { get; set; }
        public virtual ICollection<RoleModule> RoleModuleUpdatedBy { get; set; }

        public virtual ICollection<RolePermission> RolePermissionCreatedBy { get; set; }
        public virtual ICollection<RolePermission> RolePermissionUpdatedBy { get; set; }

    }
}
