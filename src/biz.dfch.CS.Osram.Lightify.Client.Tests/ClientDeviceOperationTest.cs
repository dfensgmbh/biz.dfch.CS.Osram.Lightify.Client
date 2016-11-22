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
 
﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using biz.dfch.CS.Web.Utilities.Rest;
using biz.dfch.CS.Osram.Lightify.Client.Model;
﻿using biz.dfch.CS.Testing.Attributes;
﻿using Telerik.JustMock;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    [TestClass]
    public class ClientDeviceOperationTest
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
        [ExpectContractFailure(MessagePattern = "Precondition.+id")]
        public void TurnDeviceOnWithInvalidIdThrowsContractException()
        {
            // Arrange

            // Act
            Sut.TurnDeviceOn(0);

            // Assert
        }

        [TestMethod]
        public void TurnDeviceOnSucceeds()
        {
            // Arrange
            var device = new Device
            {
                DeviceId = TestConstants.DEVICE_ID,
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.DEVICE_SET)), Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(_successOperationResponse.SerializeObject)
                .OccursOnce();

            // Act
            var result = Sut.TurnDeviceOn(device);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);

            Mock.Assert(RestCallExecutor);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+id")]
        public void TurnDeviceOffWithInvalidIdThrowsContractException()
        {
            // Arrange

            // Act
            Sut.TurnDeviceOff(0);

            // Assert
        }

        [TestMethod]
        public void TurnDeviceOffSucceeds()
        {
            // Arrange
            var device = new Device
            {
                DeviceId = TestConstants.DEVICE_ID,
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.DEVICE_SET)), Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(_successOperationResponse.SerializeObject)
                .OccursOnce();

            // Act
            var result = Sut.TurnDeviceOff(device);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);

            Mock.Assert(RestCallExecutor);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+id")]
        public void SetDeviceLevelWithInvalidIdThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceLevel(0, 1.000f);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+level")]
        public void SetDeviceLevelWithLevelLessThanZeroThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceLevel(1, -0.001f);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+level")]
        public void SetDeviceLevelWithLevelHigher1ThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceLevel(1, 1.001f);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+time")]
        public void SetDeviceLevelWithTimeLessThanZeroThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceLevel(1, 1.000f, -1);

            // Assert
        }

        public void SetDeviceLevelSucceeds()
        {
            // Arrange
            var device = new Device
            {
                DeviceId = TestConstants.DEVICE_ID,
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.DEVICE_SET)), Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(_successOperationResponse.SerializeObject)
                .OccursOnce();

            // Act
            var result = Sut.SetDeviceLevel(device, 0.1f);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);

            Mock.Assert(RestCallExecutor);
        }

        public void SetDeviceLevelWithTimeSucceeds()
        {
            // Arrange
            var device = new Device
            {
                DeviceId = TestConstants.DEVICE_ID,
            };

            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.DEVICE_SET)), Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(_successOperationResponse.SerializeObject)
                .OccursOnce();

            // Act
            var result = Sut.SetDeviceLevel(device, 0.1f, 42);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);

            Mock.Assert(RestCallExecutor);
        }
    }
}
