using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using StroyToday.Core.Enums;

namespace StroyToday.Core.Helpers
{
    public static class CustomHelper
    {
        public static string DisplayTimeOnly(this DateTime? dateTime, DateTimeDisplayType? view = DateTimeDisplayType.MmDdYyyy)
        {
            if (dateTime == null)
            {
                return string.Empty;
            }

            switch (view)
            {
                case DateTimeDisplayType.Friendly:
                case DateTimeDisplayType.FriendlyShort:
                case DateTimeDisplayType.FriendlyShortNoTime:
                case DateTimeDisplayType.FriendlyWithDay:
                    return dateTime.Value.ToString("h:mm tt", CultureInfo.InvariantCulture); // 7:11 PM

                case DateTimeDisplayType.MmDdYyyy:
                case DateTimeDisplayType.MD:
                    return dateTime.Value.ToString("HH:mm", CultureInfo.InvariantCulture); // 19:11

                case DateTimeDisplayType.MmDdYyyShort:
                case DateTimeDisplayType.MmDd:
                case DateTimeDisplayType.DdMm:
                    return dateTime.Value.ToString("HH:mm:ss", CultureInfo.InvariantCulture); // 19:11:42

                case DateTimeDisplayType.DayDate:
                    return dateTime.Value.ToString("h:mm:ss tt", CultureInfo.InvariantCulture); // 7:11:42 PM

                default:
                    return dateTime.Value.ToString("HH:mm", CultureInfo.InvariantCulture); // Default to 24-hour time without seconds
            }
        }

        public static string DisplayOnlyDate(this DateTime? dateTime, DateTimeDisplayType? view = DateTimeDisplayType.MmDdYyyy)
        {
            if (dateTime == null)
            {
                return string.Empty;
            }

            switch (view)
            {
                case DateTimeDisplayType.Friendly:
                    return dateTime.Value.ToString("ddd, MMM d", CultureInfo.InvariantCulture); // Mon, Nov 7

                case DateTimeDisplayType.MmDdYyyy:
                    return dateTime.Value.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture); // 11/07/2022

                case DateTimeDisplayType.MmDdYyyShort:
                    return dateTime.Value.ToString("MMM d", CultureInfo.InvariantCulture); // Nov 7

                case DateTimeDisplayType.FriendlyShort:
                    return dateTime.Value.ToString("ddd, MMM d", CultureInfo.InvariantCulture); // Mon, Nov 7

                case DateTimeDisplayType.MmDd:
                    return dateTime.Value.ToString("MM/dd", CultureInfo.InvariantCulture); // 11/07

                case DateTimeDisplayType.MD:
                    return dateTime.Value.ToString("M/d/yyyy", CultureInfo.InvariantCulture); // 11/7/2022

                case DateTimeDisplayType.FriendlyShortNoTime:
                    return dateTime.Value.ToString("ddd, MMM d", CultureInfo.InvariantCulture); // Mon, Nov 7

                case DateTimeDisplayType.FriendlyWithDay:
                    return dateTime.Value.ToString("ddd, M/d", CultureInfo.InvariantCulture); // Mon, 11/7

                case DateTimeDisplayType.DdMm:
                    return dateTime.Value.ToString("dd/MM", CultureInfo.InvariantCulture); // 07/11

                case DateTimeDisplayType.DayDate:
                    return dateTime.Value.ToString("dddd, MM/dd/yyyy", CultureInfo.InvariantCulture); // Monday, 11/07/2022

                default:
                    return dateTime.Value.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture); // Default to 11/07/2022
            }
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            try
            {
                if (enumValue == null)
                {
                    return string.Empty;
                }

                var val = enumValue.GetType()?
                    .GetMember(enumValue.ToString());
                if (val != null && val.Any())
                {
                    return val.First()?
                        .GetCustomAttribute<DisplayAttribute>()?
                        .GetName();
                }

                return string.Empty;

            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public static DateTime? ConvertToUserTimeZone(DateTime? dateTimeUtc, string timeZoneId)
        {
            if (dateTimeUtc == null || string.IsNullOrEmpty(timeZoneId))
            {
                return null;
            }

            try
            {
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                return TimeZoneInfo.ConvertTimeFromUtc(dateTimeUtc.Value, timeZone);
            }
            catch (TimeZoneNotFoundException)
            {
                // Логирование ошибки или обработка исключения
                return null;
            }
            catch (InvalidTimeZoneException)
            {
                // Логирование ошибки или обработка исключения
                return null;
            }
        }
    }
}
