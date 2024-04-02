using FluentAssertions;
using Soenneker.Tests.Unit;
using System;
using Soenneker.Utils.TimeZones;
using Xunit;

namespace Soenneker.Extensions.DateTime.Month.Tests;

public class DateTimeMonthExtensionTests : UnitTest
{
    private readonly System.DateTime _utcNow;

    public DateTimeMonthExtensionTests()
    {
        _utcNow = System.DateTime.UtcNow;
    }

    [Fact]
    public void ToStartOfMonth_should_return_expected()
    {
        System.DateTime result = _utcNow.ToStartOfMonth();
        result.Day.Should().Be(1);
        result.Hour.Should().Be(0);
    }

    [Fact]
    public void ToStartOfNextMonth_should_return_expected()
    {
        System.DateTime result = _utcNow.ToStartOfNextMonth();
        result.Day.Should().Be(1);
        result.Hour.Should().Be(0);
    }

    [Fact]
    public void ToStartOfMonth_kind_should_be_utc()
    {
        System.DateTime result = _utcNow.ToStartOfMonth();
        result.Kind.Should().Be(DateTimeKind.Utc);
    }

    [Fact]
    public void ToEndOfCurrentTzMonth_should_return_expected()
    {
        System.DateTime result = _utcNow.ToEndOfTzMonth(Tz.Eastern);
        System.DateTime converted = result.ToTz(Tz.Eastern);
        converted.Day.Should().BeGreaterThan(27);
        converted.Hour.Should().Be(23);
        converted.Minute.Should().Be(59);
        converted.Second.Should().Be(59);
    }

    [Fact]
    public void ToEndOfPreviousTzMonth_should_return_expected()
    {
        System.DateTime result = _utcNow.ToEndOfPreviousTzMonth(Tz.Eastern);
        System.DateTime converted = result.ToTz(Tz.Eastern);
        converted.Day.Should().BeGreaterThan(27);
        converted.Hour.Should().Be(23);
        converted.Minute.Should().Be(59);
        converted.Second.Should().Be(59);
    }

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

    [Fact]
    public void ToEndOfMonth_Should_HandleLeapYearFebruary()
    {
        // Arrange
        var leapYearDate = new System.DateTime(2024, 2, 14); // February 14, 2024 - a leap year
        var expected = new System.DateTime(2024, 2, 29, 23, 59, 59, 999); // Expected: February 29, 2024, the last moment of the day

        // Act
        System.DateTime result = leapYearDate.ToEndOfMonth();

        // Assert
        result.Should().BeCloseTo(expected, precision: TimeSpan.FromMilliseconds(1));
    }

    [Fact]
    public void ToStartOfNextMonth_Should_HandleDecemberToJanuaryTransition()
    {
        // Arrange
        var dateTime = new System.DateTime(2023, 12, 31); // December 31, 2023
        var expected = new System.DateTime(2024, 1, 1, 0, 0, 0); // Expected: January 1, 2024, at midnight

        // Act
        System.DateTime result = dateTime.ToStartOfNextMonth();

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void ToStartOfPreviousMonth_Should_HandleJanuaryToDecemberTransition()
    {
        // Arrange
        var dateTime = new System.DateTime(2023, 1, 15); // January 15, 2023
        var expected = new System.DateTime(2022, 12, 1, 0, 0, 0); // Expected: December 1, 2022, at midnight

        // Act
        System.DateTime result = dateTime.ToStartOfPreviousMonth();

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void ToStartOfTzMonth_Should_HandleDstSpringForward()
    {
        // Arrange
        var dateTime = new System.DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc); // DST starts in March for many regions

        System.DateTime expected = new System.DateTime(2023, 3, 1, 0, 0,0, 0, 0).ToUtc(Tz.Eastern);

        // Act
        System.DateTime result = dateTime.ToStartOfTzMonth(Tz.Eastern);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void ToEndOfTzMonth_Should_HandleDstFallBack()
    {
        // Arrange
        var dateTime = new System.DateTime(2023, 11, 2, 0, 0, 0, DateTimeKind.Utc); // DST ends in November for many regions

        System.DateTime expected = new System.DateTime(2023, 11, 30, 23, 59, 59, 999).ToUtc(Tz.Eastern);

        // Act
        System.DateTime result = dateTime.ToEndOfTzMonth(Tz.Eastern);

        // Assert
        result.Should().BeCloseTo(expected, precision: TimeSpan.FromMilliseconds(1));
    }

    [Fact]
    public void ToStartOfNextTzMonth_Should_HandleDstSpringForward()
    {
        // Arrange
        var dateTime = new System.DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc); // DST starts in March for many regions

        System.DateTime expected = new System.DateTime(2023, 4, 1, 0, 0, 0, 0, 0).ToUtc(Tz.Eastern);

        // Act
        System.DateTime result = dateTime.ToStartOfNextTzMonth(Tz.Eastern);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void ToStartOfPreviousTzMonth_Should_HandleDstSpringForward()
    {
        // Arrange
        var dateTime = new System.DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Utc); // DST starts in March for many regions

        System.DateTime expected = new System.DateTime(2023, 3, 1, 0, 0, 0, 0, 0).ToUtc(Tz.Eastern);

        // Act
        System.DateTime result = dateTime.ToStartOfPreviousTzMonth(Tz.Eastern);

        // Assert
        result.Should().Be(expected);
    }
}