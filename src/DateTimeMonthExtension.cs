using System.Diagnostics.Contracts;
using Soenneker.Enums.UnitOfTime;

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
        System.DateTime result = dateTime.Trim(UnitOfTime.Month);
        return result;
    }

    /// <summary>
    /// Converts the specified date and time to the end of its month.
    /// </summary>
    /// <remarks>
    /// This method calculates the last moment of the month for the given date and time. 
    /// It is marked with the [Pure] attribute, indicating that the method does not modify any object state and that its return value depends solely on its input parameters.
    /// The method relies on a custom logic defined by `TrimEnd(DateTimePrecision.Month)`, which is assumed to adjust the date and time to the end of the current month.
    /// </remarks>
    /// <param name="dateTime">The date and time to convert.</param>
    /// <returns>A <see cref="System.DateTime"/> object representing the last moment of the specified month.</returns>
    [Pure]
    public static System.DateTime ToEndOfMonth(this System.DateTime dateTime)
    {
        System.DateTime result = dateTime.TrimEnd(UnitOfTime.Month);
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
        System.DateTime result = dateTime.ToStartOfMonth().AddMonths(1);
        return result;
    }

    /// <summary>
    /// Converts the specified date and time to the start of the previous month.
    /// </summary>
    /// <remarks>
    /// This method calculates the start of the month for the given date and time, then moves back to the first moment of the previous month.
    /// It is marked with the [Pure] attribute, indicating that the method does not modify any object state and that its return value depends solely on its input parameters.
    /// </remarks>
    /// <param name="dateTime">The date and time to convert.</param>
    /// <returns>A <see cref="System.DateTime"/> object representing the first moment of the previous month.</returns>
    /// <example>
    /// <code>
    /// DateTime now = DateTime.Now;
    /// DateTime startOfPreviousMonth = now.ToStartOfPreviousMonth();
    /// Console.WriteLine(startOfPreviousMonth);
    /// </code>
    /// This example shows how to get the start of the previous month from the current system date and time.
    /// </example>
    [Pure]
    public static System.DateTime ToStartOfPreviousMonth(this System.DateTime dateTime)
    {
        System.DateTime result = dateTime.ToStartOfMonth().AddMonths(-1);
        return result;
    }

    /// <summary>
    /// Converts the specified date and time to the end of the previous month.
    /// </summary>
    /// <remarks>
    /// This method first calculates the end of the month for the given date and time, then moves back to the last moment of the previous month. 
    /// It is marked with the [Pure] attribute, indicating that the method does not modify any object state and that its return value depends solely on its input parameters.
    /// </remarks>
    /// <param name="dateTime">The date and time to convert.</param>
    /// <returns>A <see cref="System.DateTime"/> object representing the last moment of the previous month.</returns>
    [Pure]
    public static System.DateTime ToEndOfPreviousMonth(this System.DateTime dateTime)
    {
        System.DateTime result = dateTime.ToEndOfMonth().AddMonths(-1);
        return result;
    }

    /// <summary>
    /// Converts the specified date and time to the end of the next month.
    /// </summary>
    /// <remarks>
    /// This method calculates the end of the month for the given date and time, then advances to the last moment of the following month.
    /// It utilizes the <see cref="ToEndOfMonth"/> method to first determine the end of the current month before adding a month to reach the end of the next month.
    /// It is marked with the [Pure] attribute, indicating that the method does not modify any object state and that its return value depends solely on its input parameters.
    /// </remarks>
    /// <param name="dateTime">The date and time to convert.</param>
    /// <returns>A <see cref="System.DateTime"/> object representing the last moment of the next month.</returns>
    [Pure]
    public static System.DateTime ToEndOfNextMonth(this System.DateTime dateTime)
    {
        System.DateTime result = dateTime.ToEndOfMonth().AddMonths(1);
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
    public static System.DateTime ToStartOfTzMonth(this System.DateTime utcNow, System.TimeZoneInfo tzInfo)
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
    public static System.DateTime ToEndOfTzMonth(this System.DateTime utcNow, System.TimeZoneInfo tzInfo)
    {
        System.DateTime result = utcNow.ToTz(tzInfo).ToEndOfMonth().ToUtc(tzInfo);
        return result;
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
        System.DateTime result = utcNow.ToTz(tzInfo).ToEndOfPreviousMonth().ToUtc(tzInfo);
        return result;
    }

    /// <summary>
    /// Converts the specified UTC date and time to the start of the previous month according to the specified time zone.
    /// </summary>
    /// <remarks>
    /// This method calculates the start of the current month for the given UTC date and time in the specified time zone, then moves back to the first moment of the previous month.
    /// It is marked with the [Pure] attribute, indicating that the method does not modify any object state and that its return value depends solely on its input parameters.
    /// </remarks>
    /// <param name="utcNow">The UTC date and time to convert.</param>
    /// <param name="tzInfo">The time zone to consider for the conversion.</param>
    /// <returns>A <see cref="System.DateTime"/> object representing the first moment of the previous month in the specified time zone.</returns>
    [Pure]
    public static System.DateTime ToStartOfPreviousTzMonth(this System.DateTime utcNow, System.TimeZoneInfo tzInfo)
    {
        System.DateTime result = utcNow.ToTz(tzInfo).ToStartOfPreviousMonth().ToUtc(tzInfo);
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
    /// Converts the specified UTC date and time to the end of the next month according to the specified time zone.
    /// </summary>
    /// <remarks>
    /// This method first calculates the end of the current month for the given UTC date and time in the specified time zone, then advances to the last moment of the next month. 
    /// It is marked with the [Pure] attribute, indicating that the method does not modify any object state and that its return value depends only on its input parameters.
    /// </remarks>
    /// <param name="utcNow">The UTC date and time to convert.</param>
    /// <param name="tzInfo">The time zone to consider for the conversion.</param>
    /// <returns>A <see cref="System.DateTime"/> object representing the last moment of the next month in the specified time zone.</returns>
    [Pure]
    public static System.DateTime ToEndOfNextTzMonth(this System.DateTime utcNow, System.TimeZoneInfo tzInfo)
    {
        System.DateTime result = utcNow.ToTz(tzInfo).ToEndOfNextMonth().ToUtc(tzInfo);
        return result;
    }


}
