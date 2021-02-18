using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DecentralizedFinance.Common.ExtensionMethods
{
    public static class DateTimeExtensionMethods
    {
        public static string ToShamsi(this DateTime miladiDate)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(miladiDate).ToString() + "/" +
                pc.GetMonth(miladiDate).ToString("00") + "/" +
                pc.GetDayOfMonth(miladiDate).ToString("00");
        }
    }
}
