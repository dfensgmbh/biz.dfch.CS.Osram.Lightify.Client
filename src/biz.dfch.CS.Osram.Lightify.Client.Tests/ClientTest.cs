﻿/**
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
using System.Net.Http;
using biz.dfch.CS.Osram.Lightify.Client.Model;
using biz.dfch.CS.Testing.Attributes;
using biz.dfch.CS.Web.Utilities.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using HttpMethod = biz.dfch.CS.Web.Utilities.Rest.HttpMethod;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    [TestClass]
    public class ClientTest
    {
        private SessionResponse _sessionResponse = new SessionResponse
            {
                UserId = TestConstants.USER_ID,
                SecurityToken = TestConstants.SECURITY_TOKEN
            };

        private Dictionary<string, string> _authorizationHeaders = new Dictionary<string, string>()
            {
                {Constants.HttpHeaders.AUTHORIZATION, TestConstants.SECURITY_TOKEN}
            };

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+username")]
        public void GetTokenWithNullUsernameThrowsContractException()
        {
            // Arrange
            var sut = new Client(TestConstants.OSRAM_LIGHTIFY_BASE_URI);

            var username = default(string);
            var password = default(string);
            var serialNumber = default(string);

            // Act
            sut.GetToken(username, password, serialNumber);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+password")]
        public void GetTokenWithNullPasswordThrowsContractException()
        {
            // Arrange
            var sut = new Client(TestConstants.OSRAM_LIGHTIFY_BASE_URI);

            var username = TestConstants.USERNAME;
            var password = default(string);
            var serialNumber = default(string);

            // Act
            sut.GetToken(username, password, serialNumber);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+serialNumber")]
        public void GetTokenWithNullSerialNumberThrowsContractException()
        {
            // Arrange
            var sut = new Client(TestConstants.OSRAM_LIGHTIFY_BASE_URI);

            var username = TestConstants.USERNAME;
            var password = TestConstants.PASSWORD;
            var serialNumber = default(string);

            // Act
            sut.GetToken(username, password, serialNumber);

            // Assert
        }

        [TestMethod]
        public void GetTokenSucceeds()
        {
            // Arrange
            var restCallExecutor = Mock.Create<RestCallExecutor>();
            Mock.Arrange(() => restCallExecutor.Invoke(Arg.IsAny<HttpMethod>(), Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.SESSION)), Arg.IsAny<IDictionary<string, string>>(), Arg.AnyString))
                .IgnoreInstance()
                .Returns(_sessionResponse.SerializeObject())
                .OccursOnce();

            var sut = new Client(TestConstants.OSRAM_LIGHTIFY_BASE_URI);

            // Act
            var result = sut.GetToken(TestConstants.USERNAME, TestConstants.PASSWORD, TestConstants.SERIAL_NUMBER);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));

            Mock.Assert(restCallExecutor);
        }

        [TestMethod]
        public void GetTokenSetsUserInformation()
        {
            // Arrange
            var restCallExecutor = Mock.Create<RestCallExecutor>();
            Mock.Arrange(() => restCallExecutor.Invoke(Arg.IsAny<HttpMethod>(), Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.SESSION)), Arg.IsAny<IDictionary<string, string>>(), Arg.AnyString))
                .IgnoreInstance()
                .Returns(_sessionResponse.SerializeObject())
                .OccursOnce();

            var sut = new Client(TestConstants.OSRAM_LIGHTIFY_BASE_URI);

            // Act
            var result = sut.GetToken(TestConstants.USERNAME, TestConstants.PASSWORD, TestConstants.SERIAL_NUMBER);
            
            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
            Assert.IsNotNull(sut.UserInformation);
            Assert.AreEqual(TestConstants.USER_ID, sut.UserInformation.UserId);
            Assert.AreEqual(TestConstants.USERNAME, sut.UserInformation.Username);
            Assert.AreEqual(TestConstants.PASSWORD, sut.UserInformation.Password);
            Assert.AreEqual(TestConstants.SERIAL_NUMBER, sut.UserInformation.SerialNumber);
            Assert.AreEqual(TestConstants.SECURITY_TOKEN, sut.UserInformation.SecurityToken);

            Mock.Assert(restCallExecutor);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+requestUriSuffix")]
        public void InvokeWithNullRequestUriSuffixThrowsContractException()
        {
            // Arrange
            var client = new Client(TestConstants.OSRAM_LIGHTIFY_BASE_URI);

            // Act
            client.Invoke(HttpMethod.Get, null, null, null);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+requestUriSuffix")]
        public void InvokeWithEmptyRequestUriSuffixThrowsContractException()
        {
            // Arrange
            var client = new Client(TestConstants.OSRAM_LIGHTIFY_BASE_URI);

            // Act
            client.Invoke(HttpMethod.Get, "", null, null);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+!headers.ContainsKey.Constants.HttpHeaders.AUTHORIZATION")]
        public void InvokeWithHeadersContainingAuthorizationHeaderThrowsContractException()
        {
            // Arrange
            var client = new Client(TestConstants.OSRAM_LIGHTIFY_BASE_URI);

            client.UserInformation = new UserInformation()
            {
                UserId = TestConstants.USER_ID,
                Username = TestConstants.USERNAME,
                Password = TestConstants.PASSWORD,
                SerialNumber = TestConstants.SERIAL_NUMBER,
                SecurityToken = TestConstants.SECURITY_TOKEN
            };

            // Act
            client.Invoke(HttpMethod.Get, Constants.ApiOperation.SESSION, _authorizationHeaders, null);

            // Assert
        }

        [TestMethod]
        public void InvokeWithNullBodyCallsRestExecutorWithEmptyBody()
        {
            // Arrange
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
            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.SESSION)), _authorizationHeaders, ""))
                .IgnoreInstance()
                .Returns(_sessionResponse.SerializeObject())
                .OccursOnce();

            // Act
            var responseAsString = client.Invoke(HttpMethod.Get, Constants.ApiOperation.SESSION, null, null);

            // Assert
            Assert.IsNotNull(responseAsString);
            Assert.IsFalse(string.IsNullOrWhiteSpace(responseAsString));

            Mock.Assert(restCallExecutor);
        }

        [TestMethod]
        public void InvokeCallsRestExecutorWithAuthorizationHeaderAndReturnsResponseAsString()
        {
            // Arrange
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
            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.SESSION)), _authorizationHeaders, Arg.AnyString))
                .IgnoreInstance()
                .Returns(_sessionResponse.SerializeObject())
                .OccursOnce();

            // Act
            var responseAsString = client.Invoke(HttpMethod.Get, Constants.ApiOperation.SESSION, null, null);

            // Assert
            Assert.IsNotNull(responseAsString);
            Assert.IsFalse(string.IsNullOrWhiteSpace(responseAsString));

            Mock.Assert(restCallExecutor);
        }

        [TestMethod]
        public void InvokeWithQueryParametersCallsRestExecutorWithUriContainingQueryParameters()
        {
            // Arrange
            var client = new Client(TestConstants.OSRAM_LIGHTIFY_BASE_URI);

            client.UserInformation = new UserInformation()
            {
                UserId = TestConstants.USER_ID,
                Username = TestConstants.USERNAME,
                Password = TestConstants.PASSWORD,
                SerialNumber = TestConstants.SERIAL_NUMBER,
                SecurityToken = TestConstants.SECURITY_TOKEN
            };

            var queryParams = new Dictionary<string, object>()
            {
                {Constants.QueryParameter.IDX, TestConstants.IDX_VALUE},
                {Constants.QueryParameter.ON_OFF, "true"}
            };

            var restCallExecutor = Mock.Create<RestCallExecutor>();
            var queryString = string.Format("?{0}={1}&{2}={3}", Constants.QueryParameter.IDX, TestConstants.IDX_VALUE, Constants.QueryParameter.ON_OFF, true.ToString().ToLower());
            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.EndsWith(queryString)), _authorizationHeaders, Arg.AnyString))
                .IgnoreInstance()
                .Returns(_sessionResponse.SerializeObject())
                .OccursOnce();

            // Act
            var responseAsString = client.Invoke(HttpMethod.Get, Constants.ApiOperation.SESSION, queryParams, null, null);

            // Assert
            Assert.IsNotNull(responseAsString);
            Assert.IsFalse(string.IsNullOrWhiteSpace(responseAsString));

            Mock.Assert(restCallExecutor);
        }

        [TestMethod]
        public void InvokeRefreshesTokenIfCallToApiFails()
        {
            // Arrange
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
            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.DEVICES)), _authorizationHeaders, Arg.AnyString))
                .IgnoreInstance()
                .Throws<HttpRequestException>()
                .InSequence()
                .OccursOnce();

            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Post, Arg.Matches<string>(s => s.EndsWith(Constants.ApiOperation.SESSION)), Arg.IsAny<Dictionary<string, string>>(), Arg.AnyString))
                .IgnoreInstance()
                .Returns(_sessionResponse.SerializeObject())
                .OccursOnce();

            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.DEVICES)), _authorizationHeaders, Arg.AnyString))
                .IgnoreInstance()
                .Returns(new Device().SerializeObject)
                .InSequence()
                .OccursOnce();

            // Act
            var responseAsString = client.Invoke(HttpMethod.Get, Constants.ApiOperation.DEVICES, null, null);

            // Assert
            Assert.IsNotNull(responseAsString);
            Assert.IsFalse(string.IsNullOrWhiteSpace(responseAsString));

            Mock.Assert(restCallExecutor);
        }

        [TestMethod]
        public void GenericInvokeCallsRestExecutorWithAuthorizationHeaderAndReturnsResponseAsSpecifiedObject()
        {
            // Arrange
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
            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.SESSION)), _authorizationHeaders, Arg.AnyString))
                .IgnoreInstance()
                .Returns(_sessionResponse.SerializeObject())
                .OccursOnce();

            // Act
            var sessionResponse = client.Invoke<SessionResponse>(HttpMethod.Get, Constants.ApiOperation.SESSION, null, null);

            // Assert
            Assert.IsNotNull(sessionResponse);
            Assert.IsTrue(typeof(SessionResponse) == sessionResponse.GetType());
            Assert.IsFalse(string.IsNullOrWhiteSpace(sessionResponse.UserId));
            Assert.IsFalse(string.IsNullOrWhiteSpace(sessionResponse.SecurityToken));

            Mock.Assert(restCallExecutor);
        }
    }
}
