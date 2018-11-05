using System;
namespace DayCounterUtils
{
    public class HolidayRule
    {
        public HolidayRule(Occurence occurence, DayOfWeek dayOfWeek, int month)
        {
            Occurence = occurence;
            DayOfWeek = dayOfWeek;
            Month = month;
            HolidayType = HolidayType.Dynamic;
        }

        public HolidayRule(int day, int month, bool withSubstitute)
        {
            Day = day;
            Month = month;
            HolidayType = withSubstitute ? HolidayType.FixedWithSubstitute : HolidayType.Fixed;
        }

        public HolidayType HolidayType
        {
            get;
        }

        public int Month
        {
            get;
        }

        public int Day
        {
            get;
        }

        public DayOfWeek DayOfWeek
        {
            get;
        }

        public Occurence Occurence
        {
            get;
        }
    }
}
