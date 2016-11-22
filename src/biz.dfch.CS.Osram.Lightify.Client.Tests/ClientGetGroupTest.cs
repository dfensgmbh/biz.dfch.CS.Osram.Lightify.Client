using System;
using System.Collections.Generic;
using biz.dfch.CS.Osram.Lightify.Client.Model;
using biz.dfch.CS.Web.Utilities.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    [TestClass]
    public class ClientGetGroupTest
    {
        private static readonly Uri OSRAM_LIGHTIFY_BASE_URI = new Uri("https://eu.lightify-api.org.example.com/lightify/services/");

        private const string USER_ID = "arbitraryUser";
        private const string SECURITY_TOKEN = "validSecurityToken";
        private const string USERNAME = "ArbitraryUser";
        private const string PASSWORD = "ArbitraryPassword";
        private const string SERIAL_NUMBER = "ArbitrarySerialNumber";

        [TestMethod]
        public void GetGroupByIdSucceeds()
        {
            var sut = new Client(OSRAM_LIGHTIFY_BASE_URI);
            sut.UserInformation = new UserInformation()
            {
                UserId = USER_ID,
                Username = USERNAME,
                Password = PASSWORD,
                SerialNumber = SERIAL_NUMBER,
                SecurityToken = SECURITY_TOKEN
            };
            var groupId = 1L;

            string response = @"
                            {
	            ""groupId"": 1,
	            ""name"": ""First and Secon"",
	            ""devices"": [2,
	            1],
	            ""scenes"": {
		
	            }
            }";

            var restCallExecutor = Mock.Create<RestCallExecutor>();
            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, Arg.AnyString, Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(response)
                .OccursOnce();

            var result = sut.GetGroupById(1);

            Mock.Assert(restCallExecutor);
            Assert.IsNotNull(result);
            Assert.AreEqual(groupId, result.GroupId);
        }

        [TestMethod]
        public void GetGroupByNameSucceeds()
        {
            var sut = new Client(OSRAM_LIGHTIFY_BASE_URI);
            sut.UserInformation = new UserInformation()
            {
                UserId = USER_ID,
                Username = USERNAME,
                Password = PASSWORD,
                SerialNumber = SERIAL_NUMBER,
                SecurityToken = SECURITY_TOKEN
            };

            string response = @"
                            {
	            ""groupId"": 1,
	            ""name"": ""testgroup"",
	            ""devices"": [2,
	            1],
	            ""scenes"": {
		
	            }
            }";

            var restCallExecutor = Mock.Create<RestCallExecutor>();
            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, Arg.AnyString, Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(response)
                .OccursOnce();

            var result = sut.GetGroupByName("testgroup");

            Mock.Assert(restCallExecutor);
            Assert.IsNotNull(result);
        }
    }
}
