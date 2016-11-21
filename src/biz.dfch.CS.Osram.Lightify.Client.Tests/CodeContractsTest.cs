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
using biz.dfch.CS.Testing.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace biz.dfch.CS.Osram.Lightify.Client.Tests
{
    [TestClass]
    public class CodeContractsTest
    {
        [TestMethod]
        public void InvokeWithTrueSucceeds()
        {
            var sut = new CodeContracts();
            sut.BoolMethod(true);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+itMustBeTrue")]
        public void InvokeWithFalseThrowsContractException()
        {
            var sut = new CodeContracts();
            sut.BoolMethod(false);
        }

        [TestMethod]
        public void InvokeWithStringSucceeds()
        {
            var sut = new CodeContracts();
            sut.StringMethod("Arbitrary String");
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+itMustNotBeNullOrEmptyOrWhiteSpace")]
        public void InvokeWithEmptyStringThrowsContractException()
        {
            var sut = new CodeContracts();
            sut.StringMethod(string.Empty);
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+itMustNotBeNullOrEmptyOrWhiteSpace")]
        public void InvokeWithWhiteSpaceStringThrowsContractException()
        {
            var sut = new CodeContracts();
            sut.StringMethod("   ");
        }

        [TestMethod]
        [ExpectContractFailure(MessagePattern = "Precondition.+itMustNotBeNullOrEmptyOrWhiteSpace")]
        public void InvokeWithNullStringThrowsContractException()
        {
            var sut = new CodeContracts();
            sut.StringMethod(default(string));
        }
    }
}
