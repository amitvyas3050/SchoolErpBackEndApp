/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

namespace ECommerce.DTO
{
    public class OrdersProfitDTO
    {
        public StatisticUnit<int> TodayProfit { get; set; }
        public StatisticUnit<int> WeekOrdersProfit { get; set; }
        public StatisticUnit<int> WeekCommentsProfit { get; set; }
    }
}
