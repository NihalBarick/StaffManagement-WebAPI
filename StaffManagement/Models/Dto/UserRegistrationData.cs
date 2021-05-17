using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StaffManagement.Models.Dto
{
    public class UserRegistrationData
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }
    }
}