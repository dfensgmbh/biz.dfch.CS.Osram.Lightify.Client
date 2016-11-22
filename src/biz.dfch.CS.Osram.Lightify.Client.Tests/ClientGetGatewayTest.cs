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
using biz.dfch.CS.Osram.Lightify.Client.Model;
using biz.dfch.CS.Web.Utilities.Rest;
using Telerik.JustMock;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    [TestClass]
    public class ClientGetGatewayTest
    {
        private const string gatewayAsJson = @"
            {
	            ""version"": ""1.1.3.22-1.2.0.68"",
	            ""serialNumber"": ""OSR42424242"",
	            ""online"": true
            }";

        [TestMethod]
        public void GetGatewaySucceedsAndReturnsGateway()
        {
            // Arrange
            var client = new Client(Constants.OSRAM_LIGHTIFY_BASE_URI);

            client.UserInformation = new UserInformation()
            {
                UserId = Constants.USER_ID,
                Username = Constants.USERNAME,
                Password = Constants.PASSWORD,
                SerialNumber = Constants.SERIAL_NUMBER,
                SecurityToken = Constants.SECURITY_TOKEN
            };

            var restCallExecutor = Mock.Create<RestCallExecutor>();
            var requestUri = new Uri(Constants.OSRAM_LIGHTIFY_BASE_URI, Lightify.Client.Constants.ApiSuffixes.GATEWAY);
            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, requestUri.AbsoluteUri, Arg.IsAny<Dictionary<string, string>>(), Arg.AnyString))
                .IgnoreInstance()
                .Returns(gatewayAsJson)
                .OccursOnce();

            // Act
            var gateway = client.GetGateway();

            // Assert
            Assert.IsNotNull(gateway);
            Assert.AreEqual("1.1.3.22-1.2.0.68", gateway.Version);
            Assert.AreEqual("OSR42424242", gateway.SerialNumber);
            Assert.IsTrue(gateway.Online);

            Mock.Assert(restCallExecutor);
        }
    }
}
