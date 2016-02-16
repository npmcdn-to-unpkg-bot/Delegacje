using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public static class DateExtensions
    {
        public static string ToAppString(this DateTime date)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(date.Day.ToString("00"));
            sb.Append("/");
            sb.Append(date.Month.ToString("00"));
            sb.Append("/");
            sb.Append(date.Year.ToString());
            return sb.ToString();
        }
    }
}
