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
    public static class RolePermissionGridFilterExtensions
    {

        public static IQueryable<Entities.RolePermission> ApplyFilter(this IQueryable<RolePermission> query, RolePermissionGridFilter filter)
        {

            if (!string.IsNullOrWhiteSpace(filter.FilterByRole))
            {
                query = query.Where(obj => obj.Role.Name.ToLower().Contains(filter.FilterByRole.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.FilterByAppModule))
            {
                query = query.Where(obj => obj.AppModule.Name.ToLower().Contains(filter.FilterByAppModule.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.FilterByAppFeature))
            {
                query = query.Where(obj => obj.AppFeature.Name.ToLower().Contains(filter.FilterByAppFeature.ToLower()));
            }



            if (!string.IsNullOrWhiteSpace(filter.SortBy) && !string.IsNullOrWhiteSpace(filter.OrderBy))
            {
                var dictionary = new Dictionary<string, string>
                {
                    { "role", "Role.Name" },
                    { "appModule", "AppModule.Name" },
                    { "appfeature", "AppFeature.Name" },
                    { "canAdd", "CanAdd" },
                    { "canEdit", "CanEdit" },
                    { "canDelete", "CanDelete" },
                    { "canView", "CanView" }
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