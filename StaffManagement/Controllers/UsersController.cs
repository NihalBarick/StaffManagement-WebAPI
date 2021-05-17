using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StaffManagement.Models;
using StaffManagement.Models.Dto;
using StaffManagement.Models.Helper;
using StaffManagement.Models.Interfaces;

namespace StaffManagement.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserProviderModel userProviderModel = null;

        private readonly IValidationHelper validationHelper = null;

        public UsersController()
        {
            this.userProviderModel = new UserProviderModel();
            this.validationHelper = new ValidationHelper();
        }

        public UsersController(IUserProviderModel userProviderModel, IValidationHelper validationHelper)
        {
            this.userProviderModel = userProviderModel;
            this.validationHelper = validationHelper;
        }

        [HttpGet]
        [Route("api/Users/staff")]
        public IHttpActionResult GetStaff()
        {
            try
            {
                return Ok(this.userProviderModel.GetAllStaff());
            }
            catch
            {
                return BadRequest();
            }            
        }

        [HttpPost]
        [Route("api/Users")]
        public IHttpActionResult Post([FromBody] LoginRequestData loginRequestData)
        {
            if (loginRequestData != null)
            {
                int userId = this.userProviderModel.AuthenticateUser(loginRequestData.UserName, loginRequestData.Password);

                if (userId > 0)
                {
                    return Ok(this.userProviderModel.GetUserRegistrationData(userId));
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/Users/Add")]
        public IHttpActionResult PostNewUser([FromBody] UserData user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            else if (user.UserName == string.Empty || user.Password == string.Empty || user.RoleId != 2)
            {
                return BadRequest("Parameters cannot be empty");
            }
            else if (this.validationHelper.IsValidTime(user.DailyCheckInTime) == false || this.validationHelper.IsValidTime(user.DailyCheckOutTime) == false || this.validationHelper.IsValidWorkingHours(user.DailyCheckInTime, user.DailyCheckOutTime) == false)
            {
                return BadRequest("Time Format is not valid");
            }
            else
            {
                try
                {
                    this.userProviderModel.RegisterUser(user);
                }
                catch
                {
                    return BadRequest();
                }

                return Ok();
            }
        }
    }
}