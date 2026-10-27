using System.Globalization;

namespace Api.Modules.Shared.Application.Helpers;

public static class DateHelper
{
    public static bool IsValidStringDate(string? birthDate)
    {
        if (string.IsNullOrWhiteSpace(birthDate)) 
            return true;

        return DateTime.TryParseExact(
            birthDate,
            ["yyyy-MM-dd"],
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out _
        );
    }
}

