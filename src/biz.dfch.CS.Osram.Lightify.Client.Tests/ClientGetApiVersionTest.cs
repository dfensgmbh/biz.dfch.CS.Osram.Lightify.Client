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
using Telerik.JustMock;
using biz.dfch.CS.Web.Utilities.Rest;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    [TestClass]
    public class ClientGetApiVersionTest
    {
        [TestMethod]
        public void GetApiVersionReturnsVersionOfOsramLightifyApi()
        {
            // Arrange
            var apiVersionAsJson = "{\"apiversion\":\"1.0.0\"}";
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
            var requestUri = new Uri(TestConstants.OSRAM_LIGHTIFY_BASE_URI, Lightify.Client.Constants.ApiOperation.VERSION);
            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, requestUri.AbsoluteUri, Arg.IsAny<Dictionary<string, string>>(), Arg.AnyString))
                .IgnoreInstance()
                .Returns(apiVersionAsJson)
                .OccursOnce();

            // Act
            var apiVersion = client.GetApiVersion();

            // Assert
            Assert.IsNotNull(apiVersion);
            Assert.AreEqual(1, apiVersion.Major);
            Assert.AreEqual(0, apiVersion.Minor);
            Assert.AreEqual(0, apiVersion.Build);

            Mock.Assert(restCallExecutor);
        }
    }
}
