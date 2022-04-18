/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using ECommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ECommerce.DataAccess.EFCore.Extensions
{
    public static class GridFilterExtensions
    {
        public static IQueryable<Order> ApplyFilter(this IQueryable<Order> query, OrdersGridFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.FilterByName))
            {
                query = query.Where(obj => obj.Name.ToLower().Contains(filter.FilterByName.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.FilterByDate))
            {
                query = query.Where(obj => obj.Date.ToString().Contains(filter.FilterByDate));
            }
            if (!string.IsNullOrWhiteSpace(filter.FilterByCountry))
            {
                query = query.Where(obj => obj.Country.Name.ToLower().Contains(filter.FilterByCountry.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.FilterByStatus))
            {
                var statuses = GetEnumValuesByFilter<OrderStatusEnum>(filter.FilterByStatus)
                    .Select(s => (int)s);
                query = query.Where(obj => statuses.Contains(obj.Status));
            }
            if (!string.IsNullOrWhiteSpace(filter.FilterByType))
            {
                var types = GetEnumValuesByFilter<OrderTypeEnum>(filter.FilterByType)
                    .Select(s => (int)s);
                query = query.Where(obj => types.Contains(obj.Type));
            }
            if (!string.IsNullOrWhiteSpace(filter.FilterBySum))
            {
                query = query.Where(obj => obj.Value.ToString().Contains(filter.FilterBySum));
            }
            if (!string.IsNullOrWhiteSpace(filter.SortBy) && !string.IsNullOrWhiteSpace(filter.OrderBy))
            {
                var dictionary = new Dictionary<string, string>
                {
                    { "name", "Name" },
                    { "date", "Date" },
                    { "sum", "Value" },
                    { "type", "Type" },
                    { "country", "Country.Name" },
                    { "status", "Status" }
                };

                query = query.OrderByDynamic(dictionary[filter.SortBy], filter.OrderBy == "DESC");
            }
            else
            {
                query = query.OrderByDescending(obj => obj.CreatedDate);
            }

            return query;
        }

        private static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string sortColumn, bool descending)
        {
            var parameter = Expression.Parameter(typeof(T), "p");

            var orderCommand = descending ? "OrderByDescending" : "OrderBy";

            PropertyInfo property;
            Expression propertyAccess = null;

            var propName = sortColumn;
            if (propName.Contains('.'))
            {
                var childProperties = propName.Split('.');
                property = typeof(T).GetProperty(childProperties[0], BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

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
                property = typeof(T).GetProperty(propName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                if (property != null) propertyAccess = Expression.MakeMemberAccess(parameter, property);
            }

            if (property == null) return query;

            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            Expression resultExpression = Expression.Call(typeof(Queryable), orderCommand, new[] { typeof(T), property.PropertyType },
                query.Expression, Expression.Quote(orderByExpression));

            return query.Provider.CreateQuery<T>(resultExpression);
        }

        private static IEnumerable<TEnum> GetEnumValuesByFilter<TEnum>(string filter) where TEnum : struct, IConvertible
        {
            return typeof(TEnum).IsEnum
                ? Enum.GetValues(typeof(TEnum))
                    .Cast<TEnum>()
                    .Where(s => s.ToString().ToLower().Contains(filter.ToLower()))
                : Enumerable.Empty<TEnum>();
        }
    }
}