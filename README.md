[![](https://img.shields.io/nuget/v/soenneker.extensions.datetime.month.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.datetime.month/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.datetime.month/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.datetime.month/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.datetime.month.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.datetime.month/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.datetime.month/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.datetime.month/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.DateTime.Month

A collection of helpful DateTime month-based extension methods.

## Installation

```bash
dotnet add package Soenneker.Extensions.DateTime.Month
```

## Quick start

```csharp
using Soenneker.Extensions.DateTime.Month;

DateTime dateTime = DateTime.UtcNow;
var result = dateTime.ToStartOfMonth();
```

## Common operations

- `ToStartOfMonth()` - Adjusts the specified DateTime to the start of its current month. Returns a new DateTime instance set to the first day of the month of the original DateTime, at 00:00 hours. Timezone information of the input DateTime is not modified or considered in the adjustment.
- `ToEndOfMonth()` - Converts the specified date and time to the end of its month. Returns a `System.DateTime` object representing the last moment of the specified month.
- `ToStartOfNextMonth()` - Adjusts the specified DateTime to the start of the next month. Returns a new DateTime instance set to the first day of the following month of the original DateTime, at 00:00 hours. Timezone information of the input DateTime is not modified or considered in the adjustment.
- `ToStartOfPreviousMonth()` - Converts the specified date and time to the start of the previous month. Returns a `System.DateTime` object representing the first moment of the previous month.
- `ToEndOfPreviousMonth()` - Converts the specified date and time to the end of the previous month. Returns a `System.DateTime` object representing the last moment of the previous month.
- `ToEndOfNextMonth()` - Converts the specified date and time to the end of the next month. Returns a `System.DateTime` object representing the last moment of the next month.
- `ToStartOfTzMonth()` - Converts the specified UTC DateTime to a specific timezone, then adjusts it to the start of the current month in that timezone, and converts it back to UTC. Returns a new DateTime instance in UTC, representing the start of the current month in the specified timezone.
- `ToEndOfTzMonth()` - Adjusts the specified UTC DateTime to the very last moment of the current month according to a specific timezone, and then converts it back to UTC. Returns a new DateTime instance in UTC, representing the last moment of the current month in the specified timezone.
- `ToEndOfPreviousTzMonth()` - Adjusts the specified UTC DateTime to the very last moment of the previous month according to a specific timezone, and then converts it back to UTC. Returns a new DateTime instance in UTC, representing the last moment of the previous month in the specified timezone.
- `ToStartOfPreviousTzMonth()` - Converts the specified UTC date and time to the start of the previous month according to the specified time zone. Returns a `System.DateTime` object representing the first moment of the previous month in the specified time zone.
- `ToStartOfNextTzMonth()` - Converts the specified UTC DateTime to a specific timezone, then adjusts it to the start of the next month in that timezone, and converts it back to UTC. Returns a new DateTime instance in UTC, representing the start of the next month in the specified timezone. This method is designed to handle time zone conversions explicitly.
- `ToEndOfNextTzMonth()` - Converts the specified UTC date and time to the end of the next month according to the specified time zone. Returns a `System.DateTime` object representing the last moment of the next month in the specified time zone. This method first calculates the end of the current month for the given UTC date and time in the specified time zone, then advances to the last moment of the next month.
