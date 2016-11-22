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
using biz.dfch.CS.Web.Utilities.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    [TestClass]
    public class ClientGetGroupsTest
    {
        [TestMethod]
        public void GetGroupsSucceeds()
        {
            var sut = new Client(Constants.OSRAM_LIGHTIFY_BASE_URI);
            sut.UserInformation = new UserInformation()
            {
                UserId = Constants.USER_ID,
                Username = Constants.USERNAME,
                Password = Constants.PASSWORD,
                SerialNumber = Constants.SERIAL_NUMBER,
                SecurityToken = Constants.SECURITY_TOKEN
            };

            string response = @"
                [{
	                ""groupId"": 1,
	                ""name"": ""First and Second"",
	                ""devices"": [2,
	                1],
	                ""scenes"": {
		
	                }
                },
                {
	                ""groupId"": 2,
	                ""name"": ""Third Lamp"",
	                ""devices"": [3],
	                ""scenes"": {
		
	                }
                }]";

            var restCallExecutor = Mock.Create<RestCallExecutor>();
            Mock.Arrange(() => restCallExecutor.Invoke(HttpMethod.Get, Arg.AnyString, Arg.IsAny<Dictionary<string, string>>(), ""))
                .IgnoreInstance()
                .Returns(response)
                .OccursOnce();

            var result = sut.GetGroups();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            
            Mock.Assert(restCallExecutor);
        }
    }
}
