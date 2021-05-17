using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StaffManagement.Models.Dto
{
    public class TaskData
    {
        public int UserId { get; set; }
        public string TaskName { get; set; }
        public string TaskDate { get; set; }
        public short NumberOfHours { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Comments { get; set; }
    }
}