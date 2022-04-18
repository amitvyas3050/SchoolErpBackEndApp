/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;

namespace ECommerce.Entities
{
    public class AppFeatureGridFilter : BaseFilter
    {
        public string FilterByName { get; set; }
        public string FilterByMenuText { get; set; }
        public string FilterByMenuIcon { get; set; }
        public string FilterByBgColor { get; set; }
        
    }
}