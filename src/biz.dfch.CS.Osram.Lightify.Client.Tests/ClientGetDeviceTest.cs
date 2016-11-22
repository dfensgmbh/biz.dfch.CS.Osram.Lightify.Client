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
using biz.dfch.CS.Osram.Lightify.Client.Model;
using biz.dfch.CS.Testing.Attributes;
using biz.dfch.CS.Web.Utilities.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

using Constants = biz.dfch.CS.Osram.Lightify.Client.Constants;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    [TestClass]
    public class ClientGetDeviceTest
    {
        public Client Sut { get; set; }
        public RestCallExecutor RestCallExecutor { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Sut = new Client(TestConstants.OSRAM_LIGHTIFY_BASE_URI)
            {
                UserInformation = new UserInformation()
                {
                    UserId = TestConstants.USER_ID,
                    Username = TestConstants.USERNAME,
                    Password = TestConstants.PASSWORD,
                    SerialNumber = TestConstants.SERIAL_NUMBER,
                    SecurityToken = TestConstants.SECURITY_TOKEN
                }
            };

            RestCallExecutor = Mock.Create<RestCallExecutor>();
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "id")]
        public void GetDeviceWithInvalidIdThrowsContractException()
        {
            var id = 0;
            var sut = new Client(TestConstants.OSRAM_LIGHTIFY_BASE_URI);

            var response = "";
            Mock.Arrange(() => RestCallExecutor.Invoke(HttpMethod.Get, Arg.Matches<string>(s => s.Contains(Constants.ApiOperation.DEVICE)), Arg.IsAny<Dictionary<string, string>>(), Arg.IsAny<string>()))
                .IgnoreInstance()
                .Returns(response)
                .OccursOnce();

            var result = sut.GetDevice(id);
        }
            
        [TestMethod]
        public void GetDeviceByIdSucceeds()
        {
            var id = 42;
            var sut = new Client(TestConstants.OSRAM_LIGHTIFY_BASE_URI);

            var result = sut.GetDevice(id);
        }

        [TestMethod]
        public void GetDeviceByNameSucceeds()
        {
            var name = "arbitaryName";
            var sut = new Client(TestConstants.OSRAM_LIGHTIFY_BASE_URI);

            var result = sut.GetDevice(name);
        }

        [TestMethod]
        public void GetDeviceByNameCaseSensitiveSucceeds()
        {
            var name = "CaseSensitiveName";
            var sut = new Client(TestConstants.OSRAM_LIGHTIFY_BASE_URI);

            var result = sut.GetDevice(name);
        }
    }
}
