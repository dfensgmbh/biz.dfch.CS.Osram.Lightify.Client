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
        public void TurnLightGroupOnSucceeds()
        {
            // Arrange
            var groupId = 1L;
            var group = new Group
            {
                GroupId = groupId,
                Name = "group1"
            };
            var responseOperation = new OperationResponse
            {
                ReturnCode = 0
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.AnyString, Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(responseOperation.SerializeObject)
                .OccursOnce();

            // Act
            var result = Sut.TurnLightGroupOn(group);

            // Assert
            Mock.Assert(RestCallExecutor);
            Assert.IsNotNull(result);
            Assert.AreEqual(responseOperation.ReturnCode, result.ReturnCode);
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
            var responseOperation = new OperationResponse
            {
                ReturnCode = 0
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.AnyString, Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(responseOperation.SerializeObject)
                .OccursOnce();

            // Act
            var result = Sut.TurnLightGroupOff(group);

            // Assert
            Mock.Assert(RestCallExecutor);
            Assert.IsNotNull(result);
            Assert.AreEqual(responseOperation.ReturnCode, result.ReturnCode);
        }

        public void SetGroupLevelSucceeds()
        {
            var groupId = 1L;
            var group = new Group
            {
                GroupId = groupId,
                Name = "group1"
            };
            var responseOperation = new OperationResponse
            {
                ReturnCode = 0
            };


            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.AnyString, Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(responseOperation.SerializeObject)
                .OccursOnce();

            var result = Sut.SetLevelGroup(group, 0.1);

            Mock.Assert(RestCallExecutor);
            Assert.IsNotNull(result);
            Assert.AreEqual(responseOperation.ReturnCode, result.ReturnCode);
        }

        public void SetGroupLevelWithTimeSucceeds()
        {
            var groupId = 1L;
            var group = new Group
            {
                GroupId = groupId,
                Name = "group1"
            };
            var responseOperation = new OperationResponse
            {
                ReturnCode = 0
            };


            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.AnyString, Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(responseOperation.SerializeObject)
                .OccursOnce();

            var result = Sut.SetLevelGroup(group, 0.1);

            Mock.Assert(RestCallExecutor);
            Assert.IsNotNull(result);
            Assert.AreEqual(responseOperation.ReturnCode, result.ReturnCode);
        }


    }
}
