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
