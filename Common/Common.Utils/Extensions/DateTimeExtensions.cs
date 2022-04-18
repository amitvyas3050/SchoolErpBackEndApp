/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System;

namespace Common.Utils.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime WeekBefore(this DateTime dt, int shift = 1)
        {
            return dt.Date.AddDays(-7 * shift);
        }

        public static DateTime YearBefore(this DateTime dt, int shift = 1)
        {
            return dt.Date.AddYears(-1 * shift);
        }

        public static DateTime MonthBefore(this DateTime dt, int shift = 1)
        {
            return dt.Date.AddMonths(-1 * shift);
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            var diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime EndOfWeek(this DateTime dt, DayOfWeek endOfWeek = DayOfWeek.Sunday)
        {
            var diff = (7 + (endOfWeek - dt.DayOfWeek)) % 7;
            return dt.AddDays(diff).Date;
        }

        public static DateTime StartOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime EndOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
        }

        public static DateTime StartOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 1, 1);
        }

        public static DateTime EndOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 12, DateTime.DaysInMonth(dt.Year, dt.Month));
        }
    }
}
