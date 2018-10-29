using System;
using System.Linq;
using System.Collections.Generic;

namespace DayCounterUtils
{
    public class BusinessDayCounter
    {
        private static readonly DayOfWeek[] weekend = { DayOfWeek.Saturday, DayOfWeek.Sunday };

        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            firstDate = firstDate.Date;
            secondDate = secondDate.Date;
            var daysDiff = (secondDate - firstDate).Days;

            if (daysDiff <= 1)
            {

                return 0;
            }

            var result = daysDiff / 7 * 5 - 1;
            var firstDay = firstDate.DayOfWeek;
            var secondDay = secondDate.DayOfWeek;

            if (secondDay > firstDay)
            {
                result += secondDay - firstDay;
            }
            else if (secondDay < firstDay)
            {
                var reverseDiff = 7 - (firstDay - secondDay);
                if (firstDay < DayOfWeek.Saturday)
                {
                    reverseDiff--;
                }
                if (secondDay > DayOfWeek.Sunday)
                {
                    reverseDiff--;
                }
                result += reverseDiff;
            }

            return result;
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime>
       publicHolidays)
        {
            var result = WeekdaysBetweenTwoDates(firstDate, secondDate);

            foreach (var holiday in publicHolidays)
            {
                if (holiday > firstDate && holiday < secondDate && !weekend.Contains(holiday.DayOfWeek))
                {
                    result--;
                }
            }

            return result;
        }


        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<HolidayRule> holidayRules)
        {
            var holidayList = GetHolidays(firstDate.Year, secondDate.Year, holidayRules);
            var result = BusinessDaysBetweenTwoDates(firstDate, secondDate, holidayList);

            return result;
        }

        private static IList<DateTime> GetHolidays(int startYear, int endYear, IList<HolidayRule> holidayRules)
        {
            var result = new List<DateTime>();

            for (int i = startYear; i <= endYear; i++)
            {
                result.AddRange(GetHolidays(i, holidayRules));
            }

            return result;
        }

        private static IList<DateTime> GetHolidays(int year, IList<HolidayRule> holidayRules)
        {
            var result = new List<DateTime>();

            foreach (var rule in holidayRules)
            {
                if (rule.HolidayType == HolidayType.Fixed)
                {
                    result.Add(new DateTime(year, rule.Month, rule.Day));
                }
                else if (rule.HolidayType == HolidayType.FixedWithSubstitiute)
                {
                    result.Add(GetHolidayWithSubstitute(year, rule.Month, rule.Day));
                }
                else
                {
                    result.Add(GetNthDay(year, rule.Month, rule.DayOfWeek, rule.Occurence));
                }
            }

            return result;
        }

        private static DateTime GetHolidayWithSubstitute(int year, int month, int day)
        {
            var date = new DateTime(year, month, day);
            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                date = date.AddDays(2);
            }
            else if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
            }
            return date;
        }

        private static DateTime GetNthDay(int year, int month, DayOfWeek dayOfWeek, Occurence occurence)
        {
            var result = new DateTime(year, month, 1);
            if (result.DayOfWeek != dayOfWeek)
            {
                var dayDifference = dayOfWeek - result.DayOfWeek;
                dayDifference = dayDifference > 0 ? dayDifference : 7 + dayDifference;
                result = result.AddDays(dayDifference);
            }
            if (occurence > Occurence.First)
            {
                result = result.AddDays(7 * (int)occurence);
            }

            return result;
        }
    }
}
