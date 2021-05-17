using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using StaffManagement.Controllers;
using StaffManagement.Models.Dto;
using StaffManagement.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http;
using System.Web.Http.Results;

namespace StaffManagement.UnitTests
{
    [TestClass()]
    class UsersControllerTest
    {
        [TestMethod]
        public void GetStaffTest()
        {
            // Arrange
            var userProviderModelStub = MockRepository.GenerateMock<IUserProviderModel>();
            var validationStub = MockRepository.GenerateMock<IValidationHelper>();

            List<StaffResultData> result = new List<StaffResultData>();

            StaffResultData dummyStaffData = new StaffResultData() { UserId = 0, UserName = "Test", DailyCheckInTime = new TimeSpan(8, 0, 0, 0), DailyCheckOutTime = new TimeSpan(10, 0, 0, 0) };

            StaffResultData dummyStaffData2 = new StaffResultData() { UserId = 0, UserName = "Test1", DailyCheckInTime = new TimeSpan(8, 0, 0, 0), DailyCheckOutTime = new TimeSpan(10, 0, 0, 0) };

            result.Add(dummyStaffData);

            result.Add(dummyStaffData2);

            userProviderModelStub.Expect(item => item.GetAllStaff()).Return(result);

            var controller = new UsersController(userProviderModelStub, validationStub);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act            
            var responseMessage = controller.GetStaff();

            // Assert
            Assert.IsInstanceOfType(responseMessage, typeof(OkNegotiatedContentResult<IList<StaffResultData>>));
        }

        [TestMethod]
        public void AuthenticateWithNullTest()
        {
            // Arrange
            var userProviderModelStub = MockRepository.GenerateMock<IUserProviderModel>();
            var validationStub = MockRepository.GenerateMock<IValidationHelper>();

            string userName = "";
            string password = "";

            userProviderModelStub.Expect(item => item.AuthenticateUser(userName, password)).Return(0);

            var controller = new UsersController(userProviderModelStub, validationStub);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            
            try
            {
                // Act            
               var response = controller.Post(null);

                //Assert
                Assert.IsInstanceOfType(response, typeof(BadRequestResult));
            }
            catch
            {
                Assert.Fail();
            }    
        }
    }
}
