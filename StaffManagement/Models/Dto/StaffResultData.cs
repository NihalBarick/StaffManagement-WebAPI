using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StaffManagement.Models.Dto
{
    public class StaffResultData
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public System.TimeSpan DailyCheckInTime { get; set; }

        public System.TimeSpan DailyCheckOutTime { get; set; }
    }
}