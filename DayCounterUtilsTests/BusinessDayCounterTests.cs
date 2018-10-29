using System;
using System.Collections.Generic;
using DayCounterUtils;
using Xunit;

namespace DayCounterUtilsTests
{
    public class BusinessDayCounterTests
    {
        private readonly BusinessDayCounter businessDayCounter;
        private readonly static IList<DateTime> publicHolidays = new List<DateTime> {
            new DateTime(2013, 12, 25),
            new DateTime(2013, 12, 26),
            new DateTime(2014, 1, 1)
        };
        private readonly IList<HolidayRule> holidayRules = new List<HolidayRule>
        {
            new HolidayRule(25, 4, false),
            new HolidayRule(1,1, true),
            new HolidayRule(Occurence.Second, DayOfWeek.Monday, 6)
        };

        private static DateTime ArrayToDate(int[] dataSet)
        {
            return new DateTime(dataSet[0], dataSet[1], dataSet[2]);
        }

        public BusinessDayCounterTests()
        {
            businessDayCounter = new BusinessDayCounter();
        }

        [Theory]
        [InlineData(new[] { 2013, 10, 7 }, new[] { 2013, 10, 9 }, 1)]
        [InlineData(new[] { 2013, 10, 5 }, new[] { 2013, 10, 14 }, 5)]
        [InlineData(new[] { 2013, 10, 7 }, new[] { 2014, 1, 1 }, 61)]
        [InlineData(new[] { 2013, 10, 7 }, new[] { 2013, 10, 5 }, 0)]
        [InlineData(new[] { 2018, 10, 26 }, new[] { 2018, 10, 29 }, 0)]
        [InlineData(new[] { 2018, 10, 27 }, new[] { 2018, 10, 28 }, 0)]
        [InlineData(new[] { 2018, 10, 20 }, new[] { 2018, 10, 28 }, 5)]
        [InlineData(new[] { 2018, 10, 20 }, new[] { 2018, 10, 30 }, 6)]
        [InlineData(new[] { 2018, 10, 20 }, new[] { 2018, 10, 23 }, 1)]
        [InlineData(new[] { 2018, 10, 19 }, new[] { 2018, 10, 26 }, 4)]
        public void WeekdaysBetweenTwoDates_ShouldReturnGoodValueForCorrectData(int[] firstDataSet, int[] secondDataSet, int expected)
        {
            var firstDate = ArrayToDate(firstDataSet);
            var secondDate = ArrayToDate(secondDataSet);
            var result = businessDayCounter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new[] { 2013, 10, 7 }, new[] { 2013, 10, 7 }, 0)]
        [InlineData(new[] { 2013, 10, 7 }, new[] { 2012, 10, 7 }, 0)]
        public void WeekdaysBetweenTwoDates_ShouldReturnZeroIfEqualOrBefore(int[] firstDataSet, int[] secondDataSet, int expected)
        {
            var firstDate = ArrayToDate(firstDataSet);
            var secondDate = ArrayToDate(secondDataSet);
            var result = businessDayCounter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new[] { 2013, 10, 7 }, new[] { 2013, 10, 9 }, 1)]
        [InlineData(new[] { 2013, 12, 24 }, new[] { 2013, 12, 27 }, 0)]
        [InlineData(new[] { 2013, 10, 7 }, new[] { 2014, 1, 1 }, 59)]
        public void BusinessDaysBetweenTwoDates_ShouldReturnCorrectResult(int[] firstDataSet, int[] secondDataSet, int expected)
        {
            var firstDate = ArrayToDate(firstDataSet);
            var secondDate = ArrayToDate(secondDataSet);
            var result = businessDayCounter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new[] { 2017, 1, 1 }, new[] { 2017, 1, 6 }, 3)]
        [InlineData(new[] { 2018, 6, 8 }, new[] { 2018, 6, 13 }, 1)]
        [InlineData(new[] { 2018, 4, 23 }, new[] { 2018, 4, 27 }, 2)]
        [InlineData(new[] { 2015, 4, 23 }, new[] { 2015, 4, 28 }, 2)]
        [InlineData(new[] { 2014, 12, 31 }, new[] { 2015, 1, 5 }, 1)]
        public void BusinessDaysBetweenTwoDates_ShouldWorkWithRules(int[] firstDataSet, int[] secondDataSet, int expected)
        {
            var firstDate = ArrayToDate(firstDataSet);
            var secondDate = ArrayToDate(secondDataSet);
            var result = businessDayCounter.BusinessDaysBetweenTwoDates(firstDate, secondDate, holidayRules);

            Assert.Equal(expected, result);
        }
    }
}
