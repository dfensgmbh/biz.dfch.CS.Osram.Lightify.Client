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
        [TestMethod]
        public void GetGroupByIdSucceeds()
        {
            var sut = new Client(Constants.OSRAM_LIGHTIFY_BASE_URI);
            sut.UserInformation = new UserInformation()
            {
                UserId = Constants.USER_ID,
                Username = Constants.USERNAME,
                Password = Constants.PASSWORD,
                SerialNumber = Constants.SERIAL_NUMBER,
                SecurityToken = Constants.SECURITY_TOKEN
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
            var sut = new Client(Constants.OSRAM_LIGHTIFY_BASE_URI);
            sut.UserInformation = new UserInformation()
            {
                UserId = Constants.USER_ID,
                Username = Constants.USERNAME,
                Password = Constants.PASSWORD,
                SerialNumber = Constants.SERIAL_NUMBER,
                SecurityToken = Constants.SECURITY_TOKEN
            };

            string responseFirstMock = @"
                [{
	                ""groupId"": 1,
	                ""name"": ""testgroup"",
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

            string responseSecondMock = @"
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
                .Returns(responseFirstMock)
                .InSequence();
            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, Arg.AnyString, Arg.IsAny<Dictionary<string, string>>(), ""))
                            .IgnoreInstance()
                            .Returns(responseSecondMock)
                            .InSequence();

            var result = sut.GetGroupByName("testgroup");

            Mock.Assert(restCallExecutor);
            Assert.IsNotNull(result);
        }
    }
}
