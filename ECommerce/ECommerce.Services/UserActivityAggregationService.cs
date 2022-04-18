/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Entities.Statistics;
using Common.Services;
using Common.Services.Infrastructure;
using Common.Utils.Extensions;
using ECommerce.DTO;
using ECommerce.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class UserActivityAggregationService : BaseService, IUserActivityAggregationService
    {
        protected readonly IUserActivityAggregatedRepository userActivityAggregatedRepository;

        public UserActivityAggregationService(ICurrentContextProvider contextProvider, IUserActivityAggregatedRepository userActivityAggregatedRepository) : base(contextProvider)
        {
            this.userActivityAggregatedRepository = userActivityAggregatedRepository;
        }

        public async Task<IEnumerable<UserActivityDTO>> GetDataForTable(string filter)
        {
            IEnumerable<UserActivityDTO> list = null;
            IEnumerable<AggregatedData> res;

            Enum.TryParse(filter, true, out PeriodFilterEnum period);
            switch (period)
            {
                case PeriodFilterEnum.Year:
                    {
                        var tenYearsBefore = DateTime.Today.YearBefore(10);
                        res = await userActivityAggregatedRepository.GetYearlyDataForTable(Session, tenYearsBefore);

                        res = res.ExtendWithEmptyData(Enumerable.Range(tenYearsBefore.Year, 10));

                        list = res.Select(x => new UserActivityDTO
                        {
                            PagesVisit = x.Count,
                            Label = x.Group.ToString()
                        }).ToList();
                        break;
                    }
                case PeriodFilterEnum.Month:
                    {
                        res = await userActivityAggregatedRepository.GetMonthDataForTable(Session);

                        res = res.ExtendWithEmptyData(Enumerable.Range(1, DateTime.Today.Day));

                        list = res.Select(x => new UserActivityDTO
                        {
                            PagesVisit = x.Count,
                            Label = new DateTime(DateTime.Today.Year, DateTime.Today.Month, x.Group).ToString("dd MMM", CultureInfo.InvariantCulture)
                        }).ToList();
                        break;
                    }
                case PeriodFilterEnum.Week:
                    {
                        var weekBefore = DateTime.Today.WeekBefore();
                        var start = weekBefore.StartOfWeek();
                        var end = weekBefore.EndOfWeek();
                        res = await userActivityAggregatedRepository.GetWeekDataForTable(Session, start, end);

                        var dates = Enumerable.Range(0, 1 + end.Subtract(start).Days).Select(offset => start.AddDays(offset)).ToArray();
                        foreach (var r in res)
                        {
                            r.Group = (int)dates.Single(x => x.Day == r.Group).DayOfWeek;
                        }

                        res = res.ExtendWithEmptyData(dates.Select(x => (int)x.DayOfWeek));

                        list = res.Select(x => new UserActivityDTO
                        {
                            PagesVisit = x.Count,
                            Label = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedDayName((DayOfWeek)x.Group)
                        }).ToList();
                        break;
                    }
            }

            if (list != null && list.Count() > 1)
            {
                for (var i = 1; i < list.Count(); i++)
                {
                    var previousValue = list.ElementAt(i - 1).PagesVisit;
                    var currentValue = list.ElementAt(i).PagesVisit;

                    list.ElementAt(i).Trend = previousValue == 0 ? 0 : (double)(currentValue - previousValue) / previousValue * 100;
                }
            }

            return list;
        }
    }
}
