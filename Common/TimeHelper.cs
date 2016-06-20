using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class TimeHelper
    {
        public static string TimeAgo(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("khoảng {0} năm trước", years);
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("khoảng {0} tháng trước", months);
            }
            if (span.Days > 0)
                return String.Format("khoảng {0} ngày trước", span.Days);
            if (span.Hours > 0)
                return String.Format("khoảng {0} tiếng trước", span.Hours);
            if (span.Minutes > 0)
                return String.Format("khoảng {0} phút trước", span.Minutes);
            if (span.Seconds > 5)
                return String.Format("khoảng {0} giây trước", span.Seconds);
            if (span.Seconds <= 5)
                return "vừa xong";
            return string.Empty;
        }
    }
}
