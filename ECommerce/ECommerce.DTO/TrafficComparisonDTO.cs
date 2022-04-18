/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

namespace ECommerce.DTO
{
    public class TrafficComparisonDTO
    {
        public string PreviousPeriod { get; set; }
        public int PreviousValue { get; set; }
        public string CurrentPeriod { get; set; }
        public int CurrentValue { get; set; }
    }
}
