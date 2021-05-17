using StaffManagement.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagement.Models.Interfaces
{
    public interface ITaskProviderModel
    {
        void AddTask(TaskData data);

        bool IsTasksAlreadyAssigned(int userId, DateTime date, TimeSpan startTime, TimeSpan endTime);

        List<TasksResultData> GetUserTasksForTheWeek(int id);

        List<TasksResultData> GetAllUserTasksForTheWeek();
    }
}
