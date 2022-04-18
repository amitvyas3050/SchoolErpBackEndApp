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
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class OrderAggregationService : BaseService, IOrderAggregationService
    {
        protected readonly IOrderAggregatedRepository orderAggregatedRepository;

        public OrderAggregationService(ICurrentContextProvider contextProvider, IOrderAggregatedRepository orderAggregatedRepository) : base(contextProvider)
        {
            this.orderAggregatedRepository = orderAggregatedRepository;
        }

        public async Task<BaseChartDTO<int>> GetCountDataForChart(string aggregation)
        {
            Dictionary<int, List<AggregatedData>> aggregatedData;
            var statuses = Enum.GetValues(typeof(OrderStatusEnum)).Cast<int>();
            var tenYearsBefore = DateTime.Today.YearBefore(10);

            Enum.TryParse(aggregation, true, out PeriodFilterEnum period);
            switch (period)
            {
                case PeriodFilterEnum.Year:
                    {
                        aggregatedData = await orderAggregatedRepository.GetCountChartDataForPeriod(statuses,
                            tenYearsBefore, DateTime.Now,
                            obj => new AggregatedData { Group = obj.Date.Year, Count = obj.Id }, Session);

                        var keys = new List<int>(aggregatedData.Keys);
                        foreach (var key in keys)
                        {
                            aggregatedData[key] = aggregatedData[key]
                                .ExtendWithEmptyData(Enumerable.Range(tenYearsBefore.Year, 10)).ToList();
                        }

                        break;
                    }
                case PeriodFilterEnum.Month:
                    {
                        aggregatedData = await orderAggregatedRepository.GetCountChartDataForPeriod(statuses,
                            tenYearsBefore, DateTime.Now,
                            obj => new AggregatedData { Group = obj.Date.Month, Count = obj.Id }, Session);

                        var keys = new List<int>(aggregatedData.Keys);
                        foreach (var key in keys)
                        {
                            aggregatedData[key] = aggregatedData[key].ExtendWithEmptyData(Enumerable.Range(1, 12)).ToList();
                        }

                        break;
                    }
                case PeriodFilterEnum.Week:
                    {
                        var weekBefore = DateTime.Today.WeekBefore();
                        var start = weekBefore.StartOfWeek();
                        var end = weekBefore.EndOfWeek();

                        aggregatedData =
                            await orderAggregatedRepository.GetCountChartDataForPeriod(statuses, start,
                                end, obj => new AggregatedData { Group = obj.Date.Day, Count = obj.Id }, Session);

                        var dates = Enumerable.Range(0, 1 + end.Subtract(start).Days)
                            .Select(offset => start.AddDays(offset)).ToArray();

                        var keys = new List<int>(aggregatedData.Keys);
                        foreach (var key in keys)
                        {
                            aggregatedData[key] = aggregatedData[key].Select(x => new AggregatedData
                            {
                                Count = x.Count,
                                Sum = x.Sum,
                                Group = (int)dates.Single(s => s.Day == x.Group).DayOfWeek
                            })
                                .ToList();
                            aggregatedData[key] = aggregatedData[key]
                                .ExtendWithEmptyData(dates.Select(x => (int)x.DayOfWeek)).ToList();
                        }

                        break;
                    }
                default: return null;
            }

            var stats = new BaseChartDTO<int>
            {
                Lines = new List<ChartDataDTO<int>>()
            };

            foreach (var status in aggregatedData)
            {
                stats.AxisXLabels = status.Value.Select(x => FormatAxisXLabels(x.Group, period));
                stats.ChartLabel = null;
                stats.Lines.Add(new ChartDataDTO<int> { Type = ((OrderStatusEnum)status.Key).ToString(), Values = status.Value.Select(x => x.Count) });
            }

            return stats;
        }

        public async Task<BaseChartDTO<int>> GetProfitDataForChart(string aggregation)
        {
            Dictionary<int, List<AggregatedData>> aggregatedData;
            var statuses = new List<int>
                {(int) OrderStatusEnum.Payment, (int) OrderStatusEnum.Canceled, (int) OrderStatusEnum.All};
            var tenYearsBefore = DateTime.Today.YearBefore(10);

            Enum.TryParse(aggregation, true, out PeriodFilterEnum period);

            switch (period)
            {
                case PeriodFilterEnum.Year:
                    {
                        aggregatedData = await orderAggregatedRepository.GetProfitChartDataForPeriod(statuses,
                            tenYearsBefore, DateTime.Now,
                            obj => new AggregatedData { Group = obj.Date.Year, Sum = obj.Value }, Session);

                        var keys = new List<int>(aggregatedData.Keys);
                        foreach (var key in keys)
                        {
                            aggregatedData[key] = aggregatedData[key]
                                .ExtendWithEmptyData(Enumerable.Range(tenYearsBefore.Year, 10)).ToList();
                        }

                        break;
                    }
                case PeriodFilterEnum.Month:
                    {
                        aggregatedData = await orderAggregatedRepository.GetProfitChartDataForPeriod(statuses,
                            tenYearsBefore, DateTime.Now,
                            obj => new AggregatedData { Group = obj.Date.Month, Sum = obj.Value }, Session);

                        var keys = new List<int>(aggregatedData.Keys);
                        foreach (var key in keys)
                        {
                            aggregatedData[key] = aggregatedData[key].ExtendWithEmptyData(Enumerable.Range(1, 12)).ToList();
                        }

                        break;
                    }
                case PeriodFilterEnum.Week:
                    {
                        var weekBefore = DateTime.Today.WeekBefore();
                        var start = weekBefore.StartOfWeek();
                        var end = weekBefore.EndOfWeek();

                        aggregatedData = await orderAggregatedRepository.GetProfitChartDataForPeriod(statuses, start,
                            end, obj => new AggregatedData { Group = obj.Date.Day, Sum = obj.Value }, Session);

                        var dates = Enumerable.Range(0, 1 + end.Subtract(start).Days)
                            .Select(offset => start.AddDays(offset)).ToArray();

                        var keys = new List<int>(aggregatedData.Keys);
                        foreach (var key in keys)
                        {
                            aggregatedData[key] = aggregatedData[key].Select(x => new AggregatedData
                            {
                                Count = x.Count,
                                Sum = x.Sum,
                                Group = (int)dates.Single(s => s.Day == x.Group).DayOfWeek
                            })
                                .ToList();
                            aggregatedData[key] = aggregatedData[key]
                                .ExtendWithEmptyData(dates.Select(x => (int)x.DayOfWeek)).ToList();
                        }

                        break;
                    }
                default: return null;
            }

            var stats = new BaseChartDTO<int>
            {
                Lines = new List<ChartDataDTO<int>>()
            };

            foreach (var status in aggregatedData)
            {
                stats.AxisXLabels = status.Value.Select(x => FormatAxisXLabels(x.Group, period));
                stats.ChartLabel = null;
                stats.Lines.Add(new ChartDataDTO<int>
                { Type = ((OrderStatusEnum)status.Key).ToString(), Values = status.Value.Select(x => (int)x.Sum) });
            }

            return stats;
        }

        public async Task<OrdersSummaryDTO> GetOrdersSummaryInfo()
        {
            var summary = await orderAggregatedRepository.GetOrdersSummaryInfo(Session);

            var ordersSummary = new OrdersSummaryDTO
            {
                LastMonth = summary.LastMonth,
                LastWeek = summary.LastWeek,
                Marketplace = summary.Marketplace,
                Today = summary.Today
            };

            return ordersSummary;
        }

        public async Task<IEnumerable<OrderTypeStatisticDTO>> GetStatisticByCountry(string code)
        {
            var countryStatistics = await orderAggregatedRepository.GetDataGroupedByCountry(code, Session);

            return countryStatistics.Select(x => new OrderTypeStatisticDTO { Count = x.Count, OrderTypeId = x.OrderTypeId });
        }

        public async Task<BaseChartDTO<int>> GetProfitChartForYear()
        {
            var statuses = new List<int> { (int)OrderStatusEnum.Payment, (int)OrderStatusEnum.All };

            var aggregatedData = await orderAggregatedRepository.GetProfitChartDataForPeriod(statuses,
                DateTime.Today.YearBefore(), DateTime.Now,
                obj => new AggregatedData { Group = obj.Date.Month, Sum = obj.Value }, Session);

            var keys = new List<int>(aggregatedData.Keys);
            foreach (var key in keys)
            {
                aggregatedData[key] = aggregatedData[key].ExtendWithEmptyData(Enumerable.Range(1, 12)).ToList();
            }

            var profitForYear = new BaseChartDTO<int>
            {
                AxisXLabels = null,
                ChartLabel = "$",
                Lines = aggregatedData.Select(x =>
                    new ChartDataDTO<int>
                    {
                        Type = x.Key == (int)OrderStatusEnum.Payment ? "transactions" : "orders",
                        Values = x.Value.Select(c => (int)c.Sum)
                    }).ToList()
            };

            return profitForYear;
        }

        public async Task<ProfitForTwoMonthChartDTO> GetProfitChartForTwoMonth()
        {
            var periodStart = DateTime.Today.MonthBefore(2).StartOfMonth();
            var periodEnd = DateTime.Today.MonthBefore(1).EndOfMonth();

            var aggregatedData = await orderAggregatedRepository.GetProfitForDateRangeChartData(Session, periodStart, periodEnd);

            var days = new List<DateTime>();
            for (var i = periodStart; i <= periodEnd; i = i.AddDays(1))
            {
                days.Add(i);
            }

            aggregatedData = aggregatedData.ExtendWithEmptyData(days).OrderBy(x => x.Group);

            var groupedData = new List<int>();
            var agrValue = 0;
            var groupBy = 10;
            for (var i = 0; i < aggregatedData.Count(); i++)
            {
                agrValue += (int)aggregatedData.ElementAt(i).Sum;
                if ((i + 1) % groupBy == 0)
                {
                    groupedData.Add(agrValue);
                    agrValue = 0;
                }
            }

            if (agrValue > 0)
            {
                groupedData.Add(agrValue);
            }

            var profitForTwoMonths = new ProfitForTwoMonthChartDTO
            {
                AxisXLabels = null,
                ChartLabel = "$",
                Lines = new List<ChartDataDTO<int>> {
                     new ChartDataDTO<int>
                    {
                        Type = null,
                        Values = groupedData
                    }
                }
            };

            var sumForFirstMonth = (int)aggregatedData.Where(x => x.Group >= periodStart && x.Group <= periodStart.EndOfMonth()).Sum(x => x.Sum);
            var sumForSecondMonth = (int)aggregatedData.Where(x => x.Group >= periodEnd.StartOfMonth() && x.Group <= periodEnd).Sum(x => x.Sum);

            profitForTwoMonths.AggregatedData = new List<ChartAdditionalInfoDTO<int>> {
                new ChartAdditionalInfoDTO<int> { Value = sumForFirstMonth,
                    Title = $"{periodStart.ToString("dd MMM", CultureInfo.InvariantCulture)} - {periodStart.EndOfMonth().ToString("dd MMM", CultureInfo.InvariantCulture)}" },
                new ChartAdditionalInfoDTO<int> { Value = sumForSecondMonth,
                    Title = $"{periodEnd.StartOfMonth().ToString("dd MMM", CultureInfo.InvariantCulture)} - {periodEnd.ToString("dd MMM", CultureInfo.InvariantCulture)}" }
            };

            return profitForTwoMonths;
        }

        public async Task<OrdersProfitDTO> GetProfitStatistic()
        {
            var profitInfo = await orderAggregatedRepository.GetProfitInfo(Session);

            //Mock data for comments count
            var yesterdayCommentsCount = (int)DateTime.Today.AddHours(12).AddMinutes(37).TimeOfDay.TotalMinutes;
            var currentCommentsCount = (int)DateTime.Now.TimeOfDay.TotalMinutes;

            var ordersProfit = new OrdersProfitDTO
            {
                WeekCommentsProfit = new StatisticUnit<int>
                {
                    Value = currentCommentsCount,
                    Trend = yesterdayCommentsCount == 0 ? 0 : Math.Round((double)(currentCommentsCount - yesterdayCommentsCount) / yesterdayCommentsCount * 100)
                },
                WeekOrdersProfit = new StatisticUnit<int>
                {
                    Value = profitInfo.CurrentWeekCount,
                    Trend = profitInfo.LastWeekCount == 0 ? 0 : Math.Round((double)(profitInfo.CurrentWeekCount - profitInfo.LastWeekCount) / profitInfo.LastWeekCount * 100)
                },
                TodayProfit = new StatisticUnit<int>
                {
                    Value = profitInfo.CurrentWeekProfit,
                    Trend = profitInfo.LastWeekProfit == 0 ? 0 : Math.Round((double)(profitInfo.CurrentWeekProfit - profitInfo.LastWeekProfit) / profitInfo.LastWeekProfit * 100)
                }
            };

            return ordersProfit;
        }

        private string FormatAxisXLabels(int value, PeriodFilterEnum mode)
        {
            switch (mode)
            {
                case PeriodFilterEnum.Year: return value.ToString();
                case PeriodFilterEnum.Month: return DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(value);
                case PeriodFilterEnum.Week:
                    return DateTimeFormatInfo.InvariantInfo.GetAbbreviatedDayName((DayOfWeek)value);
            }

            return value.ToString();
        }
    }
}
