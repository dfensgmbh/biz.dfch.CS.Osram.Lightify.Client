using System;
using System.Collections.Generic;
using biz.dfch.CS.Osram.Lightify.Client.Model;
using biz.dfch.CS.Web.Utilities.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    [TestClass]
    public class ClientGetGroupsTest
    {
        private static readonly Uri OSRAM_LIGHTIFY_BASE_URI = new Uri("https://eu.lightify-api.org.example.com/lightify/services/");

        private const string USER_ID = "arbitraryUser";
        private const string SECURITY_TOKEN = "validSecurityToken";
        private const string USERNAME = "ArbitraryUser";
        private const string PASSWORD = "ArbitraryPassword";
        private const string SERIAL_NUMBER = "ArbitrarySerialNumber";

        [TestMethod]
        public void GetGroupsSucceeds()
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
                [{
	                ""groupId"": 1,
	                ""name"": ""First and Second"",
	                ""devices"": [2,
	                1],
	                ""scenes"": {
		
	                }
                },
                {
	                ""groupId"": 2,
	                ""name"": ""Third Lamp"",
	                ""devices"": [3],
	                ""scenes"": {
		
	                }
                }]";

            var restCallExecutor = Mock.Create<RestCallExecutor>();
            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, Arg.AnyString, Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(response)
                .OccursOnce();

            var result = sut.GetGroups();

            Mock.Assert(restCallExecutor);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }
    }
}
