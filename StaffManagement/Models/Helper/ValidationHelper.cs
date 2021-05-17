using StaffManagement.Models.Interfaces;
using System;
using System.Globalization;

namespace StaffManagement.Models.Helper
{
    public class ValidationHelper : IValidationHelper
    {
        string[] formats = {"M/d/yyyy", "M/dd/yyyy", "MM/d/yyyy",
                   "MM/dd/yyyy", "yyyy-MM-dd", "yyyy-MM-d"};

        public bool IsValidTime(string time)
        {
            TimeSpan output;
            return TimeSpan.TryParseExact(time, "hh\\:mm", CultureInfo.CurrentCulture, out output);
        }

        public bool IsValidDate(string date)
        {
            DateTime dateValue;
            return DateTime.TryParseExact(date, formats,
                              CultureInfo.CurrentCulture,
                              DateTimeStyles.None,
                              out dateValue);
        }

        public bool IsValidWorkingHours(string startTime, string endTime)
        {
            TimeSpan start = TimeSpan.Parse(startTime);
            TimeSpan end = TimeSpan.Parse(endTime);

            var difference = end - start;

            if ( difference > new TimeSpan(8, 0, 0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}