using System;

namespace NetTools.Common;

public static class Time
{
    private const string DefaultDateStringFormat = "yyyy-MM-dd'T'HH:mm:ssK";

    public static DateTime LocalNow => DateTime.Now;

    public static string LocalNowString => LocalNow.ToString(DefaultDateStringFormat);

    public static TimeZoneInfo LocalTimeZone => TimeZoneInfo.Local;

    public static string LocalTimeZoneString => TimeZoneInfo.Local.DisplayName;

    public static DateTime UtcNow => Local.Datetime.ToUtcDatetime(LocalNow);

    public static string UtcNowString => UtcNow.ToString(DefaultDateStringFormat);

    public static TimeSpan Between(DateTime startTime, DateTime endTime)
    {
        return endTime - startTime;
    }

    public static string DateTimeToHumanFormatString(DateTime time, string format = "yyyy-MM-dd HH:mm:ss")
    {
        return DateTimeToString(time, format);
    }

    public static bool Lapsed(DateTime startTime, int expectedDurationInMinutes)
    {
        if (!Validation.Exists(startTime)) return false;

        // TODO Account for timezone?
        var ts = Between(startTime, DateTime.Now);

        return ts.TotalMinutes >= expectedDurationInMinutes;
    }

    public static DateTime Shift(DateTime startTime, int days = 0, int hours = 0, int minutes = 0,
        int seconds = 0)
    {
        var shift = new TimeSpan(days, hours, minutes, seconds);
        return startTime.Add(shift);
    }

    public static TimeSpan Since(DateTime time)
    {
        return Between(time, DateTime.Now);
    }

    public static string TimeSpanToHumanFormatString(TimeSpan timeSpan, bool includeSeconds = false,
        bool forceIncludeDays = false)
    {
        var timeString = $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}";

        if (timeSpan.Days > 0 || forceIncludeDays) timeString = $"{timeSpan.Days} day(s), {timeString}";

        if (includeSeconds) timeString = $"{timeString}:{timeSpan.Seconds:D2}";

        return timeString;
    }

    public static TimeSpan Until(DateTime time)
    {
        return Between(DateTime.Now, time);
    }

    private static string DateTimeToString(DateTime time, string? format = null)
    {
        format ??= DefaultDateStringFormat;

        return time.ToString(format);
    }

    private static DateTime StringToDateTime(string timeString)
    {
        return DateTime.Parse(timeString);
    }

    public static class MoreThan
    {
        public static class Seconds
        {
            public static bool Since(DateTime time, int length)
            {
                var timeSpan = Time.Since(time);
                return timeSpan.Seconds > length;
            }

            public static bool Until(DateTime time, int length)
            {
                var timeSpan = Time.Until(time);
                return timeSpan.Seconds > length;
            }
        }

        public static class Minutes
        {
            public static bool Since(DateTime time, int length)
            {
                var timeSpan = Time.Since(time);
                return timeSpan.Minutes > length;
            }

            public static bool Until(DateTime time, int length)
            {
                var timeSpan = Time.Until(time);
                return timeSpan.Minutes > length;
            }
        }

        public static class Hours
        {
            public static bool Since(DateTime time, int length)
            {
                var timeSpan = Time.Since(time);
                return timeSpan.Hours > length;
            }

            public static bool Until(DateTime time, int length)
            {
                var timeSpan = Time.Until(time);
                return timeSpan.Hours > length;
            }
        }

        public static class Days
        {
            public static bool Since(DateTime time, int length)
            {
                var timeSpan = Time.Since(time);
                return timeSpan.Days > length;
            }

            public static bool Until(DateTime time, int length)
            {
                var timeSpan = Time.Until(time);
                return timeSpan.Days > length;
            }
        }
    }

    public static class LessThan
    {
        public static class Seconds
        {
            public static bool Since(DateTime time, int length)
            {
                return !MoreThan.Seconds.Since(time, length);
            }

            public static bool Until(DateTime time, int length)
            {
                return !MoreThan.Seconds.Until(time, length);
            }
        }

        public static class Minutes
        {
            public static bool Since(DateTime time, int length)
            {
                return !MoreThan.Minutes.Since(time, length);
            }

            public static bool Until(DateTime time, int length)
            {
                return !MoreThan.Minutes.Until(time, length);
            }
        }

        public static class Hours
        {
            public static bool Since(DateTime time, int length)
            {
                return !MoreThan.Hours.Since(time, length);
            }

            public static bool Until(DateTime time, int length)
            {
                return !MoreThan.Hours.Until(time, length);
            }
        }

        public static class Days
        {
            public static bool Since(DateTime time, int length)
            {
                return !MoreThan.Days.Since(time, length);
            }

            public static bool Until(DateTime time, int length)
            {
                return !MoreThan.Days.Until(time, length);
            }
        }
    }

    public static class Utc
    {
        public static class Datetime
        {
            public static DateTime ToLocalDatetime(DateTime utcTime)
            {
                return utcTime.ToLocalTime();
            }

            public static string ToLocalString(DateTime utcTime)
            {
                var localTime = ToLocalDatetime(utcTime);
                return Local.Datetime.ToString(localTime);
            }

            public static string ToString(DateTime utcTime)
            {
                return DateTimeToString(utcTime);
            }
        }

        public static class String
        {
            public static DateTime ToDatetime(string utcTimeString)
            {
                return StringToDateTime(utcTimeString);
            }

            public static DateTime ToLocalDatetime(string utcTimeString)
            {
                var localTimeString = ToLocalString(utcTimeString);
                return Local.String.ToDatetime(localTimeString);
            }

            public static string ToLocalString(string utcTimeString)
            {
                var utcTime = ToDatetime(utcTimeString);
                return Datetime.ToLocalString(utcTime);
            }
        }
    }

    public static class Local
    {
        public static class Datetime
        {
            public static string ToString(DateTime localTime)
            {
                return DateTimeToString(localTime);
            }

            public static DateTime ToUtcDatetime(DateTime localTime)
            {
                return localTime.ToUniversalTime();
            }

            public static string ToUtcString(DateTime localTime)
            {
                var utcTime = ToUtcDatetime(localTime);
                return Utc.Datetime.ToString(utcTime);
            }
        }

        public static class String
        {
            public static DateTime ToDatetime(string localTimeString)
            {
                return StringToDateTime(localTimeString);
            }

            public static DateTime ToUtcDatetime(string localTimeString)
            {
                var localTime = ToDatetime(localTimeString);
                return Datetime.ToUtcDatetime(localTime);
            }

            public static string ToUtcString(string localTimeString)
            {
                var localTime = ToDatetime(localTimeString);
                return Datetime.ToUtcString(localTime);
            }
        }
    }

    public class Format
    {
        public static string HoursAndMinutes(int hours = 0, int minutes = 0)
        {
            if (hours > 0)
            {
                if (minutes > 0) return $"{hours} {Text.Suffix("Hour", hours > 1 ? "s" : "")}, {minutes} {Text.Suffix("Minute", minutes > 1 ? "s" : "")}";

                return $"{hours} {Text.Suffix("Hour", hours > 1 ? "s" : "")}";
            }

            return $"{minutes} {Text.Suffix("Minute", minutes > 1 ? "s" : "")}";
        }
    }
}
