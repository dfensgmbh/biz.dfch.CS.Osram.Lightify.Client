/**
 * Copyright 2016 d-fens GmbH
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using biz.dfch.CS.Osram.Lightify.Client.Model;
using biz.dfch.CS.Testing.Attributes;
using biz.dfch.CS.Web.Utilities.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Telerik.JustMock;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    [TestClass]
    public class ClientGetDeviceTest
    {
        public Client Sut { get; set; }
        public RestCallExecutor RestCallExecutor { get; set; }

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
        [ExpectContractFailure(MessagePattern = "id")]
        public void GetDeviceWithInvalidIdThrowsContractException()
        {
            // Arrange
            var id = 0;
            var sut = new Client(TestConstants.OSRAM_LIGHTIFY_BASE_URI);

            // Act
            var result = sut.GetDevice(id);

            // Assert
        }
            
        [TestMethod]
        public void GetDeviceByIdSucceeds()
        {
            // Arrange
            var id = 42;

            var response = new Device
            {
                DeviceId = id,
                Name = "arbitaryName"
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.DEVICES)), Arg.IsAny<Dictionary<string, string>>(), Arg.IsAny<string>()))
                .IgnoreInstance()
                .Returns(response.SerializeObject())
                .OccursOnce();

            // Act
            var result = Sut.GetDevice(id);

            // Assert
        }

        [TestMethod]
        public void GetDeviceByIdWithInconsistentResponseReturnsNull()
        {
            // Arrange
            var id = 42;

            var response = new Device
            {
                DeviceId = 1,
                Name = "arbitaryName"
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.DEVICES)), Arg.IsAny<Dictionary<string, string>>(), Arg.IsAny<string>()))
                .IgnoreInstance()
                .Returns(response.SerializeObject())
                .OccursOnce();

            // Act
            var result = Sut.GetDevice(id);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetDeviceByNameSucceeds()
        {
            // Arrange
            var name = "arbitaryName";

            var devices = new List<Device>
            {
                new Device
                {
                    DeviceId = 1,
                    Name = name
                }
                ,
                new Device
                {
                    DeviceId = 2,
                    Name = "otherName"
                }
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.DEVICES)), Arg.IsAny<Dictionary<string, string>>(), Arg.IsAny<string>()))
                .IgnoreInstance()
                .Returns(JsonConvert.SerializeObject(devices))
                .OccursOnce();

            // Act
            var result = Sut.GetDevice(name);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(devices[0].DeviceId, result.DeviceId);
            Assert.AreEqual(devices[0].Name, result.Name);
        }

        [TestMethod]
        public void GetDeviceByNameCaseSensitiveSucceeds()
        {
            // Arrange
            var name = "arbitaryName";

            var devices = new List<Device>
            {
                new Device
                {
                    DeviceId = 1,
                    Name = name.ToUpper()
                }
                ,
                new Device
                {
                    DeviceId = 2,
                    Name = "otherName"
                }
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.DEVICES)), Arg.IsAny<Dictionary<string, string>>(), Arg.IsAny<string>()))
                .IgnoreInstance()
                .Returns(JsonConvert.SerializeObject(devices))
                .OccursOnce();

            // Act
            var result = Sut.GetDevice(name, ignoreCase: true);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(devices[0].DeviceId, result.DeviceId);
            Assert.AreEqual(name.ToUpper(), result.Name);
        }

        [TestMethod]
        public void GetDeviceByNameCaseSensitiveReturnsNull()
        {
            // Arrange
            var name = "arbitaryName";

            var devices = new List<Device>
            {
                new Device
                {
                    DeviceId = 1,
                    Name = name.ToUpper()
                }
                ,
                new Device
                {
                    DeviceId = 2,
                    Name = "otherName"
                }
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.DEVICES)), Arg.IsAny<Dictionary<string, string>>(), Arg.IsAny<string>()))
                .IgnoreInstance()
                .Returns(JsonConvert.SerializeObject(devices))
                .OccursOnce();

            // Act
            var result = Sut.GetDevice(name, ignoreCase: false);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "devicesMatchingName.Count")]
        public void GetDeviceByNameWithDuplicateNameThrowsContractException()
        {
            // Arrange
            var name = "arbitaryName";

            var devices = new List<Device>
            {
                new Device
                {
                    DeviceId = 1,
                    Name = name
                }
                ,
                new Device
                {
                    DeviceId = 2,
                    Name = name
                }
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.DEVICES)), Arg.IsAny<Dictionary<string, string>>(), Arg.IsAny<string>()))
                .IgnoreInstance()
                .Returns(JsonConvert.SerializeObject(devices))
                .OccursOnce();

            // Act
            var result = Sut.GetDevice(name);

            // Assert
            Assert.IsNull(result);
        }
    }
}
