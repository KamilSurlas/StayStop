using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.DateTimeExtension
{
    public static class DateTimeExtensions
    {
        public static int NumberOfNights(this DateTime date1, DateTime date2)
        {
            var frm = date1 < date2 ? date1 : date2;
            var to = date1 < date2 ? date2 : date1;
            var totalDays = (int)(to - frm).TotalDays;
            return totalDays > 0 ? totalDays : 1;
        }
    }
}
