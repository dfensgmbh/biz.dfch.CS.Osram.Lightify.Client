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
        private static readonly Uri OSRAM_LIGHTIFY_BASE_URI = new Uri("https://eu.lightify-api.org.example.com/lightify/services/");

        private const string USER_ID = "arbitraryUser";
        private const string SECURITY_TOKEN = "validSecurityToken";
        private const string USERNAME = "ArbitraryUser";
        private const string PASSWORD = "ArbitraryPassword";
        private const string SERIAL_NUMBER = "ArbitrarySerialNumber";

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+username")]
        public void GetTokenWithNullUsernameThrowsContractException()
        {
            // Arrange
            var sut = new Client(OSRAM_LIGHTIFY_BASE_URI);

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
            var sut = new Client(OSRAM_LIGHTIFY_BASE_URI);

            var username = "ArbitraryUser";
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
            var sut = new Client(OSRAM_LIGHTIFY_BASE_URI);

            var username = "ArbitraryUser";
            var password = "ArbitraryPassword";
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
                UserId = USER_ID,
                SecurityToken = SECURITY_TOKEN
            };
            var json = input.SerializeObject();
            
            var restClient = Mock.Create<RestCallExecutor>();
            Mock.Arrange(() => restClient.Invoke(Arg.IsAny<HttpMethod>(), Arg.IsAny<string>(), Arg.IsAny<IDictionary<string, string>>(), Arg.IsAny<string>()))
                .IgnoreInstance()
                .Returns(json);
            
            var sut = new Client(OSRAM_LIGHTIFY_BASE_URI);

            // Act
            var result = sut.GetToken(USERNAME, PASSWORD, SERIAL_NUMBER);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void GetTokenSetsUserInformation()
        {
            // Arrange
            var input = new SessionResponse
            {
                UserId = USER_ID,
                SecurityToken = SECURITY_TOKEN
            };
            var json = input.SerializeObject();

            var restClient = Mock.Create<RestCallExecutor>();
            Mock.Arrange(() => restClient.Invoke(Arg.IsAny<HttpMethod>(), Arg.IsAny<string>(), Arg.IsAny<IDictionary<string, string>>(), Arg.IsAny<string>()))
                .IgnoreInstance()
                .Returns(json);

            var sut = new Client(OSRAM_LIGHTIFY_BASE_URI);

            // Act
            var result = sut.GetToken(USERNAME, PASSWORD, SERIAL_NUMBER);
            
            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
            Assert.IsNotNull(sut.UserInformation);
            Assert.AreEqual(USER_ID, sut.UserInformation.UserId);
            Assert.AreEqual(USERNAME, sut.UserInformation.Username);
            Assert.AreEqual(PASSWORD, sut.UserInformation.Password);
            Assert.AreEqual(SERIAL_NUMBER, sut.UserInformation.SerialNumber);
            Assert.AreEqual(SECURITY_TOKEN, sut.UserInformation.SecurityToken);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+requestUriSuffix")]
        public void InvokeWithNullRequestUriSuffixThrowsContractException()
        {
            // Arrange
            var client = new Client(OSRAM_LIGHTIFY_BASE_URI);

            // Act
            client.Invoke(HttpMethod.Get, null, null, "");

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+requestUriSuffix")]
        public void InvokeWithEmptyRequestUriSuffixThrowsContractException()
        {
            // Arrange
            var client = new Client(OSRAM_LIGHTIFY_BASE_URI);

            // Act
            client.Invoke(HttpMethod.Get, "", null, "");

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+!headers.ContainsKey.Constants.HttpHeaders.AUTHORIZATION")]
        public void InvokeWithHeadersContainingAuthorizationHeaderThrowsContractException()
        {
            // Arrange
            var client = new Client(OSRAM_LIGHTIFY_BASE_URI);

            client.UserInformation = new UserInformation()
            {
                UserId = USER_ID,
                Username = USERNAME,
                Password = PASSWORD,
                SerialNumber = SERIAL_NUMBER,
                SecurityToken = SECURITY_TOKEN
            };

            var headers = new Dictionary<string, string>()
            {
                {Constants.HttpHeaders.AUTHORIZATION, SECURITY_TOKEN}
            };

            // Act
            client.Invoke(HttpMethod.Get, Constants.ApiSuffixes.SESSION, headers, "");

            // Assert
        }

        [TestMethod]
        public void InvokeWithNullBodyCallsRestExecutorWithEmptyBody()
        {
            // Arrange
            var client = new Client(OSRAM_LIGHTIFY_BASE_URI);

            client.UserInformation = new UserInformation()
            {
                UserId = USER_ID,
                Username = USERNAME,
                Password = PASSWORD,
                SerialNumber = SERIAL_NUMBER,
                SecurityToken = SECURITY_TOKEN
            };

            var input = new SessionResponse
            {
                UserId = USER_ID,
                SecurityToken = SECURITY_TOKEN
            };
            var json = input.SerializeObject();

            var headers = new Dictionary<string, string>()
            {
                {Constants.HttpHeaders.AUTHORIZATION, SECURITY_TOKEN}
            };

            var restClient = Mock.Create<RestCallExecutor>();
            var requestUri = new Uri(OSRAM_LIGHTIFY_BASE_URI, Constants.ApiSuffixes.SESSION);
            Mock.Arrange(() => restClient.Invoke(HttpMethod.Get, requestUri.AbsoluteUri, headers, ""))
                .IgnoreInstance()
                .Returns(json);

            // Act
            var responseAsString = client.Invoke(HttpMethod.Get, Constants.ApiSuffixes.SESSION, null, null);

            // Assert
            Assert.IsNotNull(responseAsString);
            Assert.IsFalse(string.IsNullOrWhiteSpace(responseAsString));
        }

        [TestMethod]
        public void InvokeCallsRestExecutorWithAuthorizationHeaderAndReturnsResponseAsString()
        {
            // Arrange
            var client = new Client(OSRAM_LIGHTIFY_BASE_URI);

            client.UserInformation = new UserInformation()
            {
                UserId = USER_ID,
                Username = USERNAME,
                Password = PASSWORD,
                SerialNumber = SERIAL_NUMBER,
                SecurityToken = SECURITY_TOKEN
            };

            var input = new SessionResponse
            {
                UserId = USER_ID,
                SecurityToken = SECURITY_TOKEN
            };
            var json = input.SerializeObject();

            var headers = new Dictionary<string, string>()
            {
                {Constants.HttpHeaders.AUTHORIZATION, SECURITY_TOKEN}
            };

            var restClient = Mock.Create<RestCallExecutor>();
            var requestUri = new Uri(OSRAM_LIGHTIFY_BASE_URI, Constants.ApiSuffixes.SESSION);
            Mock.Arrange(() => restClient.Invoke(HttpMethod.Get, requestUri.AbsoluteUri, headers, Arg.IsAny<string>()))
                .IgnoreInstance()
                .Returns(json);

            // Act
            var responseAsString = client.Invoke(HttpMethod.Get, Constants.ApiSuffixes.SESSION, null, "");

            // Assert
            Assert.IsNotNull(responseAsString);
            Assert.IsFalse(string.IsNullOrWhiteSpace(responseAsString));
        }

        [TestMethod]
        public void GenericInvokeCallsRestExecutorWithAuthorizationHeaderAndReturnsResponseAsSpecifiedObject()
        {
            // Arrange
            var client = new Client(OSRAM_LIGHTIFY_BASE_URI);

            client.UserInformation = new UserInformation()
            {
                UserId = USER_ID,
                Username = USERNAME,
                Password = PASSWORD,
                SerialNumber = SERIAL_NUMBER,
                SecurityToken = SECURITY_TOKEN
            };

            var input = new SessionResponse
            {
                UserId = USER_ID,
                SecurityToken = SECURITY_TOKEN
            };
            var json = input.SerializeObject();

            var headers = new Dictionary<string, string>()
            {
                {Constants.HttpHeaders.AUTHORIZATION, SECURITY_TOKEN}
            };

            var restClient = Mock.Create<RestCallExecutor>();
            var requestUri = new Uri(OSRAM_LIGHTIFY_BASE_URI, Constants.ApiSuffixes.SESSION);
            Mock.Arrange(() => restClient.Invoke(HttpMethod.Get, requestUri.AbsoluteUri, headers, Arg.IsAny<string>()))
                .IgnoreInstance()
                .Returns(json);

            // Act
            var sessionResponse = client.Invoke<SessionResponse>(HttpMethod.Get, Constants.ApiSuffixes.SESSION, null, "");

            // Assert
            Assert.IsNotNull(sessionResponse);
            Assert.IsTrue(typeof(SessionResponse) == sessionResponse.GetType());
            Assert.IsFalse(string.IsNullOrWhiteSpace(sessionResponse.UserId));
            Assert.IsFalse(string.IsNullOrWhiteSpace(sessionResponse.SecurityToken));
        }

        [TestMethod]
        public void GetGroupsSucceeds()
        {
            var groupID = 1;
            var name = "Arbitrary Name";

            var input = new Group()
            {

            };
            var json = input.SerializeObject();

            var client = Mock.Create<RestCallExecutor>();
            Mock.Arrange(() => client.Invoke(Arg.IsAny<HttpMethod>(), Arg.IsAny<string>(), Arg.IsAny<IDictionary<string, string>>(), Arg.IsAny<string>()))
                .IgnoreInstance()
                .Returns(json);
            var uri = new Uri("https://eu.lightify-api.org.example.com/lightify/services/");
            var sut = new Client(uri);

            var result = sut.GetGroups();
            Assert.IsFalse(null == result);
        }
    }
}
