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
﻿using biz.dfch.CS.Osram.Lightify.Client.General;
﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using biz.dfch.CS.Testing.Attributes;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests.General
{
    [TestClass]
    public class UriHelperTest
    {
        [TestMethod]
        [ExpectContractFailure]
        public void CreateQueryStringWithNullDictionaryThrowsContractException()
        {
            // Arrange

            // Act
            UriHelper.CreateQueryString(null);

            // Assert
        }

        [TestMethod]
        [ExpectContractFailure]
        public void CreateQueryStringWithEmptyDictionaryThrowsContractException()
        {
            // Arrange
            var emptyQueryParameters = new Dictionary<string, object>();

            // Act
            UriHelper.CreateQueryString(emptyQueryParameters);

            // Assert
        }

        [TestMethod]
        public void CreateQueryStringReturnsQueryParametersAsString()
        {
            // Arrange
            var expectedQueryString = string.Format("?{0}={1}&{2}=true", Constants.QueryParameter.IDX, TestConstants.IDX_VALUE, Constants.QueryParameter.ON_OFF);

            var queryParameters = new Dictionary<string, object>()
            {
                {Constants.QueryParameter.IDX, TestConstants.IDX_VALUE},
                {Constants.QueryParameter.ON_OFF, true}
            };

            // Act
            var queryString = UriHelper.CreateQueryString(queryParameters);

            // Assert
            Assert.AreEqual(expectedQueryString, queryString);
        }
    }
}
