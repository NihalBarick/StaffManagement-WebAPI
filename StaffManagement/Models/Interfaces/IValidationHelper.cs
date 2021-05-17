using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagement.Models.Interfaces
{
    interface IValidationHelper
    {
        bool IsValidTime(string time);

        bool IsValidDate(string date);

        bool IsValidWorkingHours(string startTime, string endTime);
    }
}
