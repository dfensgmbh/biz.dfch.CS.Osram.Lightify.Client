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
            Sut.SetDeviceLevel(TestConstants.DEVICE_ID, -0.001f);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+level")]
        public void SetDeviceLevelWithLevelHigher1ThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceLevel(TestConstants.DEVICE_ID, 1.001f);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+time")]
        public void SetDeviceLevelWithTimeLessThanZeroThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceLevel(TestConstants.DEVICE_ID, 1.000f, -1);

            // Assert
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+device")]
        public void SetDeviceSaturationWithNullDeviceThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceSaturation(null, 0.500f, 0);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+id")]
        public void SetDeviceSaturationWithInvalidIdThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceSaturation(0, 0.500f, 0);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+saturation")]
        public void SetDeviceSaturationWithSaturationLessThanZeroThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceSaturation(TestConstants.DEVICE_ID, -0.001f, 0);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+saturation")]
        public void SetDeviceSaturationWithSaturationHigherThan1ThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceSaturation(TestConstants.DEVICE_ID, 1.001f, 0);

            // Assert
        }


        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+time")]
        public void SetDeviceSaturationWithTimeLessThanZeroThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceSaturation(TestConstants.DEVICE_ID, 1.000f, -1);

            // Assert
        }

        [TestMethod]
        public void SetDeviceSaturationSucceeds()
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
            var result = Sut.SetDeviceSaturation(device, 0.1f, 42);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);

            Mock.Assert(RestCallExecutor);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+device")]
        public void SetDeviceCTempWithNullDeviceThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceCTemp(null, TestConstants.CTEMP_MIN_VALUE, 0);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+id")]
        public void SetDeviceCTempWithInvalidIdThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceCTemp(0, TestConstants.CTEMP_MIN_VALUE, 0);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+ctemp")]
        public void SetDeviceCTempWithCtempLessThan1000ThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceCTemp(TestConstants.DEVICE_ID, TestConstants.CTEMP_MIN_VALUE - 1, 0);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+ctemp")]
        public void SetDeviceSaturationWithSaturationHigherThan8000ThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceCTemp(TestConstants.DEVICE_ID, TestConstants.CTEMP_MAX_VALUE + 1, 0);

            // Assert
        }


        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+time")]
        public void SetDeviceCTempWithTimeLessThanZeroThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceCTemp(TestConstants.DEVICE_ID, TestConstants.CTEMP_MIN_VALUE, -1);

            // Assert
        }

        [TestMethod]
        public void SetDeviceCTempSucceeds()
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
            var result = Sut.SetDeviceCTemp(device, TestConstants.CTEMP_MIN_VALUE, 42);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);

            Mock.Assert(RestCallExecutor);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+device")]
        public void SetDeviceColorWithNullDeviceThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceColor(null, TestConstants.SAMPLE_COLOR, 0);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+id")]
        public void SetDeviceColorWithInvalidIdThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceColor(0, TestConstants.SAMPLE_COLOR, 0);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+hexColorCode")]
        public void SetDeviceColorWithInvalidColorThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceColor(TestConstants.DEVICE_ID, "", 0);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+hexColorCode")]
        public void SetDeviceColorWithInvalidColorThrowsContractException2()
        {
            // Arrange

            // Act
            Sut.SetDeviceColor(TestConstants.DEVICE_ID, "ABCDEFG", 0);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+hexColorCode")]
        public void SetDeviceColorWithInvalidColorThrowsContractExceptio3()
        {
            // Arrange

            // Act
            Sut.SetDeviceColor(TestConstants.DEVICE_ID, "ABCDEI", 0);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+time")]
        public void SetDeviceColorWithTimeLessThanZeroThrowsContractException()
        {
            // Arrange

            // Act
            Sut.SetDeviceColor(TestConstants.DEVICE_ID, TestConstants.SAMPLE_COLOR, -1);

            // Assert
        }

        [TestMethod]
        public void SetDeviceColorSucceeds()
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
            var result = Sut.SetDeviceColor(device, TestConstants.SAMPLE_COLOR, 42);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);

            Mock.Assert(RestCallExecutor);
        }
    }
}
