using System;
using System.Collections.Generic;
using biz.dfch.CS.Osram.Lightify.Client.Model;
using biz.dfch.CS.Web.Utilities.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    [TestClass]
    public class ClientGroupOperationTest
    {
        public Client Sut;
        public RestCallExecutor RestCallExecutor;

        private OperationResponse _successOperationResponse;

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

            _successOperationResponse = new OperationResponse
            {
                ReturnCode = 0
            };
        }

        [TestMethod]
        public void TurnLightGroupOnSucceeds()
        {
            // Arrange
            var groupId = 1L;
            var group = new Group
            {
                GroupId = groupId,
                Name = "group1"
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.AnyString, Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(_successOperationResponse.SerializeObject)
                .OccursOnce();

            // Act
            var result = Sut.TurnLightGroupOn(group);

            // Assert
            Mock.Assert(RestCallExecutor);
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TurnLightGroupOffSucceeds()
        {
            // Arrange
            var groupId = 1L;
            var group = new Group
            {
                GroupId = groupId,
                Name = "group1"
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.AnyString, Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(_successOperationResponse.SerializeObject)
                .OccursOnce();

            // Act
            var result = Sut.TurnLightGroupOff(group);

            // Assert
            Mock.Assert(RestCallExecutor);
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        public void SetGroupLevelSucceeds()
        {
            var groupId = 1L;
            var group = new Group
            {
                GroupId = groupId,
                Name = "group1"
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.AnyString, Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(_successOperationResponse.SerializeObject)
                .OccursOnce();

            var result = Sut.SetGroupLevel(group, 0.1f);

            Mock.Assert(RestCallExecutor);
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        public void SetGroupLevelWithTimeSucceeds()
        {
            var groupId = 1L;
            var group = new Group
            {
                GroupId = groupId,
                Name = "group1"
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.AnyString, Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(_successOperationResponse.SerializeObject)
                .OccursOnce();

            var result = Sut.SetGroupLevel(group, 0.1f);

            Mock.Assert(RestCallExecutor);
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }


    }
}
