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
            var major = 1;
            var minor = 2;
            var build = 3;
            var version = new Version(major, minor, build, 0);
            var apiVersionResponse = new ApiVersion()
            {
                Version = version
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
            var requestUri = new Uri(TestConstants.OSRAM_LIGHTIFY_BASE_URI, Constants.ApiOperation.VERSION);
            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, requestUri.AbsoluteUri, Arg.IsAny<Dictionary<string, string>>(), Arg.AnyString))
                .IgnoreInstance()
                .Returns(apiVersionResponse.SerializeObject())
                .OccursOnce();

            // Act
            var result = client.GetApiVersion();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(major, result.Major);
            Assert.AreEqual(minor, result.Minor);
            Assert.AreEqual(build, result.Build);

            Mock.Assert(restCallExecutor);
        }
    }
}
