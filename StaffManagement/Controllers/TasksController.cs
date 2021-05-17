using StaffManagement.Models;
using StaffManagement.Models.Dto;
using StaffManagement.Models.Helper;
using StaffManagement.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StaffManagement.Controllers
{
    public class TasksController : ApiController
    {
        private readonly ITaskProviderModel taskProviderModel = null;

        private readonly IValidationHelper validationHelper = null;

        public TasksController()
        {
            this.taskProviderModel = new TaskProviderModel();
            this.validationHelper = new ValidationHelper();
        }

        public TasksController(ITaskProviderModel taskProviderModel, IValidationHelper validationHelper)
        {
            this.taskProviderModel = taskProviderModel;
            this.validationHelper = validationHelper;
        }

        [HttpGet]
        [Route("api/Tasks")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                if (id == 0)
                {
                    return Ok(this.taskProviderModel.GetAllUserTasksForTheWeek());
                }
                else
                {
                    return Ok(this.taskProviderModel.GetUserTasksForTheWeek(id));
                }                
            }
            catch
            {
                return BadRequest();
            }         
        }

        [HttpPost]
        [Route("api/Tasks/Add")]
        public IHttpActionResult Post([FromBody] TaskData data)
        {
            if (data == null)
            {
                return BadRequest();
            }
            else if (data.UserId == 0 || data.TaskName == "" || data.NumberOfHours <= 0 || data.NumberOfHours >= 24)
            {
                return BadRequest();
            }
            else if (this.validationHelper.IsValidTime(data.StartTime) == false || this.validationHelper.IsValidTime(data.EndTime) == false || this.validationHelper.IsValidDate(data.TaskDate) == false)
            {
                return BadRequest("Time format is not proper");
            }
            else if (this.taskProviderModel.IsTasksAlreadyAssigned(data.UserId, DateTime.Parse(data.TaskDate), TimeSpan.Parse(data.StartTime), TimeSpan.Parse(data.EndTime)))
            {
                return BadRequest("Task already exists for the user");
            }
            else
            {
                try
                {
                    this.taskProviderModel.AddTask(data);
                    return Ok();
                }
                catch
                {
                    return BadRequest("Could not add task. Check your Database Connection");
                }
            }
        }
    }
}