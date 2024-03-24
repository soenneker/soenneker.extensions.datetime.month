using System.Diagnostics.Contracts;
using Soenneker.Enums.DateTimePrecision;

namespace Soenneker.Extensions.DateTime.Month;

/// <summary>
/// Provides a set of extension methods for the <see cref="System.DateTime"/> class to facilitate month-based operations.
/// </summary>
public static class DateTimeMonthExtension
{
    /// <summary>
    /// Adjusts the specified DateTime to the start of its current month.
    /// </summary>
    /// <param name="dateTime">The DateTime instance to be adjusted.</param>
    /// <returns>A new DateTime instance set to the first day of the month of the original DateTime, at 00:00 hours.</returns>
    /// <remarks>
    /// Timezone information of the input DateTime is not modified or considered in the adjustment. The returned DateTime maintains the original DateTimeKind.
    /// </remarks>
    [Pure]
    public static System.DateTime ToStartOfMonth(this System.DateTime dateTime)
    {
        System.DateTime result = dateTime.Trim(DateTimePrecision.Month);
        return result;
    }

    /// <summary>
    /// Adjusts the specified DateTime to the start of the next month.
    /// </summary>
    /// <param name="dateTime">The DateTime instance to be adjusted.</param>
    /// <returns>A new DateTime instance set to the first day of the following month of the original DateTime, at 00:00 hours.</returns>
    /// <remarks>
    /// Timezone information of the input DateTime is not modified or considered in the adjustment. The returned DateTime maintains the original DateTimeKind.
    /// </remarks>
    [Pure]
    public static System.DateTime ToStartOfNextMonth(this System.DateTime dateTime)
    {
        System.DateTime result = dateTime.AddMonths(1).ToStartOfMonth();
        return result;
    }

    /// <summary>
    /// Converts the specified UTC DateTime to a specific timezone, then adjusts it to the start of the next month in that timezone, and converts it back to UTC.
    /// </summary>
    /// <param name="utcNow">The current UTC DateTime.</param>
    /// <param name="tzInfo">The target timezone information.</param>
    /// <returns>A new DateTime instance in UTC, representing the start of the next month in the specified timezone.</returns>
    /// <remarks>
    /// This method is designed to handle time zone conversions explicitly. It is particularly useful for scheduling tasks based on the local time of a specific timezone.
    /// </remarks>
    [Pure]
    public static System.DateTime ToStartOfNextTzMonth(this System.DateTime utcNow, System.TimeZoneInfo tzInfo)
    {
        System.DateTime result = utcNow.ToTz(tzInfo).ToStartOfNextMonth().ToUtc(tzInfo);
        return result;
    }

    /// <summary>
    /// Converts the specified UTC DateTime to a specific timezone, then adjusts it to the start of the current month in that timezone, and converts it back to UTC.
    /// </summary>
    /// <param name="utcNow">The current UTC DateTime.</param>
    /// <param name="tzInfo">The target timezone information.</param>
    /// <returns>A new DateTime instance in UTC, representing the start of the current month in the specified timezone.</returns>
    /// <remarks>
    /// This method is useful for aligning UTC dates with local times in specific timezones, particularly for the beginning of the month calculations.
    /// </remarks>
    [Pure]
    public static System.DateTime ToStartOfCurrentTzMonth(this System.DateTime utcNow, System.TimeZoneInfo tzInfo)
    {
        System.DateTime result = utcNow.ToTz(tzInfo).ToStartOfMonth().ToUtc(tzInfo);
        return result;
    }

    /// <summary>
    /// Adjusts the specified UTC DateTime to the very last moment of the current month according to a specific timezone, and then converts it back to UTC.
    /// </summary>
    /// <param name="utcNow">The current UTC DateTime.</param>
    /// <param name="tzInfo">The target timezone information.</param>
    /// <returns>A new DateTime instance in UTC, representing the last moment of the current month in the specified timezone.</returns>
    /// <remarks>
    /// This method calculates the end of the current month in the specified timezone by first determining the start of the next month and then subtracting one tick. It is particularly useful for tasks that require alignment with the end of the month in specific time zones.
    /// </remarks>
    [Pure]
    public static System.DateTime ToEndOfCurrentTzMonth(this System.DateTime utcNow, System.TimeZoneInfo tzInfo)
    {
        System.DateTime end = utcNow.ToStartOfNextTzMonth(tzInfo).AddTicks(-1);
        return end;
    }

    /// <summary>
    /// Adjusts the specified UTC DateTime to the very last moment of the previous month according to a specific timezone, and then converts it back to UTC.
    /// </summary>
    /// <param name="utcNow">The current UTC DateTime.</param>
    /// <param name="tzInfo">The target timezone information.</param>
    /// <returns>A new DateTime instance in UTC, representing the last moment of the previous month in the specified timezone.</returns>
    /// <remarks>
    /// This method calculates the end of the previous month in the specified timezone by determining the start of the current month and then subtracting one tick. Useful for aligning tasks with the end of the month in various time zones, especially for retrospective data processing.
    /// </remarks>
    [Pure]
    public static System.DateTime ToEndOfPreviousTzMonth(this System.DateTime utcNow, System.TimeZoneInfo tzInfo)
    {
        System.DateTime end = utcNow.ToStartOfCurrentTzMonth(tzInfo).AddTicks(-1);
        return end;
    }
}
