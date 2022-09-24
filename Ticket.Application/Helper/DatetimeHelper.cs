using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Application.Helper
{
    public static class DatetimeHelper
    {
        public static double CalculateDifferenceInMinutes(DateTime start, DateTime end)
        {
            TimeSpan ts = end - start;
            return ts.TotalMinutes;
        }
    }
}
