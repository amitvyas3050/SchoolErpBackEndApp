/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using ECommerce.Entities;
using ECommerce.Services.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Tests.RepositoriesTests
{
    [TestClass]
    public class OrderRepository : TestBase<IOrderRepository, Order>
    {
        private static Order _testOrder;

        [ClassInitialize]
        public static async Task ClassInit(TestContext context)
        {
            ClassInitBase();

            _testOrder = new Order
            {
                Name = "Test Order",
                CountryId = _testCountry1.Id,
                Currency = "USD",
                Date = DateTime.Now.AddYears(-2),
                Status = (int) OrderStatusEnum.Canceled,
                Type = (int) OrderTypeEnum.Sofas,
                Value = 24.2M
            };

            await Repository.Edit(_testOrder, ContextSession);
        }

        [TestMethod]
        public async Task OrderRepositoryTests()
        {
            await GetFilteredOrdersTest();
            await GetOrderTest();
            await EditOrderTest();
        }

        [ClassCleanup]
        public static async Task ClassCleanup()
        {
            await Repository.Delete(_testOrder.Id, ContextSession);
        }

        #region Test Methods

        private async Task GetFilteredOrdersTest()
        {
            var filter = new OrdersGridFilter
            {
                PageNumber = 1,
                PageSize = 20,
                FilterByCountry = _testCountry1.Name,
                FilterByDate = _testOrder.Date.Day.ToString(),
                FilterByName = _testOrder.Name,
                FilterByStatus = ((OrderStatusEnum)_testOrder.Status).ToString(),
                FilterBySum = Convert.ToInt32(_testOrder.Value).ToString(),
                FilterByType = ((OrderTypeEnum)_testOrder.Type).ToString()
            };

            var orders = await Repository.GetFilteredListWithTotalCount(filter, ContextSession);

            var actual = orders.Item1.FirstOrDefault();

            Assert.AreEqual(1, orders.Item2,
                $"{nameof(GetFilteredOrdersTest)} - GetFilteredListWithTotalCount() should return single entity by this filter.");
            Assert.IsNotNull(actual,
                $"{nameof(GetFilteredOrdersTest)} - GetFilteredListWithTotalCount() shouldn't return null entity by this filter.");

            //improvement: for case if Id will not assigned to test order entity
            _testOrder.Id = actual.Id;

            Compare(_testOrder, actual, nameof(GetFilteredOrdersTest));
        }

        private async Task GetOrderTest()
        {
            var actual = await Repository.Get(_testOrder.Id, ContextSession);
            Compare(_testOrder, actual, nameof(GetOrderTest));
        }

        private async Task EditOrderTest()
        {
            _testOrder.Name = "Test Order Edited";
            _testOrder.Currency = "BYN";
            _testOrder.Date = _testOrder.Date.AddHours(2);
            _testOrder.Status = (int)OrderStatusEnum.Payment;
            _testOrder.Type = (int)OrderTypeEnum.Textiles;
            _testOrder.Value = 55M;

            await Repository.Edit(_testOrder, ContextSession);

            var actual = await Repository.Get(_testOrder.Id, ContextSession);
            Compare(_testOrder, actual, nameof(EditOrderTest));
        }

        #endregion
        
        protected override void Compare(Order actual, Order expected, string message)
        {
            Assert.AreEqual(expected.Id, actual.Id, message);
            Assert.AreEqual(expected.Name, actual.Name, message);
            Assert.AreEqual(expected.Status, actual.Status, message);
            Assert.AreEqual(expected.Value, actual.Value, message);
            Assert.AreEqual(expected.CountryId, actual.CountryId, message);
            Assert.AreEqual(expected.Currency, actual.Currency, message);
            Assert.AreEqual(expected.Date.Date, actual.Date.Date, message);
            Assert.AreEqual(expected.Type, actual.Type, message);
        }
    }
}
