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

		public static string ToAppStringWithTime(this DateTime date)
		{
			return date.ToString("dd\\/MM\\/yyyy HH:mm");
		}

        public static DateTime ParseAppString(this string date)
        {
            string[] parts = date.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 2)
            {
                string[] dParts = parts[0].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                string[] tParts = parts[1].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                int day = int.Parse(dParts[0]);
                int month = int.Parse(dParts[1]);
                int year = int.Parse(dParts[2]);
                int hour = int.Parse(tParts[0]);
                int minute = int.Parse(tParts[1]);

                return new DateTime(year, month, day, hour, minute, 0);
            }
            else
            {
                string[] dParts = parts[0].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                int day = int.Parse(dParts[0]);
                int month = int.Parse(dParts[1]);
                int year = int.Parse(dParts[2]);

                return new DateTime(year, month, day);
            }
        }
    }
}
