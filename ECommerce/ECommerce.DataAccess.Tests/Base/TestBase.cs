/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using ECommerce.Entities;
using ECommerce.Services.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Tests
{
    [TestClass]
    public abstract class TestBase
    {
        private static ContextSession _contextSession;

        protected static Country _testCountry1;
        protected static Country _testCountry2;
        // UserId = 1 - replace it with TestUserId
        protected static ContextSession ContextSession =>
            _contextSession ?? (_contextSession = new ContextSession {UserId = 1});

        private static ICountryRepository _countryRepository;

        [AssemblyInitialize]
        public static async Task AssemblyInit(TestContext context)
        {
            _countryRepository = Container.Resolve<ICountryRepository>();
            _testCountry1 =
                await _countryRepository.Edit(new Country { Code = "TEST1", Name = "Test Country 1" }, ContextSession);
            _testCountry2 =
                await _countryRepository.Edit(new Country { Code = "TEST2", Name = "Test Country 2" }, ContextSession);
        }

        [AssemblyCleanup]
        public static async Task AssemblyCleanup()
        {
            await _countryRepository.Delete(_testCountry1.Id, ContextSession);
            await _countryRepository.Delete(_testCountry2.Id, ContextSession);
        }
    }

    public abstract class TestBase<TRepository, TEntity>: TestBase where TEntity : BaseEntity, new()
    {
        protected static TRepository Repository;
        protected abstract void Compare(TEntity actual, TEntity expected, string message);

        protected static void ClassInitBase()
        {
            Repository = Container.Resolve<TRepository>();
        }
    }
}