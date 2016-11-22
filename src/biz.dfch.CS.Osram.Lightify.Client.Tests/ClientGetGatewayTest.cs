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
﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using biz.dfch.CS.Osram.Lightify.Client.Model;
using biz.dfch.CS.Web.Utilities.Rest;
using Telerik.JustMock;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    [TestClass]
    public class ClientGetGatewayTest
    {
        [TestMethod]
        public void GetGatewaySucceedsAndReturnsGateway()
        {
            // Arrange
            var gateway = new Gateway()
            {
                Version = TestConstants.GATEWAY_VERSION,
                SerialNumber = TestConstants.SERIAL_NUMBER,
                Online = true
            };

            var client = new Client(TestConstants.OSRAM_LIGHTIFY_BASE_URI);

            client.UserInformation = new UserInformation()
            {
                UserId = TestConstants.USER_ID,
                Username = TestConstants.USERNAME,
                Password = TestConstants.PASSWORD,
                SerialNumber = TestConstants.SERIAL_NUMBER,
                SecurityToken = TestConstants.SECURITY_TOKEN
            };

            var restCallExecutor = Mock.Create<RestCallExecutor>();
            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.GATEWAY)), Arg.IsAny<Dictionary<string, string>>(), Arg.AnyString))
                .IgnoreInstance()
                .Returns(gateway.SerializeObject()))
                .OccursOnce();

            // Act
            var result = client.GetGateway();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(TestConstants.GATEWAY_VERSION, result.Version);
            Assert.AreEqual(TestConstants.SERIAL_NUMBER, result.SerialNumber);
            Assert.IsTrue(result.Online);

            Mock.Assert(restCallExecutor);
        }
    }
}
