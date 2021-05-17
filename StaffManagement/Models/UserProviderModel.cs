using StaffManagement.Models.Dto;
using StaffManagement.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StaffManagement.Models
{
    public class UserProviderModel : IUserProviderModel
    {
        public int AuthenticateUser(string userName, string password)
        {
            int id = 0;

            using (var context = new StaffManagementEntities())
            {
                var user = context.Users.Where(s => s.UserName == userName && s.Password == password).FirstOrDefault();

                if (user != null)
                {
                    id = user.UserId;
                }
            }

            return id;
        }

        public UserRegistrationData GetUserRegistrationData(int id)
        {
            using (var context = new StaffManagementEntities())
            {
                var query = from user in context.Users
                            join role in context.Roles
                            on user.RoleId equals role.RoleId
                            where user.UserId == id
                            select new UserRegistrationData
                            {
                                UserId = id,
                                UserName = user.UserName,
                                Role = role.RoleName
                            };

                if (query != null)
                {
                    return query.FirstOrDefault();
                }
            }

            return null;
        }

        public void RegisterUser(UserData userData)
        {
            using (var context = new StaffManagementEntities())
            {
                context.Users.Add(new User 
                { 
                    UserName = userData.UserName,
                    Password = userData.Password,
                    RoleId = userData.RoleId,
                    DailyCheckInTime = TimeSpan.Parse(userData.DailyCheckInTime),
                    DailyCheckOutTime = TimeSpan.Parse(userData.DailyCheckOutTime)
                });

                context.SaveChanges();
            }
        }

        public List<StaffResultData> GetAllStaff()
        {
            List<StaffResultData> users = new List<StaffResultData>();

            using (var context = new StaffManagementEntities())
            {
                var query = from user in context.Users
                            where user.RoleId == 2
                            select new StaffResultData()
                            {
                                UserId = user.UserId,
                                UserName = user.UserName,
                                DailyCheckInTime = user.DailyCheckInTime,
                                DailyCheckOutTime = user.DailyCheckOutTime
                            };

                foreach(var result in query)
                {
                    users.Add(result);
                }
            }

            return users;
        }
    }
}