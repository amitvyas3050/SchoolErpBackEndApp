/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using ECommerce.DIContainer;
using Unity;

namespace ECommerce.DataAccess.Tests
{
    public static class Container
    {
        private static readonly IUnityContainer _container;

        static Container()
        {
            if (_container != null) return;
            _container = new UnityContainer();
            _container.AddExtension(new ContainerExtension());
        }

        internal static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
