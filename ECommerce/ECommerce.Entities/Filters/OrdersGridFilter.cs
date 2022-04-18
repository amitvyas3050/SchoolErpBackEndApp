/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;

namespace ECommerce.Entities
{
    public class OrdersGridFilter : BaseFilter
    {
        public string FilterByName { get; set; }
        public string FilterByDate { get; set; }
        public string FilterBySum { get; set; }
        public string FilterByType { get; set; }
        public string FilterByStatus { get; set; }
        public string FilterByCountry { get; set; }
    }
}