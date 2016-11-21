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
using biz.dfch.CS.Testing.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    [TestClass]
    public class LoginTest
    {
        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+userName")]
        public void GetTokenWithNullUsernameThrowsContractException()
        {
            var sut = new Login();

            var userName = default(string);
            var password = default(string);
            var serialNumber = default(string);

            sut.GetToken(userName, password, serialNumber);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+password")]
        public void GetTokenWithNullPasswordThrowsContractException()
        {
            var sut = new Login();

            var userName = "ArbitraryUser";
            var password = default(string);
            var serialNumber = default(string);

            sut.GetToken(userName, password, serialNumber);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+serialNumber")]
        public void GetTokenWithNullSerialNumberThrowsContractException()
        {
            var sut = new Login();

            var userName = "ArbitraryUser";
            var password = "ArbitraryPassword";
            var serialNumber = default(string);

            sut.GetToken(userName, password, serialNumber);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+uri")]
        public void GetTokenWithEmptyUriThrowsContractException()
        {
            var sut = new Login();

            var userName = "ArbitraryUser";
            var password = "ArbitraryPassword";
            var serialNumber = "ArbitrarySerialNumber";

            var result = sut.GetToken(userName, password, serialNumber);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void GetTokenSucceeds()
        {
            var uri = new Uri("https://eu.lightify-api.org.example.com/lightify/services/session");
            var sut = new Login(uri);

            var userName = "ArbitraryUser";
            var password = "ArbitraryPassword";
            var serialNumber = "ArbitrarySerialNumber";

            var result = sut.GetToken(userName, password, serialNumber);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void GetTokenSucceeds2()
        {
            var uri = new Uri("https://eu.lightify-api.org/lightify/services/session");
            var sut = new Login(uri);

            var userName = "ArbitraryUser";
            var password = "ArbitraryPassword";
            var serialNumber = "ArbitrarySerialNumber";

            var result = sut.GetToken(userName, password, serialNumber);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }
    }
}
