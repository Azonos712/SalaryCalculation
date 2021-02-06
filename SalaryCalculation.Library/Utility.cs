using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SalaryCalculation.Library
{
    public static class Utility
    {
        public static string FirstCharToUpper(string s) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s);
    }
}
