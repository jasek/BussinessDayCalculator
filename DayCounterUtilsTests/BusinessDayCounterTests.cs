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

        [Theory]
        [InlineData(new[] { 2013, 10, 7 }, new[] { 2013, 10, 9 }, 1)]
        [InlineData(new[] { 2013, 10, 5 }, new[] { 2013, 10, 14 }, 5)]
        [InlineData(new[] { 2013, 10, 7 }, new[] { 2014, 1, 1 }, 61)]
        [InlineData(new[] { 2013, 10, 7 }, new[] { 2013, 10, 5 }, 0)]
        public void Test1(int[] firstDataSet, int[] secondDataSet, int expected)
        {
            var firstDate = new DateTime(firstDataSet[0], firstDataSet[1], firstDataSet[2]);
            var secondDate = new DateTime(secondDataSet[0], secondDataSet[1], secondDataSet[2]);
            var result = businessDayCounter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            Assert.Equal(expected, result);
        }
    }
}
