using StaffManagement.Models.Dto;
using StaffManagement.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StaffManagement.Models
{
    public class TaskProviderModel : ITaskProviderModel
    {
        public void AddTask(TaskData data)
        {
            using (var context = new StaffManagementEntities())
            {
                context.Tasks.Add(new Task
                {
                    UserId = data.UserId,
                    TaskName = data.TaskName,
                    TaskDate = DateTime.Parse(data.TaskDate),
                    NumberOfHours = data.NumberOfHours,
                    StartTime = TimeSpan.Parse(data.StartTime),
                    EndTime = TimeSpan.Parse(data.EndTime),
                    Comments = data.Comments
                });

                context.SaveChanges();
            }
        }

        public bool IsTasksAlreadyAssigned(int userId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            using (var context = new StaffManagementEntities())
            {
                var result = context.Tasks.Where(x => x.UserId == userId && x.TaskDate == date && ((startTime > x.StartTime && startTime < x.EndTime) || (endTime > x.StartTime && endTime < x.EndTime)));

                if (result.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<TasksResultData> GetUserTasksForTheWeek(int id)
        {
            DateTime week = DateTime.Today.AddDays(-7);

            using (var context = new StaffManagementEntities())
            {
                var data = from task in context.Tasks
                           join user in context.Users
                           on task.UserId equals user.UserId
                           where task.UserId == id && task.TaskDate >= week && task.TaskDate <= DateTime.Today
                           select new TasksResultData()
                           {
                               UserName = user.UserName,
                               TaskName = task.TaskName,
                               TaskDate = task.TaskDate.ToString(),
                               NumberOfHours = task.NumberOfHours,
                               StartTime = task.StartTime.ToString(),
                               EndTime = task.EndTime.ToString(),
                               Comments = task.Comments
                           };

                if (data != null && data.ToList().Count() > 0)
                {
                    return data.ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        public List<TasksResultData> GetAllUserTasksForTheWeek()
        {
            DateTime week = DateTime.Today.AddDays(-7);

            using (var context = new StaffManagementEntities())
            {
                var data = from task in context.Tasks
                           join user in context.Users
                           on task.UserId equals user.UserId
                           where task.TaskDate >= week && task.TaskDate <= DateTime.Today
                               select new TasksResultData()
                               {
                                   UserName = user.UserName,
                                   TaskName = task.TaskName,
                                   TaskDate = task.TaskDate.ToString(),
                                   NumberOfHours = task.NumberOfHours,
                                   StartTime = task.StartTime.ToString(),
                                   EndTime = task.EndTime.ToString(),
                                   Comments = task.Comments
                               };

                if (data != null && data.ToList().Count() > 0)
                {
                    return data.ToList();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}