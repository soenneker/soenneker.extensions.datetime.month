[![](https://img.shields.io/nuget/v/soenneker.extensions.datetime.month.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.datetime.month/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.datetime.month/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.datetime.month/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.datetime.month.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.datetime.month/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.datetime.month/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.datetime.month/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.DateTime.Month

Computes current, previous, and next calendar-month boundaries for `DateTime`, with optional time-zone-aware UTC results.

## Installation

```bash
dotnet add package Soenneker.Extensions.DateTime.Month
```

## Calendar-field boundaries

```csharp
using Soenneker.Extensions.DateTime.Month;

System.DateTime value = new(2026, 8, 29, 16, 42, 30, DateTimeKind.Utc);

System.DateTime start = value.ToStartOfMonth();
System.DateTime end = value.ToEndOfMonth();
System.DateTime previousStart = value.ToStartOfPreviousMonth();
System.DateTime nextEnd = value.ToEndOfNextMonth();
```

| Method | Result |
| --- | --- |
| `ToStartOfMonth()` | First tick of the current month |
| `ToEndOfMonth()` | Last tick before the next month |
| `ToStartOfPreviousMonth()` | First tick of the previous month |
| `ToEndOfPreviousMonth()` | Last tick before the current month |
| `ToStartOfNextMonth()` | First tick of the next month |
| `ToEndOfNextMonth()` | Last tick before the month after next |

These methods operate on the existing calendar fields, handle varying month lengths and leap years through `DateTime` calendar arithmetic, and preserve the input `Kind`. They do not perform time-zone conversion.

## Time-zone-aware boundaries

```csharp
TimeZoneInfo eastern = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
System.DateTime utc = new(2026, 8, 29, 18, 0, 0, DateTimeKind.Utc);

System.DateTime localMonthStartUtc = utc.ToStartOfTzMonth(eastern);
System.DateTime localMonthEndUtc = utc.ToEndOfTzMonth(eastern);
```

The time-zone variants select the current, previous, or next month from the input instant's local calendar and return its boundary as a UTC `DateTime`:

- `ToStartOfTzMonth()` / `ToEndOfTzMonth()`
- `ToStartOfPreviousTzMonth()` / `ToEndOfPreviousTzMonth()`
- `ToStartOfNextTzMonth()` / `ToEndOfNextTzMonth()`

If the input `Kind` is not `Utc`, its fields are treated as UTC rather than converted from the machine's local zone. Supply an actual UTC value to avoid ambiguity.

Month ends are defined as one tick before the following valid local month boundary. If a local month begins in a daylight-saving gap, the boundary advances to the first valid local minute; if it is ambiguous, the earlier UTC instant is selected.
