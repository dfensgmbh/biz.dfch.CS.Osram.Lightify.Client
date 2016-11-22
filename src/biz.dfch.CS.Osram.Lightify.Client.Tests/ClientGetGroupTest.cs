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
        public Client Sut;
        public RestCallExecutor RestCallExecutor;

        [TestInitialize]
        public void TestInitialize()
        {
        
            Sut = new Client(TestConstants.OSRAM_LIGHTIFY_BASE_URI)
            {
                UserInformation = new UserInformation()
                {
                    UserId = TestConstants.USER_ID,
                    Username = TestConstants.USERNAME,
                    Password = TestConstants.PASSWORD,
                    SerialNumber = TestConstants.SERIAL_NUMBER,
                    SecurityToken = TestConstants.SECURITY_TOKEN
                }
            };
            
            RestCallExecutor = Mock.Create<RestCallExecutor>();
        }

        [TestMethod]
        public void GetGroupByIdSucceeds()
        {
            var groupId = 1L;
            var responseGroup = new Group
            {
                GroupId = groupId,
                Name = "group1"
            };


            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.AnyString, Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(responseGroup.SerializeObject)
                .OccursOnce();

            var result = Sut.GetGroup(1);

            Mock.Assert(RestCallExecutor);
            Assert.IsNotNull(result);
            Assert.AreEqual(responseGroup.GroupId, result.GroupId);
        }

        [TestMethod]
        public void GetGroupByNameSucceeds()
        {
            var groups = new List<Group>();
            groups.Add(new Group
            {
                GroupId = 1L,
                Name = "group1"
            });
            groups.Add(new Group
            {
                GroupId = 2L,
                Name = "group2"
            });

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.AnyString, Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(Newtonsoft.Json.JsonConvert.SerializeObject(groups))
                .InSequence();
            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.AnyString, Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(groups[1].SerializeObject)
                .InSequence();

            var result = Sut.GetGroup("testgroup");

            Mock.Assert(RestCallExecutor);
            Assert.IsNotNull(result);
        }
    }
}
