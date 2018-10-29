using System;
namespace DayCounterUtils
{
    public class HolidayRule
    {
        public HolidayRule(Order order, DayOfWeek dayOfWeek, int month)
        {
            Order = order;
            DayOfWeek = dayOfWeek;
            Month = month;
            HolidayType = HolidayType.Dynamic;
        }

        public HolidayRule(int day, int month, bool withSubstitute)
        {
            Day = day;
            Month = month;
            HolidayType = withSubstitute ? HolidayType.FixedWithSubstitiute : HolidayType.Fixed;
        }

        public HolidayType HolidayType
        {
            get;
            internal set;
        }

        public int Month
        {
            get;
            internal set;
        }

        public int Day
        {
            get;
            internal set;
        }

        public DayOfWeek DayOfWeek
        {
            get;
            internal set;
        }

        public Order Order
        {
            get;
            internal set;
        }
    }
}
