using System.Globalization;

namespace Api.Modules.Shared.Application.Helpers;

public static class DateHelper
{
    public static bool IsValidStringDate(string? birthDate)
    {
        if (string.IsNullOrWhiteSpace(birthDate)) 
            return false;

        return DateTime.TryParseExact(
            birthDate,
            ["yyyy-MM-dd"],
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out _
        );
    }
}


public static class TimeHelper
{
    public static bool IsValidStringTime(string? time)
    {
        if (string.IsNullOrWhiteSpace(time)) 
            return false;

        return TimeOnly.TryParseExact(
            time,
            ["HH:mm:ss"],
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out _
        );
    }
}