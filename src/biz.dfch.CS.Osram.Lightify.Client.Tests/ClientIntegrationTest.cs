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

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    [TestClass]
    public class ClientIntegrationTest
    {
        private Client Client;

        [TestInitialize]
        public void TestInitialize()
        {
            Client = new Client(IntegrationTestEnvironment.OsramApiBaseUri);
        }

        [TestMethod]
        [TestCategory("SkipOnTeamCity")]
        public void GetTokenSucceeds()
        {
            // Arrange

            // Act
            var sessionResponse = Client.GetToken(IntegrationTestEnvironment.Username,
                IntegrationTestEnvironment.Password, IntegrationTestEnvironment.SerialNumber);

            // Assert
            Assert.IsNotNull(sessionResponse);
        }

        [TestMethod]
        [TestCategory("SkipOnTeamCity")]
        public void TurnDeviceOnAndOffSucceeds()
        {
            // Arrange
            Client.GetToken(IntegrationTestEnvironment.Username,
                IntegrationTestEnvironment.Password, IntegrationTestEnvironment.SerialNumber);

            var devices = Client.GetDevices();
            Contract.Assert(null != devices);

            var device = devices.FirstOrDefault();
            Contract.Assert(null != device);

            // Act
            if (device.On == 1)
            {
                Contract.Assert(Client.TurnDeviceOff(device));
            }
            var succeeded = Client.TurnDeviceOn(device);

            device = Client.GetDevice(device.DeviceId);

            // Assert
            Assert.IsTrue(succeeded);
            Assert.AreEqual(1, device.On);
        }

        [TestMethod]
        [TestCategory("SkipOnTeamCity")]
        public void TurnGroupOnAndOffSucceeds()
        {
            // Arrange
            Client.GetToken(IntegrationTestEnvironment.Username, IntegrationTestEnvironment.Password,
                IntegrationTestEnvironment.SerialNumber);

            var groups = Client.GetGroups();
            Contract.Assert(null != groups);

            var firstGroup = groups.FirstOrDefault();
            Contract.Assert(null != firstGroup);

            Client.TurnLightGroupOff(firstGroup);

            // Act
            var succeededOn = Client.TurnLightGroupOn(firstGroup);
            System.Threading.Thread.Sleep(3000);
            var succeededOff = Client.TurnLightGroupOff(firstGroup);

            // Assert
            Assert.IsTrue(succeededOff);
            Assert.IsTrue(succeededOn);
        }

        [TestMethod]
        [TestCategory("SkipOnTeamCity")]
        public void SetColorSucceeds()
        {
            // Arrange
            Client.GetToken(IntegrationTestEnvironment.Username, IntegrationTestEnvironment.Password,
                IntegrationTestEnvironment.SerialNumber);

            var groups = Client.GetGroups();
            Contract.Assert(null != groups);

            var firstGroup = groups.FirstOrDefault();
            Contract.Assert(null != firstGroup);

            Client.TurnLightGroupOn(firstGroup);

            // Act
            var succeededColorChange = Client.SetGroupColor(firstGroup, "FF0000");
            var succeededColorTimedChange = Client.SetGroupColor(firstGroup, "0000FF", 100);
            System.Threading.Thread.Sleep(10000);
            // Assert
            Assert.IsTrue(succeededColorChange);
            Assert.IsTrue(succeededColorTimedChange);
            Client.TurnLightGroupOff(firstGroup);
        }
    }
}
