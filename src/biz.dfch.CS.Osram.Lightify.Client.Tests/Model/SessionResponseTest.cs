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

using biz.dfch.CS.Osram.Lightify.Client.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests.Model
{
    [TestClass]
    public class SessionResponseTest
    {
        [TestMethod]
        public void DeserialiseSucceeds()
        {
            var userId = "arbitraryUser";
            var securityToken = "validSecurityToken";
            var input = new SessionResponse
            {
                UserId = userId,
                SecurityToken = securityToken
            };
            var json = input.SerializeObject();

            var sut = BaseDto.DeserializeObject<SessionResponse>(json);

            Assert.AreEqual(userId, sut.UserId);
            Assert.AreEqual(securityToken, sut.SecurityToken);
        }
    }
}
