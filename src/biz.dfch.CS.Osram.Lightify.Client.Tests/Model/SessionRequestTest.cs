using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace biz.dfch.CS.Osram.Lightify.Client.Model.Tests
{
    [TestClass]
    public class SessionRequestTest
    {
        [TestMethod]
        public void SerializationSucceeds()
        {
            var sut = new SessionRequest
            {
                Username = "ArbitraryUsername",
                Password = "ArbitraryPassword",
                SerialNumber = "ArbitrarySerialNumber"
            };
            var result = sut.SerializeObject();
            Assert.IsTrue(result.Contains("\"username\""));
        }
    }
}
