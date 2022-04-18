/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System.Collections.Generic;
using System.Linq;
using Common.Entities;

namespace Common.Services.Infrastructure
{
    public static class QueryableFilterExtensions
    {
        public static IQueryable<TUser> ApplyFilter<TUser>(this IQueryable<TUser> query, UsersGridFilter filter)
            where TUser : BaseUser, new()
        {
            // map dictionary for User object
            var mapDictionary = new Dictionary<string, string>
            {
                {"firstName", "FirstName"},
                {"lastName", "LastName"},
                {"login", "Login"},
                {"email", "Email"},
                {"age", "Age"},
                {"street", "AddressStreet"},
                {"city", "AddressCity"},
                {"zipcode", "AddressZipCode"}
            };

            if (!string.IsNullOrWhiteSpace(filter.FilterByFirstName))
            {
                query = query.Where(obj => obj.FirstName.ToLower().Contains(filter.FilterByFirstName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.FilterByLastName))
            {
                query = query.Where(obj => obj.LastName.ToLower().Contains(filter.FilterByLastName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.FilterByLogin))
            {
                query = query.Where(obj => obj.Login.ToLower().Contains(filter.FilterByLogin.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.FilterByEmail))
            {
                query = query.Where(obj => obj.Email.ToLower().Contains(filter.FilterByEmail.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.FilterByAge))
            {
                int.TryParse(filter.FilterByAge, out var age);
                query = query.Where(obj => obj.Age == age);
            }

            if (!string.IsNullOrWhiteSpace(filter.FilterByStreet))
            {
                query = query.Where(obj => obj.AddressStreet.ToLower().Contains(filter.FilterByStreet.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.FilterByCity))
            {
                query = query.Where(obj => obj.AddressCity.ToLower().Contains(filter.FilterByCity.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.FilterByZipCode))
            {
                query = query.Where(obj => obj.AddressZipCode.ToLower().Contains(filter.FilterByZipCode.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.SortBy) && !string.IsNullOrWhiteSpace(filter.OrderBy))
            {
                query = query.OrderByDynamic(mapDictionary[filter.SortBy], filter.OrderBy == "DESC");
            }
            else
            {
                query = query.OrderByDescending(obj => obj.Login);
            }

            return query;
        }
    }
}