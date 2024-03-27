using FluentAssertions;
using Soenneker.Tests.Unit;
using System;
using Xunit;

namespace Soenneker.Extensions.DateTime.Month.Tests;

public class DateTimeMonthExtensionTests : UnitTest
{

    [Theory]
    [InlineData("2023-08-15", "2023-08-01")]
    [InlineData("2023-02-28", "2023-02-01")]
    public void ToStartOfMonth_ReturnsFirstDayOfMonthAtMidnight(string testDate, string expectedDate)
    {
        // Arrange
        System.DateTime dateTime = System.DateTime.Parse(testDate);

        // Act
        System.DateTime result = dateTime.ToStartOfMonth();

        // Assert
        result.Should().Be(System.DateTime.Parse(expectedDate));
    }

    [Theory]
    [InlineData("2023-03-01", "2023-03-31 23:59:59.9999999")]
    [InlineData("2023-02-15", "2023-02-28 23:59:59.9999999")] // Leap year not accounted
    public void ToEndOfMonth_ReturnsLastMomentOfMonth(string testDate, string expectedDate)
    {
        // Arrange
        System.DateTime dateTime = System.DateTime.Parse(testDate);

        // Act
        System.DateTime result = dateTime.ToEndOfMonth();

        // Assert
        result.Should().BeCloseTo(System.DateTime.Parse(expectedDate), TimeSpan.FromMilliseconds(1));
    }

    [Theory]
    [InlineData("2023-03-15", "2023-04-01")]
    [InlineData("2023-12-31", "2024-01-01")]
    public void ToStartOfNextMonth_ReturnsFirstDayOfNextMonthAtMidnight(string testDate, string expectedDate)
    {
        // Arrange
        System.DateTime dateTime = System.DateTime.Parse(testDate);

        // Act
        System.DateTime result = dateTime.ToStartOfNextMonth();

        // Assert
        result.Should().Be(System.DateTime.Parse(expectedDate));
    }

    [Theory]
    [InlineData("2023-03-15", "2023-02-01")]
    [InlineData("2023-01-01", "2022-12-01")]
    public void ToStartOfPreviousMonth_ReturnsFirstDayOfPreviousMonthAtMidnight(string testDate, string expectedDate)
    {
        // Arrange
        System.DateTime dateTime = System.DateTime.Parse(testDate);

        // Act
        System.DateTime result = dateTime.ToStartOfPreviousMonth();

        // Assert
        result.Should().Be(System.DateTime.Parse(expectedDate));
    }
}
