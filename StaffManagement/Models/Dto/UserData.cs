using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StaffManagement.Models.Dto
{
    public class UserData
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public short RoleId { get; set; }

        public string DailyCheckInTime { get; set; }

        public string DailyCheckOutTime { get; set; }
    }
}