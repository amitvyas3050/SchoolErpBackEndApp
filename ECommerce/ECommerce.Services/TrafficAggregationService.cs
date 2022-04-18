/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.Entities;
using Common.Entities.Statistics;
using Common.Services;
using Common.Services.Infrastructure;
using Common.Utils.Extensions;
using ECommerce.DTO;
using ECommerce.Entities;
using ECommerce.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class TrafficAggregationService : BaseService, ITrafficAggregationService
    {
        private readonly ITrafficAggregatedRepository trafficAggregatedRepository;

        private readonly Dictionary<PeriodFilterEnum, Expression<Func<Traffic, AggregatedData>>>
            _groupSelectorsForPeriods;

        public TrafficAggregationService(ICurrentContextProvider contextProvider,
            ITrafficAggregatedRepository trafficAggregatedRepository) : base(contextProvider)
        {
            this.trafficAggregatedRepository = trafficAggregatedRepository;

            _groupSelectorsForPeriods =
                new Dictionary<PeriodFilterEnum, Expression<Func<Traffic, AggregatedData>>>
                {
                    {
                        PeriodFilterEnum.Week, obj => new AggregatedData {Group = obj.Date.Day, Sum = obj.Value}
                    },
                    {
                        PeriodFilterEnum.Month, obj => new AggregatedData {Group = obj.Date.Month, Sum = obj.Value}
                    },
                    {
                        PeriodFilterEnum.Year, obj => new AggregatedData {Group = obj.Date.Year, Sum = obj.Value}
                    }
                };
        }

        public async Task<IEnumerable<TrafficAggregationDTO>> GetDataForTable(string filter)
        {
            IEnumerable<TrafficAggregationDTO> result;
            Enum.TryParse(filter, true, out PeriodFilterEnum period);

            var parameter = _groupSelectorsForPeriods[period];

            switch (period)
            {
                case PeriodFilterEnum.Year:
                    {
                        var startYearDate = new DateTime(DateTime.Today.YearBefore(10).Year, 1, 1);

                        var res = await trafficAggregatedRepository.GetDataByPeriod(startYearDate, DateTime.Now,
                            parameter, Session);
                        var dataList = res.ToList();

                        var startYear = dataList.Min(x => x.Group);
                        var endYear = dataList.Max(x => x.Group);

                        res = dataList.ExtendWithEmptyData(Enumerable.Range(startYear, endYear - startYear));

                        result = res.Select(x => new TrafficAggregationDTO
                        {
                            Value = (int)x.Sum,
                            Period = x.Group.ToString()
                        }).ToList();

                        break;
                    }
                case PeriodFilterEnum.Month:
                    {
                        var res = await trafficAggregatedRepository.GetDataByPeriod(
                            DateTime.Now.AddYears(-1).StartOfYear(), DateTime.Now.AddYears(-1).EndOfYear(), parameter,
                            Session);
                        var dataList = res.ToList();

                        res = dataList.ExtendWithEmptyData(Enumerable.Range(1, 12));

                        result = res.Select(x => new TrafficAggregationDTO
                        {
                            Value = (int)x.Sum,
                            Period = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(x.Group)
                        }).ToList();

                        break;
                    }
                case PeriodFilterEnum.Week:
                    {
                        var weekBefore = DateTime.Today.WeekBefore();
                        var start = weekBefore.StartOfWeek();
                        var end = weekBefore.EndOfWeek();

                        var res = await trafficAggregatedRepository.GetDataByPeriod(start, end, parameter, Session);
                        var dataList = res.ToList();

                        var dates = Enumerable.Range(0, 1 + end.Subtract(start).Days).Select(offset => start.AddDays(offset)).ToArray();
                        foreach (var r in dataList)
                        {
                            r.Group = (int)dates.Single(x => x.Day == r.Group).DayOfWeek;
                        }

                        res = dataList.ExtendWithEmptyData(dates.Select(x => (int)x.DayOfWeek));

                        result = res.Select(x => new TrafficAggregationDTO
                        {
                            Value = (int)x.Sum,
                            Period = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedDayName((DayOfWeek)x.Group)
                        }).ToList();

                        break;
                    }
                default: return null;
            }

            if (result != null && result.Count() > 1)
            {
                for (var i = 1; i < result.Count(); i++)
                {
                    var previousValue = result.ElementAt(i - 1).Value;
                    var previousPeriod = result.ElementAt(i - 1).Period;
                    var currentValue = result.ElementAt(i).Value;
                    var currentElement = result.ElementAt(i);

                    currentElement.Trend = previousValue == 0 ? 0 : (double)(currentValue - previousValue) / previousValue * 100;
                    currentElement.Comparison = new TrafficComparisonDTO
                    {
                        CurrentPeriod = currentElement.Period,
                        CurrentValue = currentValue,
                        PreviousPeriod = previousPeriod,
                        PreviousValue = previousValue
                    };
                }
            }

            return result;
        }

        public async Task<BaseChartDTO<int>> GetDataForChart(string filter)
        {
            BaseChartDTO<int> result = null;
            Enum.TryParse(filter, true, out PeriodFilterEnum period);

            var parameter = _groupSelectorsForPeriods[period];

            switch (period)
            {
                case PeriodFilterEnum.Year:
                    {
                        var startYearDate = new DateTime(DateTime.Today.YearBefore(10).Year, 1, 1);

                        var res = await trafficAggregatedRepository.GetDataByPeriod(startYearDate, DateTime.Now,
                            parameter, Session);
                        var dataList = res.ToList();

                        var startYear = dataList.Min(x => x.Group);
                        var endYear = dataList.Max(x => x.Group);

                        res = dataList.ExtendWithEmptyData(Enumerable.Range(startYear, endYear - startYear));

                        result = new BaseChartDTO<int>
                        {
                            ChartLabel = "MB",
                            AxisXLabels = res.Select(x => x.Group.ToString()),
                            Lines = new List<ChartDataDTO<int>>
                            {
                                new ChartDataDTO<int>
                                {
                                    Type = null,
                                    Values = res.Select(x=>(int)x.Sum)
                                }
                            }
                        };

                        break;
                    }
                case PeriodFilterEnum.Month:
                    {
                        var res = await trafficAggregatedRepository.GetDataByPeriod(
                            DateTime.Now.AddYears(-1).StartOfYear(), DateTime.Now.AddYears(-1).EndOfYear(), parameter,
                            Session);
                        var dataList = res.ToList();

                        result = new BaseChartDTO<int>
                        {
                            ChartLabel = "MB",
                            AxisXLabels = dataList.Select(x => DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(x.Group)),
                            Lines = new List<ChartDataDTO<int>>
                            {
                                new ChartDataDTO<int>
                                {
                                    Type = null,
                                    Values = dataList.Select(x=>(int)x.Sum)
                                }
                            }
                        };

                        break;
                    }
                case PeriodFilterEnum.Week:
                    {
                        var weekBefore = DateTime.Today.WeekBefore();
                        var start = weekBefore.StartOfWeek();
                        var end = weekBefore.EndOfWeek();

                        var res = await trafficAggregatedRepository.GetDataByPeriod(start, end, parameter, Session);
                        var dataList = res.ToList();

                        var dates = Enumerable.Range(0, 1 + end.Subtract(start).Days).Select(offset => start.AddDays(offset)).ToArray();
                        foreach (var r in dataList)
                        {
                            r.Group = (int)dates.Single(x => x.Day == r.Group).DayOfWeek;
                        }

                        res = dataList.ExtendWithEmptyData(dates.Select(x => (int)x.DayOfWeek));

                        result = new BaseChartDTO<int>
                        {
                            ChartLabel = "MB",
                            AxisXLabels = res.Select(x => DateTimeFormatInfo.InvariantInfo.GetAbbreviatedDayName((DayOfWeek)x.Group)),
                            Lines = new List<ChartDataDTO<int>>
                            {
                                new ChartDataDTO<int>
                                {
                                    Type = null,
                                    Values = res.Select(x=>(int)x.Sum)
                                }
                            }
                        };

                        break;
                    }
            }

            return result;
        }
    }
}
