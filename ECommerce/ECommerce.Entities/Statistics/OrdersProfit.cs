/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

namespace ECommerce.Entities
{
    public class OrdersProfit
    {
        public int LastWeekCount { get; set; }
        public int CurrentWeekCount { get; set; }
        public int LastWeekProfit { get; set; }
        public int CurrentWeekProfit { get; set; }
    }
}
