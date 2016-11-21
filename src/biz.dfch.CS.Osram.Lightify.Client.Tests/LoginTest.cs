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
    public class LoginTest
    {
        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+userName")]
        public void GetTokenWithNullUsernameThrowsContractException()
        {
            var sut = new Client();

            var userName = default(string);
            var password = default(string);
            var serialNumber = default(string);

            sut.GetToken(userName, password, serialNumber);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+password")]
        public void GetTokenWithNullPasswordThrowsContractException()
        {
            var sut = new Client();

            var userName = "ArbitraryUser";
            var password = default(string);
            var serialNumber = default(string);

            sut.GetToken(userName, password, serialNumber);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+serialNumber")]
        public void GetTokenWithNullSerialNumberThrowsContractException()
        {
            var sut = new Client();

            var userName = "ArbitraryUser";
            var password = "ArbitraryPassword";
            var serialNumber = default(string);

            sut.GetToken(userName, password, serialNumber);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+baseUri")]
        public void GetTokenWithEmptyUriThrowsContractException()
        {
            var sut = new Client();

            var userName = "ArbitraryUser";
            var password = "ArbitraryPassword";
            var serialNumber = "ArbitrarySerialNumber";

            var result = sut.GetToken(userName, password, serialNumber);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void GetTokenSucceeds()
        {
            var userId = "arbitraryUser";
            var securityToken = "validSecurityToken";
            var input = new SessionResponse
            {
                UserId = userId,
                SecurityToken = securityToken
            };
            var json = input.SerializeObject();
            
            var client = Mock.Create<RestCallExecutor>();
            Mock.Arrange(() => client.Invoke(Arg.IsAny<HttpMethod>(), Arg.IsAny<string>(), Arg.IsAny<IDictionary<string, string>>(), Arg.IsAny<string>()))
                .IgnoreInstance()
                .Returns(json);
            var uri = new Uri("https://eu.lightify-api.org.example.com/lightify/services/");
            var sut = new Client(uri);

            var userName = "ArbitraryUser";
            var password = "ArbitraryPassword";
            var serialNumber = "ArbitrarySerialNumber";

            var result = sut.GetToken(userName, password, serialNumber);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }
    }
}
