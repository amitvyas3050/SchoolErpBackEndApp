/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities.Statistics;
using System.Collections.Generic;
using System.Linq;

namespace Common.Utils.Extensions
{
    public static class AggregatedDataExtensions
    {
        public static IEnumerable<AggregatedData> ExtendWithEmptyData(this IEnumerable<AggregatedData> data, IEnumerable<int> range)
        {
            return data.Union(range.Except(data.Select(x => x.Group)).Select(x => new AggregatedData
            {
                Count = 0,
                Sum = 0,
                Group = x
            })).OrderBy(x => x.Group);
        }

        public static IEnumerable<AggregatedData<TGroup>> ExtendWithEmptyData<TGroup>(this IEnumerable<AggregatedData<TGroup>> data, IEnumerable<TGroup> range)
        {
            return data.Union(range.Except(data.Select(x => x.Group)).Select(x => new AggregatedData<TGroup>
            {
                Count = 0,
                Sum = 0,
                Group = x
            })).OrderBy(x => x.Group);
        }
    }
}
