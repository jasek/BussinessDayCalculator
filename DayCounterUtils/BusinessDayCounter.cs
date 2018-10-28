using System;
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


            var span = secondDate - firstDate;
            var result = span.Days - 1;
            return result;
        }
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime>
       publicHolidays)
        {
            return 0;
        }
    }
}
