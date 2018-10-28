using System;
using DayCounterUtils;
using Xunit;

namespace DayCounterUtilsTests
{
    public class BusinessDayCounterTests
    {
        private readonly BusinessDayCounter businessDayCounter;

        public BusinessDayCounterTests()
        {
            businessDayCounter = new BusinessDayCounter();
        }

        [Fact]
        public void Test1()
        {
            var firstDate = new DateTime();
            var secondDate = new DateTime();
            var result = businessDayCounter.WeekdaysBetweenTwoDates(firstDate, secondDate);

        }
    }
}
