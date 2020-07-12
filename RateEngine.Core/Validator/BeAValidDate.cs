using System;
using System.Collections.Generic;
using System.Text;

namespace RateEngine.Core.Validator
{
    public static class DateValidator
    {
        public static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        public static bool BeAValidDates(DateTime startDate, DateTime endDate)
        {
            return (startDate < endDate);
        }
    }
}
