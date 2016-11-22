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

using biz.dfch.CS.Osram.Lightify.Client.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests.Model
{
    [TestClass]
    public class SessionRequestTest
    {
        [TestMethod]
        public void SerialisationSucceeds()
        {
            // Arrange
            var sut = new SessionRequest
            {
                Username = TestConstants.USERNAME,
                Password = TestConstants.PASSWORD,
                SerialNumber = TestConstants.SERIAL_NUMBER
            };

            // Act
            var result = sut.SerializeObject();

            // Assert
            Assert.IsTrue(result.Contains("\"username\""));
            Assert.IsTrue(result.Contains("\"password\""));
            Assert.IsTrue(result.Contains("\"serialNumber\""));
        }
    }
}
