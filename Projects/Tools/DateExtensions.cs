using System;
using System.Text;

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

        public static DateTime ParseAppString(this string date)
        {
            string[] parts = date.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            int day = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int year = int.Parse(parts[2]);

            return new DateTime(year, month, day);
        }
    }
}
