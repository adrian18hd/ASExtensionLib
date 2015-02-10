using System;
using System.Globalization;

namespace ASExtensionLib
{
    public static class DateTimeExtensions
    {
	    private const string EstTimezoneID = "US Eastern Standard Time";
	    private const string CstTimezoneID = "Central Standard Time";

	    public static DateTime ToOffsetDateTime(this DateTime date, double offset)
		{
            if (date.TimeOfDay.TotalSeconds > 0)
			    return date.AddHours(offset);
            return date;
		}

		public static DateTime ToCentralStandardTime(this DateTime date)
		{
			TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(CstTimezoneID);
			return TimeZoneInfo.ConvertTime(date, cstZone);
		}

        public static DateTime ToEasternStandardTime(this DateTime date)
        {
            DateTime utcDate = date.ToLocalTime().ToUniversalTime();

            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById(EstTimezoneID);
            return TimeZoneInfo.ConvertTimeFromUtc(new DateTime(utcDate.Ticks, DateTimeKind.Unspecified), estZone);
        }

        public static int WeekOfYear(this DateTime date, DayOfWeek firstDayOfWeek)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, firstDayOfWeek);
            return weekNum;
        }

        public static int WeekOfYear(this DateTime date)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
            return weekNum;
        }
    }
}