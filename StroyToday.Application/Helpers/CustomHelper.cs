using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using StroyToday.Application.Enums;

namespace StroyToday.Application.Helpers
{
    public static class CustomHelper
    {
        public static string DisplayTimeOnly(this DateTime dateTime, DateTimeDisplayType? view = DateTimeDisplayType.MmDdYyyy)
        {
            switch (view)
            {
                case DateTimeDisplayType.Friendly:
                case DateTimeDisplayType.FriendlyShort:
                case DateTimeDisplayType.FriendlyShortNoTime:
                case DateTimeDisplayType.FriendlyWithDay:
                    return dateTime.ToString("h:mm tt", CultureInfo.InvariantCulture); // 7:11 PM

                case DateTimeDisplayType.MmDdYyyy:
                case DateTimeDisplayType.MD:
                    return dateTime.ToString("HH:mm", CultureInfo.InvariantCulture); // 19:11

                case DateTimeDisplayType.MmDdYyyShort:
                case DateTimeDisplayType.MmDd:
                case DateTimeDisplayType.DdMm:
                    return dateTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture); // 19:11:42

                case DateTimeDisplayType.DayDate:
                    return dateTime.ToString("h:mm:ss tt", CultureInfo.InvariantCulture); // 7:11:42 PM

                default:
                    return dateTime.ToString("HH:mm", CultureInfo.InvariantCulture); // Default to 24-hour time without seconds
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
    }
}
