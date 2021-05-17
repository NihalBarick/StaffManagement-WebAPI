using StaffManagement.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StaffManagement.Models.Interfaces
{
    public interface IUserProviderModel
    {
        int AuthenticateUser(string userName, string password);

        UserRegistrationData GetUserRegistrationData(int id);

        void RegisterUser(UserData userData);

        List<StaffResultData> GetAllStaff();
    }
}