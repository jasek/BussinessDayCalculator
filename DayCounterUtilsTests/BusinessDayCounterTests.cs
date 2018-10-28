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
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2013, 10, 9);
            var result = businessDayCounter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            Assert.Equal(1, result);
        }
    }
}
