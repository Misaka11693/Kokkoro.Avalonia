using System.ComponentModel;

namespace Kokkoro.Extensions;

public static class EnumExtensions
{
    public static string GetLabel<TEnum>(this TEnum value)
        where TEnum : struct, Enum
    {
        var member = typeof(TEnum).GetMember(value.ToString()).FirstOrDefault();
        var description = member?
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .OfType<DescriptionAttribute>()
            .FirstOrDefault();

        return description?.Description ?? value.ToString();
    }
}
