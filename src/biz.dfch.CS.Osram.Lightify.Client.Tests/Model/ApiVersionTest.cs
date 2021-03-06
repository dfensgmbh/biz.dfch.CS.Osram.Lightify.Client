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
 
﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
﻿using biz.dfch.CS.Osram.Lightify.Client.Model;
﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests.Model
{
    [TestClass]
    public class ApiVersionTest
    {
        [TestMethod]
        public void DeserialiseSucceeds()
        {
            // Arrange
            var apiVersionAsJson = "{\"apiversion\":\"1.0.0\"}";

            // Act
            var apiVersion = BaseDto.DeserializeObject<ApiVersion>(apiVersionAsJson);

            // Assert
            Assert.IsNotNull(apiVersion);
            Assert.IsNotNull(apiVersion.Version);
            Assert.AreEqual(1, apiVersion.Version.Major);
            Assert.AreEqual(0, apiVersion.Version.Minor);
            Assert.AreEqual(0, apiVersion.Version.Build);
        }
    }
}
