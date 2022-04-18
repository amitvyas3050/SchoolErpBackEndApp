/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

namespace ECommerce.DTO
{
    public class TrafficAggregationDTO
    {
        public string Period { get; set; }
        public int Value { get; set; }
        public double Trend { get; set; }
        public TrafficComparisonDTO Comparison { get; set; }
    }
}
