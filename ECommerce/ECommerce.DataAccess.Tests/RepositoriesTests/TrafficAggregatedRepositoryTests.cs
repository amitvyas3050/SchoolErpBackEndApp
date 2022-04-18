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
    public class TrafficAggregatedRepository : TestBase
    {

        private static ITrafficAggregatedRepository _trafficAggregatedRepository;
        private static ITrafficRepository _trafficRepository;
        private static List<Traffic> _testTrafficEntities;

        [ClassInitialize]
        public static async Task ClassInit(TestContext context)
        {
            _trafficAggregatedRepository = Container.Resolve<ITrafficAggregatedRepository>();
            _trafficRepository = Container.Resolve<ITrafficRepository>();
            _testTrafficEntities = new List<Traffic>();

            foreach (var i in Enumerable.Range(1, 20))
            {
                var trafficEntity = new Traffic
                {
                    Date = DateTime.Now.YearBefore(500).AddHours(i),
                    Value = 40 + i * new Random().Next(-1, 1)
                };
                _testTrafficEntities.Add(trafficEntity);
                await _trafficRepository.Edit(trafficEntity, ContextSession);
            }
        }

        [TestMethod]
        public async Task TrafficAggregatedRepositoryTests()
        {
            await GetDataByPeriodTest();
        }

        [ClassCleanup]
        public static async Task ClassCleanup()
        {
            foreach (var trafficEntity in _testTrafficEntities)
            {
                await _trafficRepository.Delete(trafficEntity.Id, ContextSession);
            }
        }

        private async Task GetDataByPeriodTest()
        {
            var data = await _trafficAggregatedRepository.GetDataByPeriod(DateTime.Today.YearBefore(500).StartOfWeek(),
                DateTime.Today.YearBefore(500).EndOfWeek(),
                obj => new AggregatedData { Group = obj.Date.Day, Sum = obj.Value }, ContextSession);

            Assert.AreEqual(_testTrafficEntities.Sum(x => x.Value), data.Sum(x => x.Sum),
                $"{nameof(GetDataByPeriodTest)} - GetDataByPeriod() should return data with values in sum = {_testTrafficEntities.Sum(x => x.Value)}");
        }
    }
}