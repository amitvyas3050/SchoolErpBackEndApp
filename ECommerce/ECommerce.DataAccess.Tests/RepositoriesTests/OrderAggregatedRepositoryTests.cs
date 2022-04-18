/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities.Statistics;
using Common.Utils.Extensions;
using ECommerce.Entities;
using ECommerce.Services.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Tests.RepositoriesTests
{
    [TestClass]
    public class OrderAggregatedRepository : TestBase
    {
        private static IOrderAggregatedRepository _orderAggregatedRepository;
        private static IOrderRepository _orderRepository;
        private static List<Order> _testOrders;

        [ClassInitialize]
        public static async Task ClassInit(TestContext context)
        {
            _orderAggregatedRepository = Container.Resolve<IOrderAggregatedRepository>();
            _orderRepository = Container.Resolve<IOrderRepository>();
            _testOrders = new List<Order>();

            foreach (var i in Enumerable.Range(1, 20))
            {
                var order = new Order
                {
                    Name = $"Test Order {i}",
                    CountryId = _testCountry2.Id,
                    Currency = "USD",
                    Date = DateTime.Now.Date.YearBefore(500).AddHours(i),
                    Status = new Random().Next(1, 3),
                    Type = new Random().Next(1, 5),
                    Value = 40M + i * new Random().Next(-1, 1)
                };
                _testOrders.Add(order);
                await _orderRepository.Edit(order, ContextSession);
            }
        }

        [TestMethod]
        public async Task OrderAggregatedRepositoryTests()
        {
            await GetProfitInfoTest();
            await GetOrdersSummaryInfoTest();
            await GetDataGroupedByCountryTest();
            await GetCountChartDataForPeriodTest();
            await GetProfitChartDataForPeriodTest();
            await GetProfitForDateRangeChartDataTest();
        }

        [ClassCleanup]
        public static async Task ClassCleanup()
        {
            foreach (var order in _testOrders)
            {
                await _orderRepository.Delete(order.Id, ContextSession);
            }
        }

        #region Test Methods

        private async Task GetProfitInfoTest()
        {
            var profitInfo = await _orderAggregatedRepository.GetProfitInfo(ContextSession);
            Assert.IsNotNull(profitInfo, nameof(GetProfitInfoTest));
        }

        private async Task GetOrdersSummaryInfoTest()
        {
            var ordersSummary = await _orderAggregatedRepository.GetOrdersSummaryInfo(ContextSession);
            Assert.IsNotNull(ordersSummary, nameof(GetOrdersSummaryInfoTest));
        }

        private async Task GetDataGroupedByCountryTest()
        {
            var data = await _orderAggregatedRepository.GetDataGroupedByCountry(_testCountry2.Code, ContextSession);
            Assert.IsNotNull(data,
                $"{nameof(GetDataGroupedByCountryTest)} - GetDataGroupedByCountry() shouldn't return null value.");
            Assert.AreEqual(_testOrders.Count, data.Sum(s => s.Count),
                $"{nameof(GetDataGroupedByCountryTest)} - GetDataGroupedByCountry() should return {_testOrders.Count} orders count in sum.");
        }

        private async Task GetCountChartDataForPeriodTest()
        {
            var data = await _orderAggregatedRepository.GetCountChartDataForPeriod(
                Enum.GetValues(typeof(OrderStatusEnum)).Cast<int>(), DateTime.Today.YearBefore(500).StartOfWeek(),
                DateTime.Today.YearBefore(500).EndOfWeek(), obj => new AggregatedData {Group = obj.Date.Month, Count = obj.Id},
                ContextSession);

            CountChartDataCheck(data, nameof(GetCountChartDataForPeriodTest), "GetCountChartDataForPeriod()");
        }

        private async Task GetProfitChartDataForPeriodTest()
        {
            var data = await _orderAggregatedRepository.GetProfitChartDataForPeriod(
                Enum.GetValues(typeof(OrderStatusEnum)).Cast<int>(), DateTime.Today.YearBefore(500).StartOfWeek(),
                DateTime.Today.YearBefore(500).EndOfWeek(), obj => new AggregatedData {Group = obj.Date.Month, Sum = obj.Value},
                ContextSession);

            ProfitChartDataCheck(data, nameof(GetProfitChartDataForPeriodTest), "GetProfitChartDataForPeriod()");
        }
        
        private async Task GetProfitForDateRangeChartDataTest()
        {
            var data = await _orderAggregatedRepository.GetProfitForDateRangeChartData(ContextSession,
                DateTime.Today.YearBefore(500).StartOfWeek(), DateTime.Today.YearBefore(500).EndOfWeek());

            var aggregatedData = data.ToList();

            Assert.AreEqual(_testOrders.Count, aggregatedData.Count,
                $"{nameof(GetProfitForDateRangeChartDataTest)} - GetProfitForDateRangeChartData() should return {_testOrders.Count} aggregated items.");
            Assert.AreEqual(_testOrders.Sum(o => o.Value), aggregatedData.Sum(d => d.Sum),
                $"{nameof(GetProfitForDateRangeChartDataTest)} - GetProfitForDateRangeChartData() returned data should have profit in sum = {_testOrders.Sum(o => o.Value)}.");
        }

        private void ProfitChartDataCheck(Dictionary<int, List<AggregatedData>> data, string testName,
            string repositoryMethodName)
        {
            Assert.IsNotNull(data,
                $"{testName} - {repositoryMethodName} shouldn't return null value.");

            var allOrdersProfitExpected = _testOrders.Sum(o => o.Value);
            var allOrdersProfitActual = data.First(x => x.Key == (int) OrderStatusEnum.All).Value.Sum(s => s.Sum);
            var ordersProfitInSumActual =
                data.Where(x => x.Key != (int) OrderStatusEnum.All).Sum(x => x.Value.Sum(s => s.Sum));

            Assert.AreEqual(allOrdersProfitExpected, allOrdersProfitActual,
                $"{testName} - {repositoryMethodName} should return Sum = {allOrdersProfitExpected} for order type = '{OrderStatusEnum.All}'.");
            Assert.AreEqual(allOrdersProfitExpected, ordersProfitInSumActual,
                $"{testName} - {repositoryMethodName} should return Sum = {allOrdersProfitExpected} in sum for other order types.");

            var paymentOrdersProfitExpected =
                _testOrders.Where(o => o.Status == (int) OrderStatusEnum.Payment).Sum(s => s.Value);
            var paymentOrdersProfitActual =
                data.FirstOrDefault(x => x.Key == (int) OrderStatusEnum.Payment).Value.Sum(s => s.Sum);
            Assert.AreEqual(paymentOrdersProfitExpected, paymentOrdersProfitActual,
                $"{testName} - {repositoryMethodName} returns incorrect Sum for {OrderStatusEnum.Payment} orders.");

            var canceledOrdersProfitExpected =
                _testOrders.Where(o => o.Status == (int) OrderStatusEnum.Canceled).Sum(s => s.Value);
            var canceledOrdersProfitActual =
                data.FirstOrDefault(x => x.Key == (int) OrderStatusEnum.Canceled).Value.Sum(s => s.Sum);
            Assert.AreEqual(canceledOrdersProfitExpected, canceledOrdersProfitActual,
                $"{testName} - {repositoryMethodName} returns incorrect Sum for {OrderStatusEnum.Canceled} orders.");
        }

        private void CountChartDataCheck(Dictionary<int, List<AggregatedData>> data, string testName, string repositoryMethodName)
        {
            Assert.IsNotNull(data, $"{testName} - {repositoryMethodName} shouldn't return null value.");

            var allCount = data.First(x => x.Key == (int)OrderStatusEnum.All).Value.Sum(s => s.Count);
            var allSumCount = data.Where(x => x.Key != (int)OrderStatusEnum.All).Sum(x => x.Value.Sum(s => s.Count));

            Assert.AreEqual(_testOrders.Count, allCount,
                $"{testName} - {repositoryMethodName} should return {_testOrders.Count} orders count for order type = '{OrderStatusEnum.All}'.");
            Assert.AreEqual(_testOrders.Count, allSumCount,
                $"{testName} - {repositoryMethodName} should return {_testOrders.Count} orders count in sum for other order types.");
        }

        #endregion
    }
}
