using System;
using System.Collections.Generic;

namespace DayCounterUtils
{
    public class BusinessDayCounter
    {
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            firstDate = firstDate.Date;
            secondDate = secondDate.Date;

            if (firstDate.AddDays(1) >= secondDate)
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
