/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

namespace Common.Entities.Statistics
{
    public class AggregatedData
    {
        public int Group { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }

    public class AggregatedData<TGroup>
    {
        public TGroup Group { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
