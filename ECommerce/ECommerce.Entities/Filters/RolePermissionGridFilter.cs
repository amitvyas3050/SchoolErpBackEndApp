/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;

namespace ECommerce.Entities
{
    public class RolePermissionGridFilter : BaseFilter
    {

        public string FilterByRole { get; set; }
        public string FilterByAppModule { get; set; }
        public string FilterByAppFeature { get; set; }
        public string FilterByCanAdd { get; set; }
        public string FilterByCanEdit { get; set; }
        public string FilterByCanDelete { get; set; }
        public string FilterByCanView { get; set; }


    }
}