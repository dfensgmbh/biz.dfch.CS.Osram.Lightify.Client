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

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biz.dfch.CS.Osram.Lightify.Client.Model;
using biz.dfch.CS.Web.Utilities.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Telerik.JustMock;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    [TestClass]
    public class ClientGetDevicesTest
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
        public void GetDevicesSucceeds()
        {
            // Arrange
            var name = "arbitraryName";

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

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.IsAny<string>(), Arg.IsAny<Dictionary<string, string>>(), Arg.IsAny<string>()))
                .IgnoreInstance()
                .Returns(JsonConvert.SerializeObject(devices))
                .OccursOnce();

            // Act
            var result = (List<Device>) Sut.GetDevices();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(devices.Count, result.Count);
            Assert.AreEqual(devices[0].DeviceId, result[0].DeviceId);
            Assert.AreEqual(devices[1].DeviceId, result[1].DeviceId);
        }

        [TestMethod]
        public void GetDevicesByGroupIdSucceeds()
        {
            // Arrange
            var group1 = new Group {GroupId = 1};

            var name = "arbitraryName";

            var devices = new List<Device>
            {
                new Device
                {
                    DeviceId = 1,
                    Name = "arbitraryName1",
                    GroupList = new long[] { 1, 2 },
                }
                ,
                new Device
                {
                    DeviceId = 2,
                    Name = "arbitraryName2",
                    GroupList = new long[] { 2, 3 },
                }
                ,
                new Device
                {
                    DeviceId = 3,
                    Name = "arbitraryName3",
                    GroupList = new long[] { 1, 3 },
                }
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.IsAny<string>(), Arg.IsAny<Dictionary<string, string>>(), Arg.IsAny<string>()))
                .IgnoreInstance()
                .Returns(JsonConvert.SerializeObject(devices))
                .OccursOnce();

            // Act
            var result = (List<Device>) Sut.GetDevices(group1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(devices[0].DeviceId, result[0].DeviceId);
            Assert.AreEqual(devices[2].DeviceId, result[1].DeviceId);
        }

        [TestMethod]
        public void GetDevicesByInexistentGroupIdReturnsEmptyList()
        {
            // Arrange
            var name = "arbitraryName";

            var devices = new List<Device>
            {
                new Device
                {
                    DeviceId = 1,
                    Name = "arbitraryName1",
                    GroupList = new long[] { 1, 2 },
                }
                ,
                new Device
                {
                    DeviceId = 2,
                    Name = "arbitraryName2",
                    GroupList = new long[] { 2, 3 },
                }
                ,
                new Device
                {
                    DeviceId = 3,
                    Name = "arbitraryName3",
                    GroupList = new long[] { 1, 3 },
                }
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.IsAny<string>(), Arg.IsAny<Dictionary<string, string>>(), Arg.IsAny<string>()))
                .IgnoreInstance()
                .Returns(JsonConvert.SerializeObject(devices))
                .OccursOnce();

            // Act
            var result = (List<Device>) Sut.GetDevices(42);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}
