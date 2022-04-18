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
    public static class AppFeatureGridFilterExtensions
    {
        public static IQueryable<Entities.AppFeature> ApplyFilter(this IQueryable<AppFeature> query, AppFeatureGridFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.FilterByName))
            {
                query = query.Where(obj => obj.Name.ToLower().Contains(filter.FilterByName.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.FilterByMenuText))
            {
                query = query.Where(obj => obj.MenuText.ToString().Contains(filter.FilterByMenuText));
            }
            if (!string.IsNullOrWhiteSpace(filter.FilterByMenuIcon))
            {
                query = query.Where(obj => obj.MenuIcon.ToLower().Contains(filter.FilterByMenuIcon.ToLower()));
            }
                      
            if (!string.IsNullOrWhiteSpace(filter.SortBy) && !string.IsNullOrWhiteSpace(filter.OrderBy))
            {
                var dictionary = new Dictionary<string, string>
                {
                    { "name", "Name" },
                    { "menuText", "MenuText" },
                    { "menuIcon", "MenuIcon" },
                    { "bgColor1", "BgColor1" },
                    { "bgColor2", "BgColor2" }
                };

                query = query.OrderByDynamic(dictionary[filter.SortBy], filter.OrderBy == "DESC");
            }
            else
            {
                query = query.OrderBy(obj => obj.Name);
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