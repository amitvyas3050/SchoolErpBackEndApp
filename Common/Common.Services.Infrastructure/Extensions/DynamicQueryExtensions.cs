/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Common.Services.Infrastructure
{
    public static class DynamicQueryExtensions
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string sortColumn, bool descending)
        {
            var parameter = Expression.Parameter(typeof(T), "p");

            var orderCommand = descending ? "OrderByDescending" : "OrderBy";

            PropertyInfo property;
            Expression propertyAccess = null;

            var propName = sortColumn;
            if (propName.Contains('.'))
            {
                var childProperties = propName.Split('.');
                property = typeof(T).GetProperty(childProperties[0],
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                if (property != null)
                {
                    propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    for (var i = 1; i < childProperties.Length; i++)
                    {
                        if (property != null)
                        {
                            property = property.PropertyType.GetProperty(childProperties[i],
                                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                            if (property != null)
                                propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                        }
                    }
                }
            }
            else
            {
                property = typeof(T).GetProperty(propName,
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                if (property != null) propertyAccess = Expression.MakeMemberAccess(parameter, property);
            }

            if (property == null) return query;

            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            Expression resultExpression = Expression.Call(typeof(Queryable), orderCommand,
                new[] {typeof(T), property.PropertyType},
                query.Expression, Expression.Quote(orderByExpression));

            return query.Provider.CreateQuery<T>(resultExpression);
        }
    }
}