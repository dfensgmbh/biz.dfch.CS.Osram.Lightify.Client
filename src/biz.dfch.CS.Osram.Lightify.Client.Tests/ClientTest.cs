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
using biz.dfch.CS.Osram.Lightify.Client.Model;
using biz.dfch.CS.Testing.Attributes;
using biz.dfch.CS.Web.Utilities.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    [TestClass]
    public class ClientTest
    {
        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+username")]
        public void GetTokenWithNullUsernameThrowsContractException()
        {
            // Arrange
            var sut = new Client(Constants.OSRAM_LIGHTIFY_BASE_URI);

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
            var sut = new Client(Constants.OSRAM_LIGHTIFY_BASE_URI);

            var username = Constants.USERNAME;
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
            var sut = new Client(Constants.OSRAM_LIGHTIFY_BASE_URI);

            var username = Constants.USERNAME;
            var password = Constants.PASSWORD;
            var serialNumber = default(string);

            // Act
            sut.GetToken(username, password, serialNumber);

            // Assert
        }

        [TestMethod]
        public void GetTokenSucceeds()
        {
            // Arrange
            var input = new SessionResponse
            {
                UserId = Constants.USER_ID,
                SecurityToken = Constants.SECURITY_TOKEN
            };
            var json = input.SerializeObject();
            
            var restCallExecutor = Mock.Create<RestCallExecutor>();
            Mock.Arrange(() => restCallExecutor.Invoke(Arg.IsAny<HttpMethod>(), Arg.IsAny<string>(), Arg.IsAny<IDictionary<string, string>>(), Arg.AnyString))
                .IgnoreInstance()
                .Returns(json);

            var sut = new Client(Constants.OSRAM_LIGHTIFY_BASE_URI);

            // Act
            var result = sut.GetToken(Constants.USERNAME, Constants.PASSWORD, Constants.SERIAL_NUMBER);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));

            Mock.Assert(restCallExecutor);
        }

        [TestMethod]
        public void GetTokenSetsUserInformation()
        {
            // Arrange
            var input = new SessionResponse
            {
                UserId = Constants.USER_ID,
                SecurityToken = Constants.SECURITY_TOKEN
            };
            var json = input.SerializeObject();

            var restCallExecutor = Mock.Create<RestCallExecutor>();
            Mock.Arrange(() => restCallExecutor.Invoke(Arg.IsAny<HttpMethod>(), Arg.IsAny<string>(), Arg.IsAny<IDictionary<string, string>>(), Arg.AnyString))
                .IgnoreInstance()
                .Returns(json);

            var sut = new Client(Constants.OSRAM_LIGHTIFY_BASE_URI);

            // Act
            var result = sut.GetToken(Constants.USERNAME, Constants.PASSWORD, Constants.SERIAL_NUMBER);
            
            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
            Assert.IsNotNull(sut.UserInformation);
            Assert.AreEqual(Constants.USER_ID, sut.UserInformation.UserId);
            Assert.AreEqual(Constants.USERNAME, sut.UserInformation.Username);
            Assert.AreEqual(Constants.PASSWORD, sut.UserInformation.Password);
            Assert.AreEqual(Constants.SERIAL_NUMBER, sut.UserInformation.SerialNumber);
            Assert.AreEqual(Constants.SECURITY_TOKEN, sut.UserInformation.SecurityToken);

            Mock.Assert(restCallExecutor);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+requestUriSuffix")]
        public void InvokeWithNullRequestUriSuffixThrowsContractException()
        {
            // Arrange
            var client = new Client(Constants.OSRAM_LIGHTIFY_BASE_URI);

            // Act
            client.Invoke(HttpMethod.Get, null, null, "");

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+requestUriSuffix")]
        public void InvokeWithEmptyRequestUriSuffixThrowsContractException()
        {
            // Arrange
            var client = new Client(Constants.OSRAM_LIGHTIFY_BASE_URI);

            // Act
            client.Invoke(HttpMethod.Get, "", null, "");

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+!headers.ContainsKey.Constants.HttpHeaders.AUTHORIZATION")]
        public void InvokeWithHeadersContainingAuthorizationHeaderThrowsContractException()
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

            var headers = new Dictionary<string, string>()
            {
                {Lightify.Client.Constants.HttpHeaders.AUTHORIZATION, Constants.SECURITY_TOKEN}
            };

            // Act
            client.Invoke(HttpMethod.Get, Lightify.Client.Constants.ApiSuffixes.SESSION, headers, "");

            // Assert
        }

        [TestMethod]
        public void InvokeWithNullBodyCallsRestExecutorWithEmptyBody()
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

            var input = new SessionResponse
            {
                UserId = Constants.USER_ID,
                SecurityToken = Constants.SECURITY_TOKEN
            };
            var json = input.SerializeObject();

            var headers = new Dictionary<string, string>()
            {
                {Lightify.Client.Constants.HttpHeaders.AUTHORIZATION, Constants.SECURITY_TOKEN}
            };

            var restCallExecutor = Mock.Create<RestCallExecutor>();
            var requestUri = new Uri(Constants.OSRAM_LIGHTIFY_BASE_URI, Lightify.Client.Constants.ApiSuffixes.SESSION);
            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, requestUri.AbsoluteUri, headers, ""))
                .IgnoreInstance()
                .Returns(json);

            // Act
            var responseAsString = client.Invoke(HttpMethod.Get, Lightify.Client.Constants.ApiSuffixes.SESSION, null, null);

            // Assert
            Assert.IsNotNull(responseAsString);
            Assert.IsFalse(string.IsNullOrWhiteSpace(responseAsString));

            Mock.Assert(restCallExecutor);
        }

        [TestMethod]
        public void InvokeCallsRestExecutorWithAuthorizationHeaderAndReturnsResponseAsString()
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

            var input = new SessionResponse
            {
                UserId = Constants.USER_ID,
                SecurityToken = Constants.SECURITY_TOKEN
            };
            var json = input.SerializeObject();

            var headers = new Dictionary<string, string>()
            {
                {Lightify.Client.Constants.HttpHeaders.AUTHORIZATION, Constants.SECURITY_TOKEN}
            };

            var restCallExecutor = Mock.Create<RestCallExecutor>();
            var requestUri = new Uri(Constants.OSRAM_LIGHTIFY_BASE_URI, Lightify.Client.Constants.ApiSuffixes.SESSION);
            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, requestUri.AbsoluteUri, headers, Arg.AnyString))
                .IgnoreInstance()
                .Returns(json);

            // Act
            var responseAsString = client.Invoke(HttpMethod.Get, Lightify.Client.Constants.ApiSuffixes.SESSION, null, "");

            // Assert
            Assert.IsNotNull(responseAsString);
            Assert.IsFalse(string.IsNullOrWhiteSpace(responseAsString));

            Mock.Assert(restCallExecutor);
        }

        [TestMethod]
        public void GenericInvokeCallsRestExecutorWithAuthorizationHeaderAndReturnsResponseAsSpecifiedObject()
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

            var input = new SessionResponse
            {
                UserId = Constants.USER_ID,
                SecurityToken = Constants.SECURITY_TOKEN
            };
            var json = input.SerializeObject();

            var headers = new Dictionary<string, string>()
            {
                {Lightify.Client.Constants.HttpHeaders.AUTHORIZATION, Constants.SECURITY_TOKEN}
            };

            var restCallExecutor = Mock.Create<RestCallExecutor>();
            var requestUri = new Uri(Constants.OSRAM_LIGHTIFY_BASE_URI, Lightify.Client.Constants.ApiSuffixes.SESSION);
            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, requestUri.AbsoluteUri, headers, Arg.AnyString))
                .IgnoreInstance()
                .Returns(json);

            // Act
            var sessionResponse = client.Invoke<SessionResponse>(HttpMethod.Get, Lightify.Client.Constants.ApiSuffixes.SESSION, null, "");

            // Assert
            Assert.IsNotNull(sessionResponse);
            Assert.IsTrue(typeof(SessionResponse) == sessionResponse.GetType());
            Assert.IsFalse(string.IsNullOrWhiteSpace(sessionResponse.UserId));
            Assert.IsFalse(string.IsNullOrWhiteSpace(sessionResponse.SecurityToken));
            Mock.Assert(restCallExecutor);
        }
    }
}
