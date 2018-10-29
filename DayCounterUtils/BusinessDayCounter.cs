﻿using System;
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
            var result = WeekdaysBetweenTwoDates(firstDate, secondDate);

            return result;
        }

        private static IList<DateTime> GetHolidays(int startYear, int endYear,  IList<HolidayRule> holidayRules){
            var result = new List<DateTime>();




            return result;
        }
    }
}
