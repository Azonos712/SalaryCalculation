using System.Globalization;

namespace SalaryCalculation.Library
{
    public static class Utility
    {
        public static string FirstCharToUpper(string s) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s);
    }
}
